using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using System.IO;

namespace InfoSniffer
{
    public class LogManager
    {
        static object obj = new object();
        public static string LogFile;

        LogManager()
        {
            LogFile = HttpRuntime.AppDomainAppPath + @"App_Data\Snifferlog.xml";
        }

        public static void WriteLog(string innerXML)
        {
            lock (obj)
            {
                try
                {
                    System.IO.File.AppendAllText(HttpRuntime.AppDomainAppPath + @"App_Data\Snifferlog.xml", innerXML + "\r\n");
                }
                catch { }
            }
        }
        public static XmlDocument ReadLog()
        {
            lock (obj)
            {
                StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + @"App_Data\Snifferlog.xml");
                string xml = sr.ReadToEnd();
                sr.Close();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(string.Format("<root>{0}</root>", xml));
                return doc;
            }
        }
    }
}
