using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using InfoSniffer;

namespace InfoSnifferForm.Class
{
    public abstract class BaseBbs
    {
        UserInfo _user;
        string _bbsName;
        string _bbsDomain;
        CookieContainer _cookieContainer;

        /// <summary>
        /// 用户
        /// </summary>
        protected UserInfo User
        {
            get
            {
                if (_user == null)
                {
                    _user = this.Register();
                }
                return _user;
            }
        }

        /// <summary>
        /// 论坛名称
        /// </summary>
        protected string BbsName
        {
            get { return _bbsName; }
        }

        /// <summary>
        /// 论坛域名
        /// </summary>
        protected string BbsDomain
        {
            get
            {
                return _bbsDomain;
            }
        }

        /// <summary>
        /// CookieContainer
        /// </summary>
        protected CookieContainer CookieContainer
        {
            get {
                if (_cookieContainer == null)
                {
                    _cookieContainer = this.Login();
                }

                return _cookieContainer;
            }
        }

        /// <summary>
        /// 登录地址
        /// </summary>
        public string LoginUrl
        {
            get {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.LoginUrl);
            }
        }

        /// <summary>
        /// 登录提交地址
        /// </summary>
        public string LoginActionUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.LoginActionUrl);
            }
        }

        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.RegisterUrl);
            }
        }

        /// <summary>
        /// 注册提交地址
        /// </summary>
        public string RegisterActionUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.RegisterActionUrl);
            }
        }

        /// <summary>
        /// 发贴地址
        /// </summary>
        public string PostUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.PostUrl);
            }
        }

        /// <summary>
        /// 发贴提交地址
        /// </summary>
        public string PostActionUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.PostActionUrl);
            }
        }

        /// <summary>
        /// 回贴地址
        /// </summary>
        public string ReplyUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.ReplyUrl);
            }
        }

        /// <summary>
        /// 回贴提交地址
        /// </summary>
        public string ReplyActionUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.ReplyActionUrl);
            }
        }

        /// <summary>
        /// 版块地址
        /// </summary>
        public string BoardUrl
        {
            get
            {
                return string.Format("http://{0}/{1}", this.BbsDomain, this.Config.BoardUrl);
            }
        }

        /// <summary>
        /// 论坛版块ID
        /// </summary>
        public int[] BoardIds
        {
            get
            {
                return this.Config.BoardIds;
            }
        }

        /// <summary>
        /// 网站编码
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return this.Config.Encoding;
            }
        }

        public BaseBbs(string bbsName)
        {
            _bbsName = bbsName;
            _bbsDomain = "www.592zn.com";
        }

        public BbsConfig Config
        {
            get
            {
                BbsConfig config = new BbsConfig { 
                    BoardIds = new int[] { 3, 5 }, 
                    BoardUrl = "",
                    LoginUrl = "logging.php?action=login",
                    LoginActionUrl = "logging.php?action=login&loginsubmit=yes",
                    RegisterUrl = "register.php",
                    RegisterActionUrl = "register.php?regsubmit=yes",
                    PostUrl = "post.php?action=newthread&fid={0}",
                    PostActionUrl = "post.php?action=newthread&fid={0}&extra=&topicsubmit=yes", 
                    ReplyUrl = "" ,
                    ReplyActionUrl = "" 
                };
                return config;
            }
            set
            {
            }
        }

        protected virtual UserInfo GenerateUser()
        {
            UserInfo user = new UserInfo();
            user.UserName = StringUtil.GenerateRegionChineseCharacter(4);
            user.Password = StringUtil.GenerateRandomString(8);
            return user;
        }

        protected abstract UserInfo Register();
        protected abstract CookieContainer Login();
        public abstract bool SubmitPost();
        public abstract bool SubmitReply();

    }
}
