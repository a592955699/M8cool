using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSniffer
{
    public class UrlItem
    {
        string _url;
        string _title;

        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
    }
}
