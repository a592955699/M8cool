using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfoSnifferForm
{
    public partial class DataForm : Form
    {
        public DataSet _dataSource;
        public DataTable DataTable;

        public DataSet DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        public DataForm()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource = DataTable;
            RefreshData();
            timer1.Enabled = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            if (DataSource != null && DataSource.Tables.Count > 0)
            {
                if (DataTable == null)
                {
                    DataTable = DataSource.Tables[0];

                    foreach (DataColumn column in DataTable.Columns)
                    {
                        this.dataGridView.Columns.Add(column.ColumnName, column.ColumnName);
                    }
                }
                if (this.dataGridView.Rows.Count < DataTable.Rows.Count)
                {

                    for (int i = this.dataGridView.Rows.Count; i < DataTable.Rows.Count; i++)
                    {
                        object[] values = new object[DataTable.Columns.Count];
                        for (int j = 0; j < DataTable.Columns.Count; j++)
                        {
                            values[j] = DataTable.Rows[i][DataTable.Columns[j]];
                        }
                        this.dataGridView.Rows.Add(values);
                    }
                    this.dataGridView.CurrentCell = this.dataGridView.Rows[this.dataGridView.Rows.Count - 1].Cells[this.dataGridView.CurrentCell.ColumnIndex];
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}