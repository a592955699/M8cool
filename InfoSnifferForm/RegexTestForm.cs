using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using InfoSniffer;

namespace InfoSnifferForm
{
    public partial class RegexTestForm : Form
    {
        public RegexTestForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            string body = bodyTextBox.Text;
            string begin = beginTextBox.Text;
            string end = endTextBox.Text;
            MatchCollection matchColl;
            if (!string.IsNullOrEmpty(end))
            {
                if (!containCheckBox.Checked)
                    matchColl = Regex.Matches(body, string.Format("(?<={0}).*?(?={1})", begin, end), (RegexOptions)25);
                else
                    matchColl = Regex.Matches(body, string.Format("{0}.*?{1}", begin, end), (RegexOptions)25);
            }
            else
            {
                matchColl = Regex.Matches(body, begin, (RegexOptions)25);
            }

            countTextBox.Text = matchColl.Count.ToString();

            System.Text.StringBuilder sb = new StringBuilder();

            foreach (Match match in matchColl)
            {
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    sb.AppendFormat("{0}：{1}", i, match.Groups[i].Value);
                    sb.AppendLine();
                }
                sb.AppendLine();
                sb.AppendLine();

                sb.Append("-------------------------------------------------------------------------------------------");
                sb.AppendLine();
                sb.AppendLine();
            }
            resultTextBox.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encoding encoding = Encoding.UTF8;
            if (cmbEncoding.SelectedIndex == 0)
                encoding = Encoding.GetEncoding("GB2312");

            bodyTextBox.Text = FileUtil.GetPageText(tbxUrl.Text.Trim(), encoding);
        }

        private void RegexTestForm_Load(object sender, EventArgs e)
        {
            cmbEncoding.SelectedIndex = 0;
        }
    }
}