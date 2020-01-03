using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using InfoSniffer;
using System.Threading;

namespace InfoSnifferForm
{
    public partial class ImportDataForm : Form
    {
        private delegate object BackCallDelegate();

        Assembly PluginAssembly;

        public ImportDataForm()
        {
            InitializeComponent();
        }

        private void btnSelectXmlFile_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgXmlFiles.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (string file in dlgXmlFiles.FileNames)
                {
                    chkXmlFiles.Items.Add(file);
                }
            }
        }

        private void btnSelectPlugin_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgPlugin.ShowDialog();
            if (result == DialogResult.OK)
            {
                PluginAssembly = Assembly.LoadFile(dlgPlugin.FileName);
                Type[] types = PluginAssembly.GetTypes();

                foreach (Type type in types)
                {
                    drpPlugin.Items.Add(type.FullName);
                }
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            lblState.Text = "正在导入...";
            if (chkXmlFiles.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择数据文件");
                return;
            }
            if (string.IsNullOrEmpty(dlgPlugin.FileName))
            {
                MessageBox.Show("请选择插件");
                return;
            }
            if ((string)drpPlugin.SelectedValue == "")
            {
                MessageBox.Show("请选择插件类");
                return;
            }

            Thread thread = new Thread(new ThreadStart(StartImport));
            thread.Start();
        }

        void StartImport()
        {
            BackCallDelegate backCallDelegate;

            backCallDelegate = delegate()
            {
                return (string)drpPlugin.SelectedItem;
            };

            string pluginName = (string)drpPlugin.Invoke(backCallDelegate);

            Type pluginType = PluginAssembly.GetType(pluginName);

            IPlugin plugin = (IPlugin)pluginType.GetConstructor(new Type[0]).Invoke(new object[0]);

            DataSet ds;
            foreach (object xmlFile in chkXmlFiles.CheckedItems)
            {
                ds = new DataSet();

                string filePath = (string)xmlFile;

                ds.ReadXml(filePath);

                plugin.Receive(ds, filePath);
            }

            backCallDelegate = delegate()
            {
                lblState.Text = "导入完成！";
                return null;
            };

            lblState.Invoke(backCallDelegate);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (chkXmlFiles.SelectedItem == null)
            {
                MessageBox.Show("请选择数据文件");
                return;
            }

            DataSet ds = new DataSet();
            ds.ReadXml((string)chkXmlFiles.SelectedItem);

            dataGrid.AutoGenerateColumns = true;
            dataGrid.DataSource = ds.Tables[0];
        }
    }
}
