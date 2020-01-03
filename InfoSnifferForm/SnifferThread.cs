using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections.Specialized;
using InfoSniffer;
using System.Configuration;
using InfoSnifferForm.Class;

namespace InfoSnifferForm
{

    /// <summary>
    /// �߳���
    /// </summary>
    public class SnifferThread
    {
        public delegate void PageIndexChangeEventHandler(string currentPageUrl);
        public delegate void DetailPageParseDoneEventHandler(DetailPage detailPage);
        public delegate void ListPageParseDoneEventHandler(ListPage listPage);
        public delegate void FirstPageParseDoneEventHandler(ListPage firstPage);
        public delegate void CategoryParseDoneEventHandler(ListPage listPage);

        public event PageIndexChangeEventHandler PageIndexChange;
        public event DetailPageParseDoneEventHandler DetailPageParseDone;
        public event ListPageParseDoneEventHandler ListPageParseDone;
        public event FirstPageParseDoneEventHandler FirstPageParseDone;
        public event CategoryParseDoneEventHandler CategoryParseDone;

        DataSet _data;
        string PagePath;

        SnifferForm SnifferForm;
        int _threadIndex;


        #region ����
        /// <summary>
        /// �߳�����
        /// </summary>
        public int ThreadIndex
        {
            get { return _threadIndex; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public DataSet Data
        {
            get
            {
                if (_data == null)
                    _data = new DataSet();
                return _data;
            }
        }

        public bool Upload
        {
            get {
                return ConfigurationManager.AppSettings["upload"] == "true";
            }
        }
        public string UploadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["uploadurl"];
            }
        }
        #endregion



        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="sender"></param>
        public SnifferThread(SnifferForm snifferForm, int threadIndex)
        {
            SnifferForm = snifferForm;
            _threadIndex = threadIndex;

            this.PageIndexChange += new PageIndexChangeEventHandler(SnifferThread_PageIndexChange);
            this.ListPageParseDone += new ListPageParseDoneEventHandler(SnifferThread_ListPageParseDone);
            this.DetailPageParseDone += new DetailPageParseDoneEventHandler(SnifferThread_DetailPageParseDone);
            this.FirstPageParseDone += new FirstPageParseDoneEventHandler(SnifferThread_FirstPageParseDone);
            this.CategoryParseDone += new CategoryParseDoneEventHandler(SnifferThread_CategoryParseDone);
        }

        #region �¼��������
        /// <summary>
        /// һ����ϸҳ�������
        /// </summary>
        /// <param name="detailPage"></param>
        void SnifferThread_DetailPageParseDone(DetailPage detailPage)
        {
            if (Data.Tables.Count == 0)
            {
                //���û�б����ȴ�����ṹ
                Data.Tables.Add(detailPage.DetailPageConfiguration.CreateDataTable());
            }

            DataTable table = Data.Tables[0];
            DataRow row = table.NewRow();

            bool hasData = false;
            foreach (DataColumn col in table.Columns)
            {
                if (col.ColumnName != "ID" && !string.IsNullOrEmpty(row[col].ToString()))
                {
                    hasData = true;
                    break;
                }
            }

            hasData = true;
            if (hasData)
            {
                AddValueToRow(row, detailPage);
                table.Rows.Add(row);
            }
            //�ı���ɼ�¼����ʾ
            this.SnifferForm.SetCountStep(this.ThreadIndex);
        }

        void AddValueToRow(DataRow row, DetailPage page)
        {
            foreach (string key in page.ResultItems.Keys)
            {
                row[key] = page.ResultItems[key];
            }

            //foreach (DetailPage p in page.SubPages)
            //{
            //    AddValueToRow(row, p);
            //}
        }


        /// <summary>
        /// һ���б�ҳ�ɼ����
        /// </summary>
        /// <param name="listPage"></param>
        void SnifferThread_ListPageParseDone(ListPage listPage)
        {

            string dir = listPage.SavePath;
            string dirAndFileName = listPage.SavePathAndFileName;

            if (this.Upload)
            {
                MemoryStream stream = new MemoryStream();
                Data.WriteXml(stream);

                WebUpload upload = new WebUpload();
                upload.UploadUrl = this.UploadUrl;
                upload.Rename = false;
                upload.AllowExtensions = new string[] { ".xml" };
                upload.Overwrite = false;
                upload.Files = new UploadFile[] { new UploadFile(stream, Path.GetFileName(dirAndFileName), "text/xml") };
                upload.SavePath = dir;

                if (upload.Upload())
                {
                    string url = upload.Urls[0];
                    dirAndFileName = listPage.SavePath + url.Substring(url.LastIndexOf("/") + 1);
                }
            }
            else
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                //������Xml�ļ�
                Data.WriteXml(dirAndFileName);
            }


