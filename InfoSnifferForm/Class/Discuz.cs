using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using InfoSniffer;

namespace InfoSnifferForm.Class
{
    public class Discuz : BaseBbs
    {
        public Discuz(string bbsName) : base(bbsName) { }

        protected override UserInfo Register()
        {
            string body = FileUtil.GetPageText(this.LoginUrl, this.Encoding);

            UserInfo user = this.GenerateUser();
            CookieContainer cookieContainer;

            string text1 = string.Format("username={0}&password={1}", user.UserName, user.Password);

            FileUtil.RequestPage(this.RegisterActionUrl, null, "POST", this.BoardUrl, null, this.Encoding, text1, out cookieContainer);
            return user;

        }

        protected override CookieContainer Login()
        {
            CookieContainer cookieContainer;

            string text1 = string.Format("username={0}&password={1}", this.User.UserName, this.User.Password);

            FileUtil.RequestPage(this.LoginActionUrl, null, "POST", this.BoardUrl, null, this.Encoding, text1, out cookieContainer);
            return cookieContainer;
        }

        public override bool SubmitPost()
        {
            CookieContainer cookieContainer;

            string text1 = string.Format("username={0}&password={1}", this.User.UserName, this.User.Password);

            FileUtil.RequestPage(this.PostActionUrl, null, "POST", this.BoardUrl, this.CookieContainer, this.Encoding, text1, out cookieContainer);

            return true;
        }

        public override bool SubmitReply()
        {
            return true;
        }
    }
}
