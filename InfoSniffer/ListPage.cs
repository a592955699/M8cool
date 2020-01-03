using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace InfoSniffer
{
    public class ListPage : PageBase
    {
        bool _done;
        string _pageQuery;
        string _pageBody;
        string _savePath;

        List<UrlItem> _subPageUrlItems;

        MatchCollection _snifferSubPageUrlMatchCollection;

        #region 属性
        /// <summary>
        /// 是否成功采集
        /// </summary>
        public override bool Succeed
        {
            get
            {
                return SnifferSubPageUrlMatchCollection != null && SnifferSubPageUrlMatchCollection.Count > 0;
            }
        }

        /// <summary>
        /// 是否采集完成
        /// </summary>
        public override bool Done
        {
            get { return _done; }
        }

        /// <summary>
        /// 配置
        /// </summary>
        public ListPageConfiguration ListPageConfiguration
        {
            get
            {
                return (ListPageConfiguration)this.Configuration;
            }
        }

        /// <summary>
        /// 采集到的子页网址匹配项集合
        /// </summary>
        public MatchCollection SnifferSubPageUrlMatchCollection
        {
            get
            {
                if (_snifferSubPageUrlMatchCollection == null)
                {
                    _snifferSubPageUrlMatchCollection = Regex.Matches(this.PageBody, this.ListPageConfiguration.SnifferSubPageUrlItem.Expression, (RegexOptions)25);
                }
                return _snifferSubPageUrlMatchCollection;
            }
        }

        /// <summary>
        /// 采集页Url项
        /// </summary>
        public SnifferUrlItem SnifferSubPageUrlItem
        {
            get { return this.ListPageConfiguration.SnifferSubPageUrlItem; }
        }

        /// <summary>
        /// 子页Url项
        /// </summary>
        public List<UrlItem> SubPageUrlResults
        {
            get
            {
                if (_subPageUrlItems == null)
                    _subPageUrlItems = new List<UrlItem>();
                return _subPageUrlItems;
            }
        }

        /// <summary>
        /// 第一个子页Url项
        /// </summary>
        public UrlItem FirstSubPageUrlItem
        {
            get
            {
                if (_subPageUrlItems == null || _subPageUrlItems.Count == 0)
                    return null;
                return _subPageUrlItems[0];
            }
        }

        /// <summary>
        /// 翻页字符正则
        /// </summary>
        public string PageQuery
        {
            get
            {
                if (_pageQuery == null)
                    return this.ListPageConfiguration.PageQuery;
                return _pageQuery;
            }
            set
            {
                _pageQuery = value;
            }
        }

        /// <summary>
        /// 采集回来的页内容
        /// </summary>
        public override string PageBody
        {
            get
            {
                if (_pageBody == null)
                {
                    try
                    {
                        if (this.ListPageConfiguration.PageMethod == PageMethod.Get)
                        {
                            _pageBody = FileUtil.GetPageText(this.PageUrl, this.Configuration.Encoding);
                        }
                        else
                        {
                            HttpWebRequest httpReq;

                            //string url = this.PageUrl.Replace("&amp;", "&");

                            httpReq = (HttpWebRequest)WebRequest.Create(this.PageUrl);

                            httpReq.Method = "POST";
                            httpReq.KeepAlive = false; // 获取或设置一个值，该值指示是否与 Internet 资源建立持久连接。
                            httpReq.ContentType = "application/x-www-form-urlencoded";

                            string postData = string.Format(this.PageQuery, this.ListPageConfiguration.PageIndexSeed);

                            Encoding encoding = this.Configuration.Encoding;
                            byte[] byte1 = encoding.GetBytes(postData);
                            httpReq.ContentLength = byte1.Length;
                            Stream newStream = httpReq.GetRequestStream();
                            newStream.Write(byte1, 0, byte1.Length);
                            newStream.Close();

                            // Get the response.
                            HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse();

                            // Get the stream containing content returned by the server.
                            Stream dataStream = response.GetResponseStream();
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream, encoding);
                            // Read the content.
                            _pageBody = reader.ReadToEnd();
                            // Clean up the streams.
                            reader.Close();
                            dataStream.Close();
                            response.Close();
                        }
                    }
                    catch
                    {
                        _pageBody = string.Empty;
                    }
                }
                return _pageBody;
            }
        }

        public string SavePath
        {
            get
            {
                if (_savePath == null)
                {
                    string filePath = string.Empty;
                    ListPage parentListPage = (ListPage)this.Parent;
                    RootPageConfiguration rootPageConfiguration = null;

                    while (parentListPage != null)
                    {
                        filePath = parentListPage.PageName.Replace("/", "_").Replace("\\", "_") + "\\" + filePath;
                        if (parentListPage != null && parentListPage.Configuration is RootPageConfiguration)
                        {
                            rootPageConfiguration = parentListPage.Configuration as RootPageConfiguration;
                        }
                        parentListPage = (ListPage)parentListPage.Parent;

                    }
                    string text1 = rootPageConfiguration.SavePath;
                    _savePath = string.Format("{0}Data\\{1}\\{2}", text1, DateTime.Now.ToString("yyyy-MM-dd"), filePath);
                }
                return _savePath;
            }
        }

        public string SavePathAndFileName
        {
            get
            {
                string fileName = this.PageName.Replace("/", "_").Replace("\\", "_");
                string ext = ".xml";
                string tmpFileName = fileName;
                if (!this.Upload)
                {
                    int i = 1;
                    while (File.Exists(this.SavePath + tmpFileName + ext))
                    {
                        tmpFileName = string.Format("{0}({1})", fileName, i);
                        i++;
                    }
                }
                return this.SavePath + tmpFileName + ext;
            }
        }
        #endregion

        public ListPage() : base() { }

        public ListPage(ListPageConfiguration configuration) : base(configuration) { }

        public ListPage(PageBase parent, ListPageConfiguration configuration) : base(parent, configuration) { }

        /// <summary>
        /// 开始采集子页
        /// </summary>
        /// <returns></returns>
        public override bool Sniffer()
        {
            if (!Succeed || this.Done)
            {
                _done = true;
                return false;
            }

            foreach (Match item in SnifferSubPageUrlMatchCollection)
            {
                UrlItem urlItem = new UrlItem();

                string url = item.Groups[this.ListPageConfiguration.SnifferSubPageUrlItem.UrlGroupIndex].Value;

                if (!string.IsNullOrEmpty(this.SnifferSubPageUrlItem.UrlFormat))
                    url = string.Format(this.SnifferSubPageUrlItem.UrlFormat, url);

                urlItem.Url = FileUtil.GetAbsUrl(url, this.SubPageBaseUrl).Replace("&amp;", "&");
                urlItem.Title = item.Groups[this.ListPageConfiguration.SnifferSubPageUrlItem.TitleGroupIndex].Value;

                this.SubPageUrlResults.Add(urlItem);
            }
            _done = true;
            return true;
        }
    }
}
