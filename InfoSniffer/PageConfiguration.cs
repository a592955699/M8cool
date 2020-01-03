using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace InfoSniffer
{
	/// <summary>
	/// SnifferPage ��ժҪ˵����
	/// </summary>
	public class PageConfiguration
	{
		string _pageName;
		string _pageUrl;
		PageConfiguration _parent;
		PageType _pageType;
        Encoding _encoding = Encoding.GetEncoding("GB2312");
        string _endPageDetermineRegex;

        #region ����
        /// <summary>
        /// ҳ����
        /// </summary>
		public string PageName
		{
			get{return _pageName;}
			set{_pageName=value;}
		}

        /// <summary>
        /// ҳUrl
        /// </summary>
		public string PageUrl
		{
			get{return _pageUrl;}
			set{_pageUrl=value;}
		}

        /// <summary>
        /// ��ҳ
        /// </summary>
		public PageConfiguration Parent
		{
			get{return _parent;}
		}

        /// <summary>
        /// ҳ����
        /// </summary>
		public PageType PageType
		{
			get{return _pageType;}
			set{_pageType=value;}
        }

        /// <summary>
        /// ��ҳ����
        /// </summary>
        public Encoding Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        /// <summary>
        /// ���ҳ�ж�������ʽ����Ҫ�����ڷ�ҳ����������޾������
        /// </summary>
        public string EndPageDetermineRegex
        {
            get { return _endPageDetermineRegex; }
            set { _endPageDetermineRegex = value; }
        }
        #endregion

        #region ����
        public PageConfiguration() { }	
		
		public PageConfiguration(PageConfiguration parent)
		{
			_parent=parent;
        }
        #endregion
    }
}
