using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace InfoSniffer
{
	/// <summary>
	/// SnifferPage 的摘要说明。
	/// </summary>
	public class PageConfiguration
	{
		string _pageName;
		string _pageUrl;
		PageConfiguration _parent;
		PageType _pageType;
        Encoding _encoding = Encoding.GetEncoding("GB2312");
        string _endPageDetermineRegex;

        #region 属性
        /// <summary>
        /// 页名称
        /// </summary>
		public string PageName
		{
			get{return _pageName;}
			set{_pageName=value;}
		}

        /// <summary>
        /// 页Url
        /// </summary>
		public string PageUrl
		{
			get{return _pageUrl;}
			set{_pageUrl=value;}
		}

        /// <summary>
        /// 父页
        /// </summary>
		public PageConfiguration Parent
		{
			get{return _parent;}
		}

        /// <summary>
        /// 页类型
        /// </summary>
		public PageType PageType
		{
			get{return _pageType;}
			set{_pageType=value;}
        }

        /// <summary>
        /// 网页编码
        /// </summary>
        public Encoding Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        /// <summary>
        /// 最后页判断正则表达式，主要是用于翻页到最后，无穷无尽的情况
        /// </summary>
        public string EndPageDetermineRegex
        {
            get { return _endPageDetermineRegex; }
            set { _endPageDetermineRegex = value; }
        }
        #endregion

        #region 构造
        public PageConfiguration() { }	
		
		public PageConfiguration(PageConfiguration parent)
		{
			_parent=parent;
        }
        #endregion
    }
}
