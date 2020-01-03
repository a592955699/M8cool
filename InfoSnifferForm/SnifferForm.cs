using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using InfoSniffer;

namespace InfoSnifferForm
{
    public partial class SnifferForm : Form
    {
        private delegate object BackCallDelegate();
        private delegate void CreateStateTableDelegate(int threadCount);

        Thread SnifferThread;
        Thread GetFirstPageThread;
        List<ListPage> _firstPages;
        List<ListPage> _allFirstPages;
        List<Thread> _threads;
        List<SnifferThread> _snifferThreads;
        int FirstPageCount;
        int FirstPageDoneCount;
        private Object thisLock = new Object();

        public SnifferForm()
        {
            InitializeComponent();

            documentFormatComboBox.SelectedIndex = 0;
        }


        #region 属性
        /// <summary>
        /// 需要采集第一级页集合
        /// </summary>
        public List<ListPage> FirstPages
        {
            get
            {
                if (_firstPages == null)
                    _firstPages = new List<ListPage>();
                return _firstPages;
            }
        }

        /// <summary>
        /// 全部第一级页集合
        /// </summary>
        public List<ListPage> AllFirstPages
        {
            get
            {
                if (_allFirstPages == null)
                    _allFirstPages = new List<ListPage>();
                return _allFirstPages;
            }
        }

        /// <summary>
        /// 线程集合
        /// </summary>
        public List<Thread> Threads
        {
            get
            {
                if (_threads == null)
                    _threads = new List<Thread>();
                return _threads;
            }
        }

        /// <summary>
        /// SnifferThread 线程集合
        /// </summary>
        public List<SnifferThread> SnifferThreads
        {
            get
            {
                if (_snifferThreads == null)
                    _snifferThreads = new List<SnifferThread>();
                return _snifferThreads;
            }
        }

        /// <summary>
        /// 根页名称
        /// </summary>
        public string RootPageName
        {
            get
            {
                BackCallDelegate backCallDelegate = delegate()
                {
                    return rootPageComboBox.SelectedValue;
                };
                return (string)rootPageComboBox.Invoke(backCallDelegate);
            }
        }

        /// <summary>
        /// 文档格式
        /// </summary>
        public DocumentFormat DocumentFormat
        {
            get
            {
                BackCallDelegate backCallDelegate = delegate()
                {
                    if (documentFormatComboBox.SelectedIndex == 0)
                        return DocumentFormat.Xml;
                    else
                        return DocumentFormat.Xls;
                };
                return (DocumentFormat)documentFormatComboBox.Invoke(backCallDelegate);
            }
        }

        /// <summary>
        /// 线程数
        /// </summary>
        public int ThreadCount
        {
            get
            {
                return int.Parse(threadCountTextBox.Text);
            }
        }


        #endregion

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void GetConfigXml(string path)
        {
            if(string.IsNullOrEmpty(path))
                path=Application.StartupPath;

            cbxSnfFile.Items.Clear();
            rootPageComboBox.DataSource = null;
            rootPageComboBox.Items.Clear();
            string[] files = System.IO.Directory.GetFiles(path, "*.xml");
            cbxSnfFile.DisplayMember = "Text";
            cbxSnfFile.ValueMember = "Value";
            foreach (string file in files)
            {
                int sIndex = file.LastIndexOf("\\") + 1;
                int eIndex = file.IndexOf(".");
                string text = file.Substring(sIndex, eIndex - sIndex);
                cbxSnfFile.Items.Add(new CbxSnfFileListItem(text, file));
            }

            if (cbxSnfFile.Items.Count > 0)
            {
                cbxSnfFile.SelectedIndex = 0;
                SnifferConfig.OpenSnfFile(((CbxSnfFileListItem)cbxSnfFile.SelectedItem).Value);

                rootPageComboBox.DataSource = SnifferConfig.RootPageNames;
            }
        }

        /// <summary>
        /// 创建结果数据库
        /// </summary>
        private void CreateStateTable(int threadCount)
        {

            for (int i = 0; i < threadCount; i++)
            {
                this.dataGridView.Rows.Add(string.Empty, 0, 0);
            }


        }

        /// <summary>
        /// 设置当前页Url
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="pageUrl"></param>
        public void SetCurrentPageUrl(int threadIndex, string currentPageUrl)
        {
            this.dataGridView.Rows[threadIndex].Cells[0].Value = currentPageUrl;
            int count = (int)this.dataGridView.Rows[threadIndex].Cells[2].Value;
            this.dataGridView.Rows[threadIndex].Cells[2].Value = count + 1;
        }

        /// <summary>
        /// 完成记录递增 1
        /// </summary>
        /// <param name="threadIndex"></param>
        public void SetCountStep(int threadIndex)
        {
            int count = (int)this.dataGridView.Rows[threadIndex].Cells[1].Value;
            this.dataGridView.Rows[threadIndex].Cells[1].Value = count + 1;
        }


        /// <summary>
        /// 清空结果
        /// </summary>
        /// <param name="threadIndex"></param>
        public void ClearState(int threadIndex)
        {
            this.dataGridView.Rows[threadIndex].Cells[0].Value = string.Empty;
            this.dataGridView.Rows[threadIndex].Cells[1].Value = 0;
            this.dataGridView.Rows[threadIndex].Cells[2].Value = 0;
        }

        /// <summary>
        /// 开始按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "正在采集......";
            messageLabel.Visible = true;

            //开始采集进程
            SnifferThread = new Thread(new ThreadStart(Start));
            SnifferThread.IsBackground = true;
            SnifferThread.Priority = ThreadPriority.Lowest;
            SnifferThread.Start();
            //Start();
        }

        /// <summary>
        /// 开始分析，实现多线程
        /// </summary>
        public void Start()
        {
            foreach (DataGridViewRow row in rootPageGridView.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    this.FirstPages.Add(this.AllFirstPages[row.Index]);
                }
            }

            FirstPageCount = this.FirstPages.Count;

            int threadCount = this.ThreadCount;
            if (threadCount > this.FirstPages.Count)
            {
                threadCount = this.FirstPages.Count;
            }

            CreateStateTableDelegate createStateTableDelegate = new CreateStateTableDelegate(CreateStateTable);

            this.dataGridView.Invoke(createStateTableDelegate, threadCount);

