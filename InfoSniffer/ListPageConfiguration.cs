using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace InfoSniffer
{
	/// <summary>
	/// SnifferPage 的摘要说明。
	/// </summary>
	public class ListPageConfiguration : PageConfiguration
	{
		string _pageQuery;
        string _replacePageQuery;
        string _pageIndexFormat;
        int _pageIndexSeed = 1;
        int _pageIndexStep = 1;
        PageMethod _pageMethod = PageMethod.Get;
		bool _for;
        int _maxForCount = 4;
		int _currentForCount;

        SnifferUrlItem _snifferSubPageUrlItem;
        PageConfiguration _subPageConfiguration;


        #region 属性
        /// <summary>
        /// 翻页字符正则
        /// </summary>
		public string PageQuery
		{
			get{return _pageQuery;}
			set{_pageQuery=value;}
		}

        /// <summary>
        /// 翻页替换正则
        /// </summary>
        public string ReplacePageQuery
        {
            get { return _replacePageQuery; }
            set { _replacePageQuery = value; }
        }

        /// <summary>
        /// 翻页页码格式
        /// </summary>
        public string PageIndexFormat
        {
            get { return _pageIndexFormat; }
            set { _pageIndexFormat = value; }
        }

        /// <summary>
        /// 翻页页码种子，意思是讲比如页码是从0开始的还是从1开始的。
        /// </summary>
        public int PageIndexSeed
        {
            get { return _pageIndexSeed; }
            set { _pageIndexSeed = value; }
        }

        /// <summary>
        /// 翻页页码级进数，比如是当前页是1，下一页是2，则PageIndexStep为1，如果当前页是0，下页是10，则PageIndexStep为10
        /// </summary>
        public int PageIndexStep
        {
            get { return _pageIndexStep; }
            set { _pageIndexStep = value; }
        }

        /// <summary>
        /// 是POST翻页，还是GET翻页
        /// </summary>
        public PageMethod PageMethod
        {
            get { return _pageMethod; }
            set { _pageMethod = value; }
        }

        /// <summary>
        /// 如果本页找不到，是否以本页作为子页再继承测试，主要应用于，多层子分类，子分类的页结构又是一样的。
        /// </summary>
		public bool For
		{
			get{return _for;}
			set{_for=value;}
		}

        /// <summary>
        /// 当For为真时，最大循环次数
        /// </summary>
		public int MaxForCount
		{
			get{return _maxForCount;}
			set{_maxForCount=value;}
		}

        /// <summary>
        /// 当前循环记录
        /// </summary>
		public int CurrentForCount
		{
			get{return _currentForCount;}
			set{_currentForCount=value;}
        }

        /// <summary>
        /// 子页配置
        /// </summary>
        public virtual PageConfiguration SubPageConfiguration
        {
            get { return _subPageConfiguration; }
            set { _subPageConfiguration = value; }
        }


        /// <summary>
        /// 采集页Url项
        /// </summary>
        public SnifferUrlItem SnifferSubPageUrlItem
        {
            get { return _snifferSubPageUrlItem; }
            set { _snifferSubPageUrlItem = value; }
        }
        #endregion

        public ListPageConfiguration() : base() { }

        public ListPageConfiguration(PageConfiguration parent)
            : base(parent)
        {
        }
    }
}