            //����в������ò��
            RootPageConfiguration rootPageConf=null;
            PageConfiguration parentPageConf = listPage.Configuration.Parent;
            while (parentPageConf != null)
            {
                if (parentPageConf is RootPageConfiguration)
                {
                    rootPageConf = (RootPageConfiguration)parentPageConf;
                    break;
                }
                else
                {
                    parentPageConf = parentPageConf.Parent;
                }
            }
            if (rootPageConf != null && rootPageConf.Plugin != null)
            {
                rootPageConf.Plugin.Receive(Data, dirAndFileName);
            }

            //�������
            if (Data.Tables.Count > 0)
                Data.Tables.Clear();

            //�����ʾ���
            SnifferForm.ClearState(this.ThreadIndex);

            //if (string.IsNullOrEmpty(PagePath))
            //    PagePath = listPage.PageName.Trim();
            //else
            //    PagePath = listPage.PageName.Trim() + "\\" + PagePath;
        }

        /// <summary>
        /// һ����ҳ�ɼ����
        /// </summary>
        /// <param name="firstPage"></param>
        void SnifferThread_FirstPageParseDone(ListPage firstPage)
        {
            ////PagePath = firstPage.Parent.PageName + "\\" + firstPage.PageName + "\\" + PagePath;
            //PagePath = firstPage.Parent.PageName.Trim() + "\\" + PagePath;
            //int int1 = PagePath.LastIndexOf("\\");

            //string dir = string.Format("{0}\\Data\\{1}\\{2}", Application.StartupPath, DateTime.Now.ToShortDateString(), PagePath.Substring(0, int1));
            //string fileName = PagePath.Substring(int1 + 1);

            //if (!Directory.Exists(dir))
            //    Directory.CreateDirectory(dir);

            //string dirAndFileName = string.Format("{0}\\{1}.xml", dir, fileName);

            ////������Xml�ļ�
            //Data.WriteXml(dirAndFileName);

            ////�����Ҫ������Excel��ʽ����ת����ʽ
            ////if (SnifferForm.DocumentFormat == DocumentFormat.Xls)
            ////{
            ////    FileUtil.XmlFileToExcelFile(dirAndFileName);

            ////    //ɾ��XML�ļ�
            ////    if (File.Exists(dirAndFileName))
            ////        File.Delete(dirAndFileName);
            ////}

            ////���PagePath
            //PagePath = string.Empty;

            ////�������
            //if (Data.Tables.Count > 0)
            //    Data.Tables.Clear();

            ////�����ʾ���
            //SnifferForm.ClearState(this.ThreadIndex);
        }

        /// <summary>
        /// ��ǰҳ�ı�
        /// </summary>
        /// <param name="currentPageUrl"></param>
        void SnifferThread_PageIndexChange(string currentPageUrl)
        {
            this.SnifferForm.SetCurrentPageUrl(this.ThreadIndex, currentPageUrl);
        }

        /// <summary>
        /// �����б�������ɣ�Ҫ�����ﱣ��
        /// </summary>
        /// <param name="listPage"></param>
        void SnifferThread_CategoryParseDone(ListPage listPage)
        {

        }
        #endregion

        #region �¼���������
        private void OnPageIndexChange(string currentPageUrl)
        {
            if (PageIndexChange != null)
            {
                PageIndexChange(currentPageUrl);
            }
        }

        private void OnDetailPageParseDone(DetailPage detailPage)
        {
            if (DetailPageParseDone != null)
            {
                DetailPageParseDone(detailPage);
            }
        }

        private void OnListPageParseDone(ListPage listPage)
        {
            if (ListPageParseDone != null)
            {
                ListPageParseDone(listPage);
            }
        }

        private void OnFirstPageParseDone(ListPage firstPage)
        {
            if (FirstPageParseDone != null)
            {
                FirstPageParseDone(firstPage);
            }
        }

        private void OnCategoryParseDone(ListPage listPage)
        {
            if (CategoryParseDone != null)
            {
                CategoryParseDone(listPage);
            }
        }
        #endregion



        /// <summary>
        /// ��ʼ����
        /// </summary>
        public void Start()
        {
            Thread myThread = new Thread(new ThreadStart(_start));
            myThread.IsBackground = true;
            myThread.Priority = ThreadPriority.Lowest;

            SnifferForm.Threads.Add(myThread);//���߳���ӵ�������̼߳���
            SnifferForm.SnifferThreads.Add(this);

            myThread.Start();
            //_start();
        }

        private void _start()
        {

            ListPage firstPage;
            lock (SnifferForm.FirstPages)
            {
                if (SnifferForm.FirstPages.Count > 0)
                {
                    firstPage = SnifferForm.FirstPages[0];
                    SnifferForm.FirstPages.Remove(firstPage);
                }
                else
                {
                    return;
                }
            }

            if (firstPage.ListPageConfiguration.SubPageConfiguration.PageType == PageType.DetailPage && this.SnifferForm.GetStartPageIndex(firstPage) > 1)
            {

                if (firstPage.ListPageConfiguration.PageMethod == PageMethod.Get)
                {
                    ReplacePageIndex(firstPage, this.SnifferForm.GetStartPageIndex(firstPage));
                }
                else
                {
                    firstPage.PageQuery = string.Format(firstPage.PageQuery, this.SnifferForm.GetStartPageIndex(firstPage));
                }

            }
            firstPage.Sniffer();

            if (firstPage.SubPageUrlResults.Count > 0 && firstPage.ListPageConfiguration.SubPageConfiguration.PageType == PageType.DetailPage)
            {
                ParseDetailPage(firstPage);
                OnListPageParseDone(firstPage);
            }
            else
            {
                ParseListPage(firstPage);
            }

            OnFirstPageParseDone(firstPage);
            bool done = this.SnifferForm.CheckDone();
            if (!done)
                _start();
        }

