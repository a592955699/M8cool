using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfoSniffer;
using System.Threading;

namespace InfoSniffer
{
    public class SnifferContext
    {
        public List<ListPage> FirstPages;

        int _startPageIndex = 1;
        int _snifferPageCount = 1;
        bool _multiThread;

        public int StartPageIndex
        {
            get { return _startPageIndex; }
            set { _startPageIndex = value; }
        }
        public int SnifferPageCount
        {
            get { return _snifferPageCount; }
            set { _snifferPageCount = value; }
        }

        public bool MultiThread
        {
            get { return _multiThread; }
            set { _multiThread = value; }
        }

        public SnifferContext()
        {
            FirstPages = new List<ListPage>();
        }

        public int GetStartPageIndex(ListPage listPage)
        {
            return this.StartPageIndex;
        }

        public int GetSnifferPageCount(ListPage listPage)
        {
            return this.SnifferPageCount;
        }
    }
}
