using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSniffer
{
    public class RegexString
    {
        string _expression;
        int _valueGroupIndex;

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        /// <summary>
        /// 值所在组
        /// </summary>
        public int ValueGroupIndex
        {
            get { return _valueGroupIndex; }
            set { _valueGroupIndex = value; }
        }
    }
}
