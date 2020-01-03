using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using InfoSniffer;

namespace InfoSnifferForm
{
    public partial class LogoForm : Form
    {
        CookieContainer cookieContainer;

        public LogoForm()
        {
            InitializeComponent();
        }

        private void LogoForm_Load(object sender, EventArgs e)
        {
            cmbEncoding.SelectedIndex = 0;
        }

        private void btnGetVerifyCodeImage_Click(object sender, EventArgs e)
        {
            //FileUtil.RequestPage(txtUrl.Text, string.Empty, "POST", null, new byte[0], out cookieContainer);

            HttpWebResponse resp = FileUtil.RequestPage(txtVerifyCodeUrl.Text, string.Empty, "GET", null, new byte[0], out cookieContainer);
            picVerifyCode.Image = new Bitmap(resp.GetResponseStream());

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            Encoding encoding = Encoding.GetEncoding((string)cmbEncoding.SelectedItem);

            HttpWebResponse resp = FileUtil.RequestPage(txtLoginUrl.Text, string.Empty, "POST", cookieContainer, encoding, txtPostData.Text, out cookieContainer);

            txtPostData.Text = cookieContainer.GetCookieHeader(new Uri(txtLoginUrl.Text));

            CookieCollection cookieCollection = cookieContainer.GetCookies(new Uri(txtLoginUrl.Text));
            txtPostData.Text += "\r\n";
            foreach (Cookie cookie in cookieCollection)
            {
                txtPostData.Text += cookie.Name + "=" + cookie.Value + "; ";
            }

            WritePageBody(resp);

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            Encoding encoding = Encoding.GetEncoding((string)cmbEncoding.SelectedItem);

            HttpWebResponse resp = FileUtil.RequestPage(txtUrl.Text, null, "POST", txtUrl.Text, cookieContainer, encoding, txtPostData.Text, out cookieContainer);

            WritePageBody(resp);
        }

        void WritePageBody(HttpWebResponse resp)
        {
            Encoding encoding = Encoding.GetEncoding((string)cmbEncoding.SelectedItem);

            Stream stream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(stream, encoding);

            string fileBody = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            webBrowser1.DocumentText = fileBody;
        }
    }
}
