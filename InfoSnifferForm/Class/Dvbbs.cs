using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSnifferForm.Class
{
    public class Dvbbs : InfoSnifferForm.Class.BaseBbs
    {
        public Dvbbs(string bbsName) : base(bbsName) { }



        protected override UserInfo Register()
        {
            throw new NotImplementedException();
        }

        protected override System.Net.CookieContainer Login()
        {
            throw new NotImplementedException();
        }

        public override bool SubmitPost()
        {
            throw new NotImplementedException();
        }

        public override bool SubmitReply()
        {
            throw new NotImplementedException();
        }
    }
}
