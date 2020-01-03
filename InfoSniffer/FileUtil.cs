using System;
using System.IO;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace InfoSniffer
{
    /// <summary>
    /// FileUtility 的摘要说明。
    /// </summary>
    public class FileUtil
    {

        /// <summary>
        /// 读取页面内容到文本
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetPageText(string url)
        {
            return GetPageText(url, System.Text.Encoding.GetEncoding("Gb2312"));
        }

        /// <summary>
        /// 读取页面内容到文本
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetPageText(string url, Encoding encoding)
        {
            //url = url.Replace("&amp;", "&");
            string fileBody = string.Empty;
            StreamReader reader = null;
            Stream stream = null;

            try
            {
                stream = GetPageStream(url);
                reader = new StreamReader(stream, encoding);
                fileBody = reader.ReadToEnd();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                reader.Close();
                stream.Close();
            }

            return fileBody;

        }

        /// <summary>
        /// 读取页面内容到流对象
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Stream GetPageStream(string url)
        {
            WebResponse response = RequestPage(url);
            Stream stream = null;
            if (response != null)
                stream = response.GetResponseStream();
            return stream;
        }

        public static Stream GetPageStream(string url, string referer)
        {
            CookieContainer cookieContainer;
            WebResponse response = RequestPage(url, null, "GET", referer, null, null, out cookieContainer);
            Stream stream = null;
            if (response != null)
                stream = response.GetResponseStream();
            return stream;
        }

        /// <summary>
        /// 请求页
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpWebResponse RequestPage(string url)
        {
            return RequestPage(url, string.Empty, "GET", Encoding.UTF8, string.Empty);
        }

        /// <summary>
        /// 请求网页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HttpWebResponse RequestPage(string url, string method)
        {
            string contentType = "";
            if (method == "POST")
                contentType = "application/x-www-form-urlencoded";
            return RequestPage(url, contentType, method, Encoding.UTF8, string.Empty);
        }

        /// <summary>
        /// 请求网页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HttpWebResponse RequestPage(string url, string contentType, string method)
        {
            return RequestPage(url, contentType, method, Encoding.UTF8, string.Empty);
        }

        /// <summary>
        /// 请求网页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="encoding"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static HttpWebResponse RequestPage(string url, string contentType, string method, Encoding encoding, string postData)
        {
            CookieContainer cookieContainer;
            return RequestPage(url, contentType, method, encoding, postData, out  cookieContainer);
        }

        /// <summary>
        /// 请求网页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="encoding"></param>
        /// <param name="postData"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static HttpWebResponse RequestPage(string url, string contentType, string method, Encoding encoding, string postData, out CookieContainer cookieContainer)
        {
            return RequestPage(url, contentType, method, null, encoding, postData, out  cookieContainer);
        }

        /// <summary>
        /// 请求网页
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="defaultCookieContainer"></param>
        /// <param name="encoding"></param>
        /// <param name="postData"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static HttpWebResponse RequestPage(string url, string contentType, string method, CookieContainer defaultCookieContainer, Encoding encoding, string postData, out CookieContainer cookieContainer)
        {
            byte[] byte1 = null;
            if (!string.IsNullOrEmpty(postData))
                byte1 = encoding.GetBytes(postData);
            return RequestPage(url, contentType, method, defaultCookieContainer, byte1, out  cookieContainer);
        }

        public static HttpWebResponse RequestPage(string url, string contentType, string method, CookieContainer defaultCookieContainer, byte[] postData, out CookieContainer cookieContainer)
        {
            return RequestPage(url, contentType, method, null, defaultCookieContainer, postData, out  cookieContainer);
        }

        public static HttpWebResponse RequestPage(string url, string contentType, string method, string referer, CookieContainer defaultCookieContainer, Encoding encoding, string postData, out CookieContainer cookieContainer)
        {
            byte[] byte1 = null;
            if (!string.IsNullOrEmpty(postData))
                byte1 = encoding.GetBytes(postData);
            return RequestPage(url, contentType, method, referer, defaultCookieContainer, byte1, out  cookieContainer);
        }

        public static HttpWebResponse RequestPage(string url, string contentType, string method, string referer, CookieContainer defaultCookieContainer, byte[] postData, out CookieContainer cookieContainer)
        {
            try
            {
                HttpWebRequest httpReq;
                httpReq = (HttpWebRequest)WebRequest.Create(url);
                httpReq.UseDefaultCredentials = true;
                httpReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322)";
                if (defaultCookieContainer != null)
                    httpReq.CookieContainer = defaultCookieContainer;
                else
                    httpReq.CookieContainer = new CookieContainer();
                httpReq.KeepAlive = false;
                httpReq.ContentLength = 0;
                httpReq.Referer = referer;

                if (!string.IsNullOrEmpty(contentType))
                    httpReq.ContentType = contentType;

                if (method == "POST" && string.IsNullOrEmpty(contentType))
                    httpReq.ContentType = "application/x-www-form-urlencoded";

                if (!string.IsNullOrEmpty(method))
                    httpReq.Method = method;
                if (postData != null && postData.Length > 0)
                {
                    httpReq.ContentLength = postData.Length;
                    Stream stream = httpReq.GetRequestStream();
                    stream.Write(postData, 0, postData.Length);
                    stream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse();
                cookieContainer = httpReq.CookieContainer;
                return response;
            }
            catch (System.Exception e)
            {
                cookieContainer = null;
                return null;
                //throw e;
            }
        }

        /// <summary>
        /// 保存文件到文件
        /// </summary>
        /// <param name="fileBody"></param>
        /// <param name="fileName"></param>
        public static void TextSaveToFile(string fileBody, string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(fileName);
                byte[] buffer;
                buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(fileBody);
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();

            }
            catch (IOException e)
            {
                throw e;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }


        /// <summary>
        /// 保存流对象到文件
        /// </summary>
        /// <param name="fileBody"></param>
        /// <param name="fileName"></param>
        public static void StreamSaveToFile(Stream fileBody, string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(fileName);
                byte[] buffer = new byte[1024];

                int l;
                do
                {
                    l = fileBody.Read(buffer, 0, buffer.Length);
                    if (l > 0) fs.Write(buffer, 0, l);
                }
                while (l > 0);
            }
            catch (IOException e)
            {
                throw e;
            }
            finally
            {
                if (fs != null) fs.Close();
                fileBody.Close();
            }
        }


        /// <summary>
        /// 将相对路径转换成绝对路径
        /// </summary>
        /// <param name="relUrl">相对路径</param>
        /// <param name="pageUrl">相对页路径</param>
        /// <returns></returns>
        public static string GetAbsUrl(string relUrl, string pageUrl)
        {
            string absUrl = string.Empty;
            string[] arrPageUrl;
            string urlPre = string.Empty;

            if (Regex.Matches(pageUrl, "/").Count <= 2)
            {
                pageUrl += "/";
            }
            else if (pageUrl.Substring(pageUrl.LastIndexOf("/")).IndexOf(".") > -1 || pageUrl.IndexOf("/?") > -1 || pageUrl.IndexOf("/#") > -1)
            {
                pageUrl = pageUrl.Substring(0, pageUrl.LastIndexOf("/") + 1);
            }
            else
            {
                if (!pageUrl.EndsWith("/"))
                {
                    pageUrl += "/";
                }
            }

            int urlPrePos = pageUrl.IndexOf("://");

            if (urlPrePos > -1)
            {
                urlPre = pageUrl.Substring(0, urlPrePos + 3);
                pageUrl = pageUrl.Substring(urlPrePos + 3);
            }

            arrPageUrl = pageUrl.Split("/".ToCharArray());

            if (relUrl.IndexOf("://") > -1)//如果是以 "http://"、"ftp://" 等开头的路径
            {
                return relUrl;
            }

            if (relUrl.StartsWith("/"))//如果以 "/" 开头的路径
            {
                return urlPre + arrPageUrl[0] + "/" + relUrl.Substring(1);
            }

            if (relUrl.StartsWith("./"))
            {
                return urlPre + pageUrl + relUrl.Substring(2);
            }

            if (relUrl.StartsWith("../"))
            {
                int level = arrPageUrl.Length - relUrl.Substring(0, relUrl.LastIndexOf("../") + 3).Split("/".ToCharArray()).Length - 1;
                if (level < 0) level = 0;
                for (int i = 0; i <= level; i++)
                {
                    if (absUrl == string.Empty)
                    {
                        absUrl = arrPageUrl[i];
                    }
                    else
                    {
                        absUrl += "/" + arrPageUrl[i];
                    }
                }
                return urlPre + absUrl + relUrl.Substring(relUrl.LastIndexOf("..") + 2);
            }

            return urlPre + pageUrl + relUrl;
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public static void DeleteFile(string file)
        {
            if (System.IO.File.Exists(file))
            {
                File.Delete(file);
            }
        }

        ///// <summary>
        ///// 导出到 Excel
        ///// </summary>
        ///// <param name="ds"></param>
        ///// <param name="strExcelFileName"></param>
        //public static void ExportToExcel(DataSet ds,string strExcelFileName)
        //{

        //    if (ds.Tables.Count==0 || strExcelFileName=="") return;

        //    Excel.Application excel= new Excel.Application();

        //    int rowIndex=1;
        //    int colIndex=0;

        //    excel.Application.Workbooks.Add(true);


        //    System.Data.DataTable table=ds.Tables[0] ;
        //    foreach(DataColumn col in table.Columns)
        //    {
        //        colIndex++;    
        //        excel.Cells[1,colIndex]=col.ColumnName;                
        //    }

        //    foreach(DataRow row in table.Rows)
        //    {
        //        rowIndex++;
        //        colIndex=0;
        //        foreach(DataColumn col in table.Columns)
        //        {
        //            colIndex++;
        //            excel.Cells[rowIndex,colIndex]=row[col.ColumnName].ToString();
        //        }
        //    }
        //    excel.Visible=false;    
        //    excel.ActiveWorkbook.SaveAs(strExcelFileName+".XLS",Excel.XlFileFormat.xlExcel9795,null,null,false,false,Excel.XlSaveAsAccessMode.xlNoChange,null,null,null,null,null);

        //    excel.Quit();
        //    excel=null;

        //    GC.Collect();//垃圾回收
        //}


        ///// <summary>
        ///// XML文件转Excel文件
        ///// </summary>
        ///// <param name="xmlFileName"></param>
        //public static void XmlFileToExcelFile(string xmlFileName)
        //{
        //    if (xmlFileName=="") return;

        //    Excel.Application excel= new Excel.Application();


        //    excel.Visible=false;    
        //    //excel.Workbooks.Open(@"D:\InfoSniffer\bin\Debug\"+xmlFileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
        //    excel.Workbooks.OpenXML(xmlFileName,Type.Missing,XlXmlLoadOption.xlXmlLoadImportToList);

        //    excel.ActiveWorkbook.SaveAs(xmlFileName.Replace(".xml",".xls"),Excel.XlFileFormat.xlExcel5,null,null,false,false,Excel.XlSaveAsAccessMode.xlNoChange,XlSaveConflictResolution.xlLocalSessionChanges,null,null,null,null);

        //    excel.Quit();
        //    excel=null;

        //    GC.Collect();//垃圾回收
        //}
    }
}
