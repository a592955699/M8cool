using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using InfoSnifferForm;
using System.Threading;
using System.Web;

namespace InfoSniffer
{
    public class DetailPage : PageBase
    {
        bool _done;
        int _pageIndex = 1;

        ListDictionary _resultItems;
        List<DetailPage> _subPages;

        #region 属性
        /// <summary>
        /// 是否采集完成
        /// </summary>
        public override bool Done
        {
            get { return _done; }
        }
        /// <summary>
        /// 是否成功采集
        /// </summary>
        public override bool Succeed
        {
            get
            {
                return !string.IsNullOrEmpty(PageBody);
            }
        }

        /// <summary>
        /// 采集的结果集合
        /// </summary>
        public ListDictionary ResultItems
        {
            get
            {
                if (_resultItems == null)
                    _resultItems = new ListDictionary();
                return _resultItems;
            }
        }

        /// <summary>
        /// 子页集合
        /// </summary>
        public List<DetailPage> SubPages
        {
            get
            {
                if (_subPages == null)
                    _subPages = new List<DetailPage>();
                return _subPages;

            }
        }

        public DetailPageConfiguration DetailPageConfiguration
        {
            get
            {
                return (DetailPageConfiguration)this.Configuration;
            }
        }

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }
        #endregion

        public DetailPage() : base() { }

        public DetailPage(DetailPageConfiguration configuration) : base(configuration) { }

        public DetailPage(PageBase parent, DetailPageConfiguration configuration) : base(parent, configuration) { }


        /// <summary>
        /// 采集
        /// </summary>
        /// <returns></returns>
        public override bool Sniffer()
        {
            bool bool1 = InlineSniffer();
            //return bool1;
            if (!bool1)
            {
                return false;
            }

            //这里开始内容页的翻页
            if (string.IsNullOrEmpty(this.DetailPageConfiguration.PageQuery))
            {
                return bool1;
            }

            DetailPage rootDetailPage = this;
            ListPage listPage = rootDetailPage.Parent as ListPage;
            while (listPage == null)
            {
                rootDetailPage = rootDetailPage.Parent as DetailPage;
                listPage = rootDetailPage.Parent as ListPage;
            }

            DetailPage detailPage = new DetailPage(rootDetailPage, (DetailPageConfiguration)listPage.ListPageConfiguration.SubPageConfiguration);
            detailPage.PageName = this.PageName;
            detailPage.PageUrl = this.PageUrl;

            int pageIndex = this.PageIndex + this.DetailPageConfiguration.PageIndexStep;

            ReplacePageIndex(detailPage, pageIndex);

            //如果内容一样，则表示已经结束了啦
            if (detailPage.PageBody == this.PageBody)
            {
                return bool1;
            }
            else
            {
                //否则要看看，识别项的正则结果是否一样
                if (!string.IsNullOrEmpty(this.Configuration.EndPageDetermineRegex))
                {
                    Match match1 = Regex.Match(this.PageBody, this.Configuration.EndPageDetermineRegex, (RegexOptions)25);
                    Match match2 = Regex.Match(detailPage.PageBody, detailPage.Configuration.EndPageDetermineRegex, (RegexOptions)25);
                    if (match1.Value == match2.Value)
                    {
                        return bool1;
                    }
                }
            }

            detailPage.Sniffer();

            return bool1;
        }

