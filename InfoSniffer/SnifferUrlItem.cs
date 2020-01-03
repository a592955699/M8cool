using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSniffer
{
    public class SnifferUrlItem
    {
        string _expression;
        int _urlGroupIndex;
        int _titleGroupIndex;
        string _urlFormat;

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        /// <summary>
        /// URL 所在组
        /// </summary>
        public int UrlGroupIndex
        {
            get { return _urlGroupIndex; }
            set { _urlGroupIndex = value; }
        }

        /// <summary>
        /// 标题所在组
        /// </summary>
        public int TitleGroupIndex
        {
            get { return _titleGroupIndex; }
            set { _titleGroupIndex = value; }
        }

        /// <summary>
        /// URl 格式
        /// </summary>
        public string UrlFormat
        {
            get { return _urlFormat; }
            set { _urlFormat = value; }
        }
    }
}
