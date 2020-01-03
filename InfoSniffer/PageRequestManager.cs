using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.IO;

namespace InfoSniffer
{
    public class PageRequestManager
    {
        public static string AppDataPath
        {
            get {
                return HttpRuntime.AppDomainAppPath + @"App_Data\InfoSniffer\";
            }
        }

        /// <summary>
        /// 读取所有的配置文件名
        /// </summary>
        /// <returns></returns>
        public static string[] GetConfigs()
        {

            string[] files = System.IO.Directory.GetFiles(AppDataPath, "*.xml");

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                files[i] = Path.GetFileNameWithoutExtension(file);
            }

            return files;
        }

        /// <summary>
        /// 读取根页
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<ListPage> GetAllFirstPages(string fileName)
        {
            return GetAllFirstPages(fileName, null);
        }

        /// <summary>
        /// 读取根页重载
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPageName"></param>
        /// <returns></returns>
        public static List<ListPage> GetAllFirstPages(string fileName, string rootPageName)
        {
            SnifferConfig.OpenSnfFile(string.Format(AppDataPath + "{0}.xml", fileName));
            RootPageConfiguration rootPageConf = SnifferConfig.GetRootPageConfiguration(rootPageName);

            if (rootPageConf == null)
                return null;

            ListPage rootPage = new ListPage((ListPageConfiguration)rootPageConf);
            List<ListPage> allFirstPages = new List<ListPage>();

            if (rootPageConf.IsSniffer)
            {
                rootPage.Sniffer();

                if (!rootPage.Done || rootPage.SubPageUrlResults.Count == 0)
                {
                    //采集不到
                }

                foreach (UrlItem urlItem in rootPage.SubPageUrlResults)
                {
                    ListPage page = new ListPage(rootPage, (ListPageConfiguration)rootPage.ListPageConfiguration.SubPageConfiguration);
                    page.PageName = urlItem.Title;
                    page.PageUrl = urlItem.Url;
                    allFirstPages.Add(page);
                }

            }
            else
            {
                foreach (ListPageConfiguration firstPageConfi in rootPageConf.SubPageConfigurations)
                {
                    allFirstPages.Add(new ListPage(rootPage, firstPageConfi));
                }
            }

            return allFirstPages;
        }

        /// <summary>
        /// 读取列表页
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPageName"></param>
        /// <param name="firstIndex"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static ListPage GetListPage(string fileName, int firstIndex, int pageIndex)
        {
            return GetListPage(fileName, null, firstIndex, pageIndex);
        }

        /// <summary>
        /// 读取列表页重载
        /// </summary>
        public static ListPage GetListPage(string fileName, string rootPageName, int firstIndex, int pageIndex)
        {
            List<ListPage> allFirstPages = GetAllFirstPages(fileName, rootPageName);

            ListPage firstPage = allFirstPages[firstIndex];

            //列表页的页码大于1，则要替换页码
            if (firstPage.ListPageConfiguration.SubPageConfiguration.PageType == PageType.DetailPage && pageIndex > 1)
            {

                if (firstPage.ListPageConfiguration.PageMethod == PageMethod.Get)
                {
                    ReplacePageIndex(firstPage, pageIndex);
                }
                else
                {
                    firstPage.PageQuery = string.Format(firstPage.PageQuery, pageIndex);
                }

            }

            firstPage.Sniffer();

            return firstPage;
        }

        /// <summary>
        /// 读取详细页
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="firstIndex"></param>
        /// <param name="pageIndex"></param>
        /// <param name="urlIndex"></param>
        /// <returns></returns>
        public static DetailPage GetDetailPage(string fileName, int firstIndex, int pageIndex, int urlIndex)
        {
            return GetDetailPage(fileName, null, firstIndex, pageIndex, urlIndex);
        }

        /// <summary>
        /// 读取详细页重载
        /// </summary>
        /// <returns></returns>
        public static DetailPage GetDetailPage(string fileName, string rootPageName, int firstIndex, int pageIndex, int urlIndex)
        {
            ListPage firstPage = GetListPage(fileName, rootPageName, firstIndex, pageIndex);

            UrlItem urlItem = firstPage.SubPageUrlResults[urlIndex];

            DetailPageConfiguration detailPageConf = (DetailPageConfiguration)firstPage.ListPageConfiguration.SubPageConfiguration;
            DetailPage detailPage = new DetailPage(firstPage, detailPageConf);
            detailPage.PageIndex = detailPageConf.PageStartIndex;
            detailPage.PageName = urlItem.Title;
            detailPage.PageUrl = urlItem.Url;
            detailPage.Sniffer();

            return detailPage;
        }

        /// <summary>
        /// 替换页码
        /// </summary>
        /// <param name="listPage"></param>
        /// <param name="pageIndex"></param>
        private static void ReplacePageIndex(ListPage listPage, int pageIndex)
        {
            string stringPageIndex;
            if (!string.IsNullOrEmpty(listPage.ListPageConfiguration.PageIndexFormat))
                stringPageIndex = pageIndex.ToString(listPage.ListPageConfiguration.PageIndexFormat);
            else
                stringPageIndex = pageIndex.ToString();

            string pageQuery = string.Format(listPage.ListPageConfiguration.ReplacePageQuery, stringPageIndex);
            listPage.PageUrl = Regex.Replace(listPage.PageUrl, listPage.ListPageConfiguration.PageQuery, pageQuery);
        }
    }
}
