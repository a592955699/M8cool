using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace InfoSniffer
{
    public abstract class PageBase
    {
        PageConfiguration _configuration;
        public string _pageUrl;
        public string _pageName;
        public PageBase _parent;
        string _pageBody;
        string _subPageBaseUrl;

        #region 属性
        /// <summary>
        /// 是否已经完成
        /// </summary>
        public abstract bool Done
        {
            get;
        }

        /// <summary>
        /// 采集是否成功
        /// </summary>
        public abstract bool Succeed
        {
            get;
        }

        /// <summary>
        /// 页的配置
        /// </summary>
        public PageConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        /// <summary>
        /// 页名称
        /// </summary>
        public string PageName
        {
            get
            {
                if (_pageName == null)
                    return this.Configuration.PageName;
                return _pageName;

            }
            set
            {
                _pageName = value;
            }
        }

        /// <summary>
        /// 页Url
        /// </summary>
        public string PageUrl
        {
            get {
                if (_pageUrl == null)
                    return this.Configuration.PageUrl;
                return _pageUrl;
            }
            set
            {
                _pageUrl = value;
            }
        }

        /// <summary>
        /// 页类型
        /// </summary>
        public PageType PageType
        {
            get { return this.PageType; }
        }

        /// <summary>
        /// 父页
        /// </summary>
        public PageBase Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// 采集回来的页内容
        /// </summary>
        public virtual string PageBody
        {
            get
            {
                if (_pageBody == null)
                {
                    try
                    {
                        _pageBody = FileUtil.GetPageText(this.PageUrl,this.Configuration.Encoding);
                    }
                    catch
                    {
                        _pageBody = string.Empty;
                    }
                }
                return _pageBody;
            }
        }

        /// <summary>
        /// 子页基本URL
        /// </summary>
        public string SubPageBaseUrl
        {
            get
            {
                if (_subPageBaseUrl == null && !string.IsNullOrEmpty(this.PageBody))
                {
                    Match match = Regex.Match(this.PageBody, "(?<=<base[^>]*?href=[\"']?)[\\w/][^\"' >]*");
                    if (match != null)
                    {
                        _subPageBaseUrl = match.Value;
                    }
                }

                if (string.IsNullOrEmpty(_subPageBaseUrl))
                {
                    _subPageBaseUrl = this.PageUrl;
                }
                return _subPageBaseUrl;
            }
        }

        public bool Upload
        {
            get
            {
                return ConfigurationManager.AppSettings["upload"] == "true";
            }
        }
        public string UploadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["uploadurl"];
            }
        }
        #endregion


        /// <summary>
        /// 采集方法
        /// </summary>
        public abstract bool Sniffer();

        public PageBase()
        { }

        public PageBase(PageConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public PageBase(PageBase parent, PageConfiguration configuration)
        {
            this._parent = parent;
            this._configuration = configuration;
        }

        #region 实用方法
        ///// <summary>
        ///// 正则表达式，返回匹配项集合
        ///// </summary>
        //public static MatchCollection RegexMatchs(string input, string expression)
        //{
        //    if (input == null)
        //        return null;

        //    return Regex.Matches(input, expression, (RegexOptions)25);
        //}

        ///// <summary>
        ///// 正则表达式，返回匹配值
        ///// </summary>
        //public static string RegexValue(string input, RegexString regexString)
        //{
        //    MatchCollection matchColl = RegexMatchs(input, regexString.Expression);
        //    if (matchColl == null || matchColl.Count == 0)
        //        return string.Empty;
        //    return matchColl[0].Groups[regexString.ValueGroupIndex].Value;
        //}


        /// <summary>
        /// 将正则表达式的字符转义
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertRegexChar(string input)
        {
            //RegexOptions regOptions = (RegexOptions)25;
            //input = Regex.Replace(input, @"\\", @"\\", regOptions);
            //input = Regex.Replace(input, @"\$", @"\$", regOptions);
            //input = Regex.Replace(input, @"\(", @"\(", regOptions);
            //input = Regex.Replace(input, @"\)", @"\)", regOptions);
            //input = Regex.Replace(input, @"\*", @"\*", regOptions);
            //input = Regex.Replace(input, @"\+", @"\+", regOptions);
            //input = Regex.Replace(input, @"\.", @"\.", regOptions);
            //input = Regex.Replace(input, @"\[", @"\[", regOptions);
            //input = Regex.Replace(input, @"\]", @"\]", regOptions);
            //input = Regex.Replace(input, @"\?", @"\?", regOptions);
            //input = Regex.Replace(input, @"\}", @"\}", regOptions);
            //input = Regex.Replace(input, @"\{", @"\{", regOptions);
            //input = Regex.Replace(input, @"\|", @"\|", regOptions);
            //input = Regex.Replace(input, "\r\n", ".*?", regOptions);
            //input = Regex.Replace(input, "<变量>", ".*?", regOptions);

            return input;
        }

        /// <summary>
        /// 清除HTML
        /// </summary>
        public static string ClearHTML(string input)
        {
            RegexOptions regOptions = (RegexOptions)25;
            input = Regex.Replace(input, "<br[\\s/]*>", "\r\n", regOptions);
            input = Regex.Replace(input, "&nbsp;", " ", regOptions);
            input = Regex.Replace(input, "<.*?>", "", regOptions);
            return input;
        }
        #endregion
    }
}