        private bool InlineSniffer()
        {
            if (!this.Succeed)
            {
                _done = true;
                return false;
            }

            //采集项
            foreach (SnifferItem item in this.DetailPageConfiguration.SnifferItems)
            {
                //这里的意思应该是，如果不是多页的内容，则当然页如果大于开始页了，则不再采集了。
                if (this.PageIndex > this.DetailPageConfiguration.PageStartIndex && !item.MutiPage)
                {
                    continue;
                }

                if (item.RegexString != null)
                {
                    MatchCollection matchs = Regex.Matches(this.PageBody, item.RegexString.Expression, (RegexOptions)25);

                    if (matchs.Count > 0)
                    {
                        System.Text.StringBuilder sb = new StringBuilder();
                        foreach (Match match in matchs)
                        {
                            if (matchs.Count > 1 && sb.Length > 0)
                                sb.Append(item.Separator);
                            string value = match.Groups[item.RegexString.ValueGroupIndex].Value;

                            if (string.IsNullOrEmpty(value))
                            {
                                value = item.DefaultValue;
                            }
                            else
                            {
                                //清理垃圾
                                ClearRubbish(item, ref  value);
                                //将内容里的URL转成绝对路径
                                if (item.UrlToAbs)
                                    UrlToAbs(item, ref value);
                                //如果采集的是Url则转换成绝对路径
                                if (item.IsUrl)
                                    value = FileUtil.GetAbsUrl(value, this.SubPageBaseUrl);
                                //保存图片
                                if (item.SaveImage)
                                    SaveImages(item, ref value);
                                //清除A元素
                                if (item.ClearAElement)
                                    ClearAElement(item, ref value);
                                //清除HTML代码
                                if (item.IsClearHTML)
                                    value = ClearHTML(value);

                            }

                            sb.Append(value);
                        }
                        this.ResultItems.Add(item.ItemName, sb.ToString().Trim());
                    }
                    else
                    {
                        this.ResultItems.Add(item.ItemName, item.DefaultValue);
                    }
                }
                else
                {
                    this.ResultItems.Add(item.ItemName, item.DefaultValue);
                }
            }


            //整合字段
            string text1 = this.PageUrl;
            DetailPage parentPage = this.Parent as DetailPage;
            if (parentPage != null)
            {
                foreach (string key in this.ResultItems.Keys)
                {
                    string value = (string)this.ResultItems[key];
                    if (this.ResultItems.Contains(key))
                    {
                        string parentPageValue = (string)parentPage.ResultItems[key];
                        if (parentPageValue != value)
                        {
                            SnifferItem item = null;
                            foreach (SnifferItem itm in parentPage.DetailPageConfiguration.SnifferItems)
                            {
                                if (itm.ItemName == key)
                                {
                                    item = itm;
                                    break;
                                }
                            }
                            parentPage.ResultItems[key] = parentPageValue + item.MutiPageSeparator + value;
                        }
                    }
                    else
                    {
                        parentPage.ResultItems.Add(key, value);
                    }
                }
            }

            //采集子页
            foreach (DetailPageConfiguration conf in this.DetailPageConfiguration.SubPageConfigurations)
            {
                MatchCollection matches = Regex.Matches(this.PageBody, conf.SnifferSubPageUrlItem.Expression, (RegexOptions)25);

                if (matches != null && matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        if (!string.IsNullOrEmpty(match.Value))
                        {
                            DetailPage detailPage = new DetailPage(this, conf);
                            detailPage.PageName = match.Groups[conf.SnifferSubPageUrlItem.TitleGroupIndex].Value;

                            string url = match.Groups[conf.SnifferSubPageUrlItem.UrlGroupIndex].Value;

                            if (!string.IsNullOrEmpty(conf.SnifferSubPageUrlItem.UrlFormat))
                                url = string.Format(conf.SnifferSubPageUrlItem.UrlFormat, url);

                            detailPage.PageUrl = FileUtil.GetAbsUrl(url, this.SubPageBaseUrl);

                            detailPage.Sniffer();

                            this.SubPages.Add(detailPage);
                        }
                    }
                }
            }