            for (int i = 0; i < threadCount; i++)
            {
                SnifferThread thread = new SnifferThread(this, i);
                thread.Start();
            }
        }

        /// <summary>
        /// 读取采集起始页
        /// </summary>
        /// <param name="listPage"></param>
        /// <returns></returns>
        public int GetStartPageIndex(ListPage listPage)
        {
            ListPage firstPages = FindFirstPage(listPage);
            int index = this.AllFirstPages.IndexOf(firstPages);
            return Convert.ToInt32(this.rootPageGridView.Rows[index].Cells[1].Value);
        }

        /// <summary>
        /// 读取采集页数
        /// </summary>
        /// <param name="listPage"></param>
        /// <returns></returns>
        public int GetSnifferPageCount(ListPage listPage)
        {
            ListPage firstPages = FindFirstPage(listPage);
            int index = this.AllFirstPages.IndexOf(firstPages);
            return Convert.ToInt32(this.rootPageGridView.Rows[index].Cells[2].Value);
        }

        /// <summary>
        /// 查找第一页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private ListPage FindFirstPage(ListPage listPage)
        {
            ListPage firstPages = listPage;

            while (firstPages.Parent.Parent != null)
            {
                firstPages = (ListPage)firstPages.Parent;
            }
            return firstPages;
        }

        /// <summary>
        /// 显示当前页数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showDataFormButton_Click(object sender, EventArgs e)
        {
            DataForm form = new DataForm();
            form.DataSource = this.SnifferThreads[this.dataGridView.CurrentRow.Index].Data;
            form.Show();
        }

        /// <summary>
        /// 读取根页列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getRootPageButton_Click(object sender, EventArgs e)
        {
            if (getRootPageButton.Text != "取消")
            {
                getRootPageButton.Text = "取消";
                rootPageGridView.Rows.Clear();
                lbeGetRootPages.Visible = true;

                GetFirstPageThread = new Thread(GetRootPages);
                GetFirstPageThread.Start();
            }
            else
            {
                GetFirstPageThread.Abort();
                getRootPageButton.Text = "读取根级页";
                lbeGetRootPages.Visible = false;
                rootPageGridView.Rows.Clear();
            }
        }

        private void GetRootPages()
        {
            BackCallDelegate backCallDelegate = delegate()
            {
                foreach (ListPage page in this.AllFirstPages)
                {
                    rootPageGridView.Rows.Add(true, 1, txtPageCount.Text.Trim(), page.PageName, page.PageUrl);
                }

                getRootPageButton.Text = "读取根级页";
                lbeGetRootPages.Visible = false;
                return null;
            };


            RootPageConfiguration rootPageConf = SnifferConfig.GetRootPageConfiguration(this.RootPageName);

            if (rootPageConf == null)
            {
                ClearSet();
                getRootPageButton.Invoke(backCallDelegate);
                return;
            }

            ListPage rootPage = new ListPage((ListPageConfiguration)rootPageConf);

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
                    this.AllFirstPages.Add(page);
                }

            }
            else
            {
                foreach (ListPageConfiguration firstPageConfi in rootPageConf.SubPageConfigurations)
                {
                    this.AllFirstPages.Add(new ListPage(rootPage, firstPageConfi));
                }
            }


            getRootPageButton.Invoke(backCallDelegate);
        }

        /// <summary>
        /// 设置完成记录
        /// </summary>
        public bool CheckDone()
        {
            lock (thisLock)
            {
                FirstPageDoneCount++;
                if (FirstPageDoneCount == FirstPageCount)
                {
                    SnifferDone();
                    return true;
                }
            }
            return false;
        }

        private void openRegexTextFormButton_Click(object sender, EventArgs e)
        {
            RegexTestForm form = new RegexTestForm();
            form.Show();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in rootPageGridView.Rows)
            {
                row.Cells[0].Value = true;
            }
        }

        private void btnCancelSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in rootPageGridView.Rows)
            {
                row.Cells[0].Value = false;
            }
        }

        private void btnSetPageCount_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in rootPageGridView.Rows)
            {
                row.Cells[2].Value = txtPageCount.Text.Trim();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            SnifferThread.Abort();
            foreach (Thread thread in Threads)
            {
                thread.Abort();
            }
            SnifferDone();
        }

        private void SnifferDone()
        {
            FirstPages.Clear();
            //AllFirstPages.Clear();
            FirstPageCount = 0;
            FirstPageDoneCount = 0;

            SnifferThreads.Clear();
            Threads.Clear();

            BackCallDelegate backCallDelegate = delegate()
            {
                dataGridView.Rows.Clear();
                messageLabel.Text = "已经停止......";
                return null;
            };

            messageLabel.Invoke(backCallDelegate);
        }

        private void cbxSnfFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            SnifferConfig.OpenSnfFile(((CbxSnfFileListItem)cbxSnfFile.SelectedItem).Value);
            rootPageComboBox.DataSource = SnifferConfig.RootPageNames;
            ClearSet();
        }

        void ClearSet()
        {
            this.FirstPages.Clear();
            rootPageGridView.Rows.Clear();
            AllFirstPages.Clear();
            FirstPageCount = 0;
            FirstPageDoneCount = 0;
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            Form importDataForm = new ImportDataForm();
            importDataForm.Show();
        }

        private void btnOpenConfigFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                GetConfigXml(folderBrowserDialog1.SelectedPath);
            }
        }
    }

    public class CbxSnfFileListItem
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public CbxSnfFileListItem(string text, string value)
        {
            _text = text;
            _value = value;
        }
    }
}