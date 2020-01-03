using System;
using System.Collections;

namespace InfoSniffer
{
	/// <summary>
	/// InfoItem ��ժҪ˵����
	/// </summary>
	public class SnifferItem
	{
		string _itemName;
		string _defaultValue;
		bool _saveImage;
        string _imageUrlPath;
		bool _isUrl;
        bool _isClearHTML;
        string _saveImagesPath;
        bool _urlToAbs;
        string _separator;
        bool _clearAElement;
        bool _mutiPage;
        string _mutiPageSeparator;

        string _clearRegexString;
        RegexString _regexString;

        #region ����
        /// <summary>
        /// ������
        /// </summary>
		public string ItemName{
			get{return _itemName;}
			set{_itemName=value;}
		}

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
		public string DefaultValue{
			get{return _defaultValue;}
			set{_defaultValue=value;}
		}

        /// <summary>
        /// �Ƿ񱣴�ͼƬ
        /// </summary>
		public bool SaveImage
		{
			get{return _saveImage;}
			set{_saveImage=value;}
		}

        public string ImageUrlPath
        {
            get { return _imageUrlPath; }
            set { _imageUrlPath = value; }
        }

        /// <summary>
        /// �Ƿ��ҳ
        /// </summary>
        public bool MutiPage
        {
            get { return _mutiPage; }
            set { _mutiPage = value; }
        }

        /// <summary>
        /// ��ҳ�ָ���
        /// </summary>
        public string MutiPageSeparator
        {
            get { return _mutiPageSeparator; }
            set { _mutiPageSeparator = value; }
        }

        /// <summary>
        /// �ж���ɼ���ʱ�ķָ���
        /// </summary>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        /// <summary>
        /// �Ƿ���Url�������Url�����ת���ɾ���·��
        /// </summary>
		public bool IsUrl
		{
			get{return _isUrl;}
			set{_isUrl=value;}
		}

        /// <summary>
        /// �Ƿ���� HTML ����
        /// </summary>
        public bool IsClearHTML
        {
            get { return _isClearHTML; }
            set { _isClearHTML = value; }
        }

        /// <summary>
        /// ����ͼƬ·��
        /// </summary>
        public string SaveImagesPath
        {
            get { return _saveImagesPath; }
            set { _saveImagesPath = value; }
        }

        /// <summary>
        /// ��ɼ�����
        /// </summary>
        public RegexString RegexString
        {
            get { return _regexString; }
            set { _regexString = value; }
        }

        /// <summary>
        /// ��Urlת��������·��
        /// </summary>
        public bool UrlToAbs
        {
            get { return _urlToAbs; }
            set { _urlToAbs = value; }
        }

        /// <summary>
        /// ����AԪ��
        /// </summary>
        public bool ClearAElement
        {
            get { return _clearAElement; }
            set { _clearAElement = value; }
        }

        /// <summary>
        /// Ҫ��������ݵ�������ʽ
        /// </summary>
        public string ClearRegexString
        {
            get { return _clearRegexString; }
            set { _clearRegexString = value; }
        }
        #endregion

        #region ����
        public SnifferItem(){}

        public SnifferItem(string itemName,RegexString regexString, string defaultValue, bool saveImage, string saveImagesPath, bool isClearHTML)
        {
            this._itemName = itemName;
            this._regexString = regexString;
            this._defaultValue = defaultValue;
            this._saveImage = saveImage;
            this._saveImagesPath = saveImagesPath;
            this._isClearHTML = isClearHTML;
        }
        #endregion
    }
}