            _done = true;
            return true;
        }

        private void ReplacePageIndex(DetailPage detailPage, int pageIndex)
        {
            detailPage.PageIndex = pageIndex;
            string stringPageIndex;
            if (!string.IsNullOrEmpty(detailPage.DetailPageConfiguration.PageIndexFormat))
                stringPageIndex = pageIndex.ToString(detailPage.DetailPageConfiguration.PageIndexFormat);
            else
                stringPageIndex = pageIndex.ToString();

            string pageQuery = string.Format(detailPage.DetailPageConfiguration.ReplacePageQuery, stringPageIndex);
            string pageUrl = Regex.Replace(detailPage.PageUrl, detailPage.DetailPageConfiguration.PageQuery, pageQuery);
            detailPage.PageUrl = pageUrl;
        }

        /// <summary>
        /// 清理值中一些垃圾，比如js等等
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public void ClearRubbish(SnifferItem item, ref string value)
        {
            if (!string.IsNullOrEmpty(item.ClearRegexString))
                value = Regex.Replace(value, item.ClearRegexString, string.Empty, (RegexOptions)25);
            value = Regex.Replace(value, "<script[^>]*?>(.*?)</script>", string.Empty, (RegexOptions)25);
            value = Regex.Replace(value, "(<[^>]*? )(onclick=['\"].*?['\"])(( [^>]*?)?>.*?</[^>]*?>)", "$1$3", (RegexOptions)25);
            value = Regex.Replace(value, "(<[^>]*?)=(['\"](java|vb)script:.*?['\"])(( [^>]*?)?>.*?</[^>]*?>)", "$1$4", (RegexOptions)25);
            value = Regex.Replace(value, "<!--.*?-->", "", (RegexOptions)25);
        }

        /// <summary>
        /// 清理A元素
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public void ClearAElement(SnifferItem item, ref string value)
        {
            value = Regex.Replace(value, "<a\\s[^>]*?>(.*?)</a>", "$1", (RegexOptions)25);

            //MatchCollection matchs = Regex.Matches(value, "<a [^>]*?>(.*?)</a>", (RegexOptions)25);

            //List<Match> matchList = new List<Match>();

            //foreach (Match match in matchs)
            //{
            //    bool bool1 = false;
            //    foreach (Match match1 in matchList)
            //    {
            //        if (string.Compare(match1.Value, match.Value, true) == 0)
            //            bool1 = true;
            //    }
            //    if (!bool1)
            //    {
            //        value = value.Replace(match.Value, match.Groups[1].Value);
            //        matchList.Add(match);
            //    }
            //}
        }

        /// <summary>
        /// 将链接替成绝对路径
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public void UrlToAbs(SnifferItem item, ref string value)
        {
            MatchCollection matchs = Regex.Matches(value, "((?<=<a[^>]*?href=[\"']?)(\\.|/|http)[^\"' >]*)|((?<=<img[^>]*?src=[\"']?)(\\.|/|http)[^\"' >]*)", (RegexOptions)25);

            List<Match> matchList = new List<Match>();

            foreach (Match match in matchs)
            {
                bool bool1 = false;
                foreach (Match match1 in matchList)
                {
                    if (string.Compare(match1.Value, match.Value, true) == 0)
                        bool1 = true;
                }
                if (!bool1)
                {
                    string absUrl = FileUtil.GetAbsUrl(match.Value, this.SubPageBaseUrl);
                    value = value.Replace(match.Value, absUrl);
                    matchList.Add(match);
                }
            }

        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="value"></param>
        public void SaveImages(SnifferItem item, ref string value)
        {
            value = value.Replace("wallpaper:", "http:");

            MatchCollection imgMatchs;
            if (item.IsUrl)
                imgMatchs = Regex.Matches(value, "^.*$", (RegexOptions)25);
            else
                imgMatchs = Regex.Matches(value, "(?<=<img[^>]*?src=[\"']?)(\\.|/|http)[^\"' >]*", (RegexOptions)25);

            ListPage parentPage = this.Parent as ListPage;
            if (parentPage == null)
                parentPage = this.Parent.Parent as ListPage;

            string saveImagesPath = string.Empty;

            string dtPath = DateTime.Now.ToString("yyyy\\\\MM");

            if (!string.IsNullOrEmpty(item.SaveImagesPath))
                saveImagesPath = string.Format(item.SaveImagesPath, dtPath);
            else
                saveImagesPath = parentPage.SavePathAndFileName.Substring(0, parentPage.SavePathAndFileName.LastIndexOf(".")) + "\\";

            if (!saveImagesPath.EndsWith("\\"))
                saveImagesPath += "\\";

            if (!Directory.Exists(saveImagesPath))
            {
                Directory.CreateDirectory(saveImagesPath);
            }

            foreach (Match match in imgMatchs)
            {
                int lastIndex = match.Value.LastIndexOf("/") + 1;
                string fileName;
                if (lastIndex >= 0)
                    fileName = match.Value.Substring(lastIndex);
                else
                    fileName = match.Value;

                if (fileName.Contains("?"))
                {
                    fileName = fileName.Substring(0, fileName.IndexOf("?"));
                }

                fileName = HttpUtility.UrlDecode(fileName);
                try
                {
                    string absUrl = FileUtil.GetAbsUrl(match.Value, this.SubPageBaseUrl);

                    Stream stream = FileUtil.GetPageStream(absUrl, this.SubPageBaseUrl);
                    if (stream != null)
                    {
                        if (this.Upload)
                        {
                            Thread thread = new Thread(new ParameterizedThreadStart(UploadFile));
                            thread.Start(new object[] { stream, saveImagesPath, fileName, "image/jpeg" });

                        }
                        else
                        {
                            FileUtil.StreamSaveToFile(stream, saveImagesPath + fileName);
                        }
                    }

                    string text1 = string.Empty;
                    if (!string.IsNullOrEmpty(item.ImageUrlPath))
                    {
                        string imageUrlPath = string.Format(item.ImageUrlPath, dtPath.Replace("\\", "/"));
                        if (!imageUrlPath.EndsWith("/"))
                        {
                            imageUrlPath += "/";
                        }
                        text1 = value.Replace(match.Value, imageUrlPath + fileName);
                    }
                    else
                    {
                        text1 = value.Replace(match.Value, saveImagesPath + fileName);
                    }
                    value = text1;
                }
                catch (System.Exception e)
                {
                    LogManager.WriteLog(string.Format("<saveimgerr><img>{0}</img><error>{1}</error></saveimgerr>", match.Value, e.Message));
                    //throw e;
                };
            }
        }

        void UploadFile(object args)
        {
            object[] arr = (object[])args;
            Stream stream = (Stream)arr[0];
            MemoryStream mStream = new MemoryStream();
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while (true)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    break;
                mStream.Write(buffer, 0, bytesRead);
            }
            string dir = (string)arr[1];
            string fileName = (string)arr[2];
            string contentType = (string)arr[3];
            WebUpload upload = new WebUpload();
            upload.UploadUrl = this.UploadUrl;
            upload.Rename = false;
            upload.Overwrite = true;
            upload.Files = new UploadFile[] { new UploadFile(mStream, fileName, contentType) };
            upload.SavePath = dir;
            upload.Upload();
        }
    }
}
