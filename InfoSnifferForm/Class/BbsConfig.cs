using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSnifferForm.Class
{
    public class BbsConfig
    {
        public string LoginUrl
        {
            get;
            set;
        }

        public string LoginActionUrl
        {
            get;
            set;
        }

        public string RegisterUrl
        {
            get;
            set;
        }

        public string RegisterActionUrl
        {
            get;
            set;
        }

        public string PostUrl
        {
            get;
            set;
        }

        public string PostActionUrl
        {
            get;
            set;
        }

        public string ReplyUrl
        {
            get;
            set;
        }

        public string ReplyActionUrl
        {
            get;
            set;
        }

        public string BoardUrl
        {
            get;
            set;
        }

        public int[] BoardIds
        {
            get;
            set;
        }

        public Encoding Encoding
        {
            get;
            set;
        }
    }
}
