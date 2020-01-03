using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace InfoSnifferForm
{
    public partial class BbsSnifferForm : Form
    {
        SHDocVw.WebBrowser wb;

        public BbsSnifferForm()
        {
            InitializeComponent();

            wb = webBrowser.ActiveXInstance as SHDocVw.WebBrowser;
            wb.BeforeNavigate2 += new SHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(wb_BeforeNavigate2);
            wb.DocumentComplete += new SHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(wb_DocumentComplete);
        }

        void wb_DocumentComplete(object pDisp, ref object URL)
        {
            //textBox1.Text = wb.GetType().ToString();
        }

        void wb_BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            textBox1.Text += Headers + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strCookies = webBrowser.Document.Cookie;

            string[] arrCookie = Regex.Split(strCookies, "; ");

            CookieCollection cookieCollection = new CookieCollection();

            foreach (string cookie in arrCookie)
            {
                string[] arr = cookie.Split('=');
                cookieCollection.Add(new Cookie(arr[0], arr[1]));
            }

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri("http://www.592zn.com"), cookieCollection);

            CookieContainer outCookieContainer;

            HttpWebResponse response = InfoSniffer.FileUtil.RequestPage("http://www.592zn.com", null, "GET", "", cookieContainer, null, null, out outCookieContainer);

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("GB2312"));
            string fileBody = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            textBox1.Text = fileBody;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //StreamReader reader = new StreamReader(webBrowser1.DocumentStream, Encoding.GetEncoding("GB2312"));
            //string fileBody = reader.ReadToEnd();
            //textBox1.Text = fileBody;




        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(txtUrl.Text);
        }


        public string GetCookie()
        {
            Uri url = new Uri(txtUrl.Text);

            if (url == null)
                return null;
            string dir = url.Host;
            FileStream fr = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\index.dat", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] __dat = new byte[(int)fr.Length];
            fr.Read(__dat, 0, __dat.Length);
            fr.Close();
            fr.Dispose();
            string __datstream = Encoding.Default.GetString(__dat);
            int p1 = 0;
            p1 = __datstream.IndexOf("@" + dir, p1);
            if (p1 == -1)
                p1 = __datstream.IndexOf("@" + dir.Substring(dir.IndexOf('.') + 1));
            if (p1 == -1)
                return webBrowser.Document.Cookie;
            int p2 = __datstream.IndexOf(".txt", p1 + 1);
            p1 = __datstream.LastIndexOf('@', p2);
            string dm = __datstream.Substring(p1 + 1, p2 - p1 + 3).TrimStart('?');
            p1 = __datstream.LastIndexOf(":", p1);
            p2 = __datstream.IndexOf('@', ++p1);
            __datstream = string.Format("{0}@{1}", __datstream.Substring(p1, p2 - p1), dm);

            Dictionary<string, string> __cookiedicts = new Dictionary<string, string>();
            string __n;
            StringBuilder __cookies = new StringBuilder();
            __datstream = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\" + __datstream, Encoding.Default);
            p1 = -2;
            do
            {
                p1 += 2;
                p2 = __datstream.IndexOf('\n', p1);
                if (p2 == -1)
                    break;
                __n = __datstream.Substring(p1, p2 - p1);
                p1 = p2 + 1;
                p2 = __datstream.IndexOf('\n', p1);
                if (!__cookiedicts.ContainsKey(__n))
                    __cookiedicts.Add(__n, __datstream.Substring(p1, p2 - p1));
            }
            while ((p1 = __datstream.IndexOf("*\n", p1)) > -1);
            if (webBrowser.Document.Cookie != null && webBrowser.Document.Cookie.Length > 0)
            {
                foreach (string s in webBrowser.Document.Cookie.Split(';'))
                {
                    p1 = s.IndexOf('=');
                    if (p1 == -1)
                        continue;
                    __datstream = s.Substring(0, p1).TrimStart();
                    if (__cookiedicts.ContainsKey(__datstream))
                        __cookiedicts[__datstream] = s.Substring(p1 + 1);
                    else
                        __cookiedicts.Add(__datstream, s.Substring(p1 + 1));
                }
            }
            foreach (string s in __cookiedicts.Keys)
            {
                if (__cookies.Length > 0)
                    __cookies.Append(';');
                __cookies.Append(s);
                __cookies.Append('=');
                __cookies.Append(__cookiedicts[s]);
            }
            return __cookies.ToString();
        }
    }
}
