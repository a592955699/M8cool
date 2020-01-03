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

namespace InfoSnifferPlugin
{
    public class MobifocusDownloadPlugin : IPlugin
    {
        public void Receive(DataSet data, string file)
        {
            QwpDataParameter parm = new QwpDataParameter("CheckedTitle", DbType.Int32, 1);

            DataTable table = data.Tables[0];

            for (int i = table.Rows.Count - 1; i >= 0; i--)
            {
                try
                {
                    DataRow row = data.Tables[0].Rows[i];

                    BbsPostInfo post = new BbsPostInfo();
                    post.PublishToSite = true;

                    if (row["论坛ID"] == DBNull.Value)
                    {
                        //MessageBox.Show(string.Format("记录 {0} 的论坛ID为空", i));
                        continue;
                    }

                    string title;

                    if (row["标题"] == DBNull.Value || ((string)row["标题"]).Trim() == "")
                    {
                        title = "无标题";
                    }
                    else
                    {
                        title = (string)row["标题"];
                    }

                    post.BoardId = int.Parse((string)row["论坛ID"]);
                    post.Title = title;

                    string content = "";

                    if (table.Columns.Contains("内容") && row["内容"] != DBNull.Value)
                        content = (string)row["内容"];
                    else
                        content = post.Title;

                    //这里保存一部份内容作为摘要使用
                    post.Content = content;

                    //不再保存内容到数据库里，而是链接到xml文件，这样就可以大大的节省数据库空间了。
                    XmlNode node = post.PluginDatas.CreateElement("download");
                    XmlAttribute attaSoftname = post.PluginDatas.CreateAttribute("softname");
                    XmlAttribute attaLogourl = post.PluginDatas.CreateAttribute("logourl");

                    if (table.Columns.Contains("软件名称") && row["软件名称"] != DBNull.Value)
                        attaSoftname.Value = (string)row["软件名称"];
                    if (attaSoftname.Value == "")
                        attaSoftname.Value = title;

                    if (table.Columns.Contains("图标") && row["图标"] != DBNull.Value)
                        attaLogourl.Value = (string)row["图标"];

                    node.Attributes.Append(attaSoftname);
                    node.Attributes.Append(attaLogourl);

                    string os = "";
                    if (table.Columns.Contains("操作系统") && row["操作系统"] != DBNull.Value)
                        os = (string)row["操作系统"];

                    string resolution = "";
                    if (table.Columns.Contains("分辨率") && row["分辨率"] != DBNull.Value)
                        resolution = (string)row["分辨率"];

                    string mobile = "";
                    if (table.Columns.Contains("适用机型") && row["适用机型"] != DBNull.Value)
                        mobile = (string)row["适用机型"];

                    string url = "";
                    if (table.Columns.Contains("下载地址") && row["下载地址"] != DBNull.Value)
                        url = (string)row["下载地址"];


                    node.InnerXml = string.Format("<item os=\"{0}\" resolution=\"{1}\" mobile=\"{2}\" url=\"{3}\" />", os, resolution, mobile, url);
                    post.PluginDatas.DocumentElement.AppendChild(node);

                    if (table.Columns.Contains("贴子类型") && row["贴子类型"] != DBNull.Value)
                    {
                        post.PostType = (string)row["贴子类型"];
                    }


                    DateTime postdate = DateTime.MinValue;
                    if (table.Columns.Contains("日期") && row["日期"] != DBNull.Value)
                    {
                        string datetime = ((string)row["日期"]).Trim();

                        if (Regex.IsMatch(datetime, @"^\d+ minutes$"))
                        {
                            postdate = DateTime.Now.AddMinutes(-int.Parse(datetime.Replace(" minutes", "")));
                        }
                        else if (Regex.IsMatch(datetime, @"^\d+ hour$"))
                        {
                            postdate = DateTime.Now.AddHours(-int.Parse(datetime.Replace(" hour", "")));
                        }
                        else if (Regex.IsMatch(datetime, @"^\d+ day$"))
                        {
                            postdate = DateTime.Now.AddDays(-int.Parse(datetime.Replace(" day", "")));
                        }
                        else if (Regex.IsMatch(datetime, @"^\d+ month$"))
                        {
                            postdate = DateTime.Now.AddMonths(-int.Parse(datetime.Replace(" month", "")));
                        }
                        else if (Regex.IsMatch(datetime, @"^[一二三四五六七八九十]{1,2} \d{2}, \d{4}$"))
                        {
                            datetime = datetime.Replace("一", "1").Replace("二", "2").Replace("三", "3").Replace("四", "4")
                                .Replace("五", "5").Replace("六", "6").Replace("七", "7").Replace("八", "8").Replace("九", "9").Replace("十", "1");
                            postdate = DateTime.Parse(Regex.Replace(datetime, @"^([1-9]{1,2})( \d{2}, \d{4})$", "$1,$2"));
                        }
                        else
                        {
                            if (Regex.IsMatch(datetime, @"^\d{1,2}-\d{1,2}$"))
                            {
                                datetime = "2010-" + datetime;
                            }

                            DateTime.TryParse(datetime, out postdate);
                        }
                    }
                    if (postdate == DateTime.MinValue)
                        postdate = DateTime.Now;
                    post.Postdate = postdate;

                    if (table.Columns.Contains("附件"))
                    {
                        post.Attachments = new List<AttachmentInfo>();
                        string attachments = (string)row["附件"];
                        if (!string.IsNullOrEmpty(attachments))
                        {
                            string[] attaarr = attachments.Split(',');
                            int thumbIndex = -1;
                            for (int j = 0; j < attaarr.Length; j++)
                            {
                                if (attaarr[j].Length < 8)
                                    continue;
                                string atta = attaarr[j].Substring(attaarr[j].IndexOf("/", 8));
                                string attapath = Application.StartupPath + atta.Replace("/", "\\");
                                AttachmentInfo attaInfo = new AttachmentInfo();
                                attaInfo.Attachment = Path.GetFileName(attapath);
                                attaInfo.FileType = FileMimes.GetMimeType(attaInfo.Attachment);
                                attaInfo.Extension = Path.GetExtension(attaInfo.Attachment);
                                attaInfo.FileSize = -1;
                                if (attaInfo.Extension.EndsWith(".jpg") || attaInfo.Extension.EndsWith(".gif") || attaInfo.Extension.EndsWith(".png") || attaInfo.Extension.EndsWith(".bmp"))
                                {
                                    attaInfo.AttachmentType = AttachmentType.Image;
                                    if (thumbIndex == -1)
                                        thumbIndex = j;
                                }
                                else if (attaInfo.FileType == "application/x-shockwave-flash")
                                {
                                    attaInfo.AttachmentType = AttachmentType.Flash;
                                }
                                else
                                {
                                    attaInfo.AttachmentType = AttachmentType.File;
                                }
                                attaInfo.FileName = atta;
                                attaInfo.Description = "";
                                attaInfo.Postdate = post.Postdate;
                                attaInfo.Index = j;

                                post.Attachments.Add(attaInfo);
                            }
                            if (thumbIndex > -1)
                                post.ImageUrl = attaarr[thumbIndex];
                        }
                    }

                    if (table.Columns.Contains("作者") && row["作者"] != DBNull.Value)
                    {
                        post.UserName = (string)row["作者"];
                        post.UserName = post.UserName.Replace("孤岛", "很拽的猪");
                    }
                    else
                    {
                        post.UserName = "迪克羊仔";
                    }

                    Match match = Regex.Match(content, "(?<=<img[^>]*?src=[\"']?)(\\.|/|http)[^\"' >]*", RegexOptions.IgnoreCase);
                    if (match != null && match.Value != string.Empty)
                    {
                        post.ImageUrl = match.Value;
                    }

                    int postId = QwpDatabase.SaveRecord(post, QwpDataActions.Insert, "BbsPost_Insert", parm.DbParameter);
                    SaveDownload(post);
                    string postId1 = postId.ToString();
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(string.Format("记录 {0} 出错：{1}", table.Rows.Count - i, e.Message));
                }

            }

        }

        void SaveDownload(BbsPostInfo post)
        {
            XmlNodeList nodes = post.PluginDatas.SelectNodes("//download/item");
            string os = ",";
            for (int i = 0; i < nodes.Count; i++)
            {
                os += nodes[i].Attributes["os"].Value + ",";
            }
            string sql = "INSERT DownloadPlugin(SoftOS,PostId) VALUES (@SoftOS,@PostId)";

            QwpDataParameter[] parms = new QwpDataParameter[]{
                new QwpDataParameter("@SoftOS",DbType.String,os),
                new QwpDataParameter("@PostId",DbType.Int32,post.PostId)
            };
            QwpDatabase.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
    }
}
