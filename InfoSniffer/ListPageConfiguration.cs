using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace InfoSniffer
{
	/// <summary>
	/// SnifferPage ��ժҪ˵����
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


        #region ����
        /// <summary>
        /// ��ҳ�ַ�����
        /// </summary>
		public string PageQuery
		{
			get{return _pageQuery;}
			set{_pageQuery=value;}
		}

        /// <summary>
        /// ��ҳ�滻����
        /// </summary>
        public string ReplacePageQuery
        {
            get { return _replacePageQuery; }
            set { _replacePageQuery = value; }
        }

        /// <summary>
        /// ��ҳҳ���ʽ
        /// </summary>
        public string PageIndexFormat
        {
            get { return _pageIndexFormat; }
            set { _pageIndexFormat = value; }
        }

        /// <summary>
        /// ��ҳҳ�����ӣ���˼�ǽ�����ҳ���Ǵ�0��ʼ�Ļ��Ǵ�1��ʼ�ġ�
        /// </summary>
        public int PageIndexSeed
        {
            get { return _pageIndexSeed; }
            set { _pageIndexSeed = value; }
        }

        /// <summary>
        /// ��ҳҳ�뼶�����������ǵ�ǰҳ��1����һҳ��2����PageIndexStepΪ1�������ǰҳ��0����ҳ��10����PageIndexStepΪ10
        /// </summary>
        public int PageIndexStep
        {
            get { return _pageIndexStep; }
            set { _pageIndexStep = value; }
        }

        /// <summary>
        /// ��POST��ҳ������GET��ҳ
        /// </summary>
        public PageMethod PageMethod
        {
            get { return _pageMethod; }
            set { _pageMethod = value; }
        }

        /// <summary>
        /// �����ҳ�Ҳ������Ƿ��Ա�ҳ��Ϊ��ҳ�ټ̳в��ԣ���ҪӦ���ڣ�����ӷ��࣬�ӷ����ҳ�ṹ����һ���ġ�
        /// </summary>
		public bool For
		{
			get{return _for;}
			set{_for=value;}
		}

        /// <summary>
        /// ��ForΪ��ʱ�����ѭ������
        /// </summary>
		public int MaxForCount
		{
			get{return _maxForCount;}
			set{_maxForCount=value;}
		}

        /// <summary>
        /// ��ǰѭ����¼
        /// </summary>
		public int CurrentForCount
		{
			get{return _currentForCount;}
			set{_currentForCount=value;}
        }

        /// <summary>
        /// ��ҳ����
        /// </summary>
        public virtual PageConfiguration SubPageConfiguration
        {
            get { return _subPageConfiguration; }
            set { _subPageConfiguration = value; }
        }


        /// <summary>
        /// �ɼ�ҳUrl��
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