        /// <summary>
        /// �ɼ��б�ҳ
        /// </summary>
        /// <param name="listPage"></param>
        public void ParseListPage(ListPage listPage)
        {
            listPage.Sniffer();

            if (listPage.SubPageUrlResults.Count > 0)
            {
                foreach (UrlItem urlItem in listPage.SubPageUrlResults)
                {
                    ListPage subListPage = new ListPage(listPage, (ListPageConfiguration)listPage.ListPageConfiguration.SubPageConfiguration);
                    subListPage.PageName = urlItem.Title;
                    subListPage.PageUrl = urlItem.Url;
                    subListPage.Sniffer();

                    if (subListPage.SubPageUrlResults.Count > 0)
                    {
                        if (subListPage.ListPageConfiguration.SubPageConfiguration.PageType == PageType.DetailPage)
                        {
                            string pageName = subListPage.PageName;
                            ParseDetailPage(subListPage);
                            OnListPageParseDone(subListPage);
                        }
                        else
                        {
                            ParseListPage(subListPage);
                        }

                    }
                    else if (listPage.ListPageConfiguration.For && !string.IsNullOrEmpty(listPage.PageBody))
                    {
                        subListPage = new ListPage(listPage, (ListPageConfiguration)listPage.ListPageConfiguration);
                        subListPage.PageName = urlItem.Title;
                        subListPage.PageUrl = urlItem.Url;

                        ParseListPage(subListPage);

                    }
                }
            }

        }

        /// <summary>
        /// �ɼ���ϸҳ
        /// </summary>
        /// <param name="listPage"></param>
        public void ParseDetailPage(ListPage listPage)
        {
            ListPage backListPage = null;
            bool isNotPage = false;
            int pageIndex = this.SnifferForm.GetStartPageIndex(listPage);
            pageIndex = pageIndex + (listPage.ListPageConfiguration.PageIndexSeed -1);
            int snifferPageCount = SnifferForm.GetSnifferPageCount(listPage);
            int donePageCount = 0;
            while (!isNotPage)
            {

                listPage.Sniffer();

                //������ɹ������ߴ���Ҫ�ɼ���ҳ�������ʾû�������ˣ�����˷���
                if (!listPage.Succeed || donePageCount == snifferPageCount || (backListPage != null && backListPage.PageBody == listPage.PageBody))
                {
                    break;
                }

                OnPageIndexChange(listPage.PageUrl);

                foreach (UrlItem urlItem in listPage.SubPageUrlResults)
                {
                    DetailPageConfiguration detailPageConf = (DetailPageConfiguration)listPage.ListPageConfiguration.SubPageConfiguration;
                    DetailPage detailPage = new DetailPage(listPage, detailPageConf);
                    detailPage.PageIndex = detailPageConf.PageStartIndex;
                    detailPage.PageName = urlItem.Title;
                    detailPage.PageUrl = urlItem.Url;
                    detailPage.Sniffer();
                    OnDetailPageParseDone(detailPage);
                }


                pageIndex = pageIndex + listPage.ListPageConfiguration.PageIndexStep;
                donePageCount++;

                ListPage newListPage = new ListPage(listPage.Parent, listPage.ListPageConfiguration);
                newListPage.PageName = listPage.PageName;
                newListPage.PageUrl = listPage.PageUrl;

                if (listPage.ListPageConfiguration.PageMethod == PageMethod.Get)
                {
                    ReplacePageIndex(newListPage, pageIndex);
                }
                else
                {
                    newListPage.PageQuery = string.Format(newListPage.PageQuery, pageIndex);
                }

                backListPage = listPage;
                listPage = newListPage;
            }
        }

        private void ReplacePageIndex(ListPage listPage, int pageIndex)
        {
            string stringPageIndex;
            if (!string.IsNullOrEmpty(listPage.ListPageConfiguration.PageIndexFormat))
                stringPageIndex = pageIndex.ToString(listPage.ListPageConfiguration.PageIndexFormat);
            else
                stringPageIndex = pageIndex.ToString();

            string pageQuery = string.Format(listPage.ListPageConfiguration.ReplacePageQuery, stringPageIndex);
             listPage.PageUrl = Regex.Replace(listPage.PageUrl, listPage.ListPageConfiguration.PageQuery, pageQuery);
             string url = listPage.PageUrl;
             string s = url;
        }
    }
}
