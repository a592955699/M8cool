using System;
using System.Collections;

namespace InfoSniffer
{
	/// <summary>
	/// InfoItem 的摘要说明。
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

        #region 属性
        /// <summary>
        /// 项名称
        /// </summary>
		public string ItemName{
			get{return _itemName;}
			set{_itemName=value;}
		}

        /// <summary>
        /// 默认值
        /// </summary>
		public string DefaultValue{
			get{return _defaultValue;}
			set{_defaultValue=value;}
		}

        /// <summary>
        /// 是否保存图片
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
        /// 是否分页
        /// </summary>
        public bool MutiPage
        {
            get { return _mutiPage; }
            set { _mutiPage = value; }
        }

        /// <summary>
        /// 分页分隔符
        /// </summary>
        public string MutiPageSeparator
        {
            get { return _mutiPageSeparator; }
            set { _mutiPageSeparator = value; }
        }

        /// <summary>
        /// 有多个采集项时的分隔符
        /// </summary>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        /// <summary>
        /// 是否是Url，如果是Url，则会转换成绝对路径
        /// </summary>
		public bool IsUrl
		{
			get{return _isUrl;}
			set{_isUrl=value;}
		}

        /// <summary>
        /// 是否清除 HTML 代码
        /// </summary>
        public bool IsClearHTML
        {
            get { return _isClearHTML; }
            set { _isClearHTML = value; }
        }

        /// <summary>
        /// 保存图片路径
        /// </summary>
        public string SaveImagesPath
        {
            get { return _saveImagesPath; }
            set { _saveImagesPath = value; }
        }

        /// <summary>
        /// 项采集正则
        /// </summary>
        public RegexString RegexString
        {
            get { return _regexString; }
            set { _regexString = value; }
        }

        /// <summary>
        /// 将Url转换到绝对路径
        /// </summary>
        public bool UrlToAbs
        {
            get { return _urlToAbs; }
            set { _urlToAbs = value; }
        }

        /// <summary>
        /// 清理A元素
        /// </summary>
        public bool ClearAElement
        {
            get { return _clearAElement; }
            set { _clearAElement = value; }
        }

        /// <summary>
        /// 要清理的内容的正则表达式
        /// </summary>
        public string ClearRegexString
        {
            get { return _clearRegexString; }
            set { _clearRegexString = value; }
        }
        #endregion

        #region 构造
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
