using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using M8cool.Lib.Bbs;
using QuickWeb.Data;
using System.Windows.Forms;
using InfoSniffer;
using System.Text.RegularExpressions;
using System.Xml;
using QuickWeb.Utility;
using System.IO;
using System.Web;
using System.Net;
using System.Collections.Specialized;

namespace InfoSnifferPlugin
{
    public class GooodoPlugin : IPlugin
    {
        public void Receive(DataSet data,string file)
        {
            WebClient wc = new WebClient();

            DataTable table = data.Tables[0];
            DataRow row;
            NameValueCollection postData;
            string title;
            string content;
            DateTime create_time;

            for (int i = table.Rows.Count - 1; i >= 0; i--)
            {
                try
                {
                    row = data.Tables[0].Rows[i];
                    postData=new NameValueCollection();

                    if (row["标题"] == DBNull.Value || ((string)row["标题"]).Trim() == "")
                        title = "无标题";
                    else
                        title = (string)row["标题"];
                    postData.Add("title", title);

                    if (table.Columns.Contains("内容") && row["内容"] != DBNull.Value)
                        content = (string)row["内容"];
                    else
                        content = title;
                    postData.Add("content", content);

                    postData.Add("user_name", (string)row["作者"]);
                    postData.Add("fid", (string)row["论坛ID"]);

                    DateTime.TryParse(((string)row["日期"]).Trim(), out create_time);
                    if (create_time == DateTime.MinValue)
                        create_time = DateTime.Now;
                    postData.Add("create_time", create_time.ToString("yyyy-MM-dd hh:mm:ss"));


                    wc.UploadValues("http://www.gooodo.com/import.php", postData);
 
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(string.Format("记录 {0} 出错：{1}", table.Rows.Count - i, e.Message));
                }

            }

        }
    }
}
