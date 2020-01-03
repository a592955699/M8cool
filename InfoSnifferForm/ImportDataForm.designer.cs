namespace InfoSnifferForm
{
    partial class ImportDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectXmlFile = new System.Windows.Forms.Button();
            this.btnSelectPlugin = new System.Windows.Forms.Button();
            this.dlgXmlFiles = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.dlgPlugin = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.drpPlugin = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.chkXmlFiles = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectXmlFile
            // 
            this.btnSelectXmlFile.Location = new System.Drawing.Point(523, 61);
            this.btnSelectXmlFile.Name = "btnSelectXmlFile";
            this.btnSelectXmlFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectXmlFile.TabIndex = 0;
            this.btnSelectXmlFile.Text = "打开文件";
            this.btnSelectXmlFile.UseVisualStyleBackColor = true;
            this.btnSelectXmlFile.Click += new System.EventHandler(this.btnSelectXmlFile_Click);
            // 
            // btnSelectPlugin
            // 
            this.btnSelectPlugin.Location = new System.Drawing.Point(523, 12);
            this.btnSelectPlugin.Name = "btnSelectPlugin";
            this.btnSelectPlugin.Size = new System.Drawing.Size(75, 23);
            this.btnSelectPlugin.TabIndex = 2;
            this.btnSelectPlugin.Text = "选择插件";
            this.btnSelectPlugin.UseVisualStyleBackColor = true;
            this.btnSelectPlugin.Click += new System.EventHandler(this.btnSelectPlugin_Click);
            // 
            // dlgXmlFiles
            // 
            this.dlgXmlFiles.DefaultExt = "xml";
            this.dlgXmlFiles.Filter = "XML文件|*.xml";
            this.dlgXmlFiles.Multiselect = true;
            this.dlgXmlFiles.Title = "选择要导入的数据";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择要导入的数据：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(663, 61);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 44);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始导入";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dlgPlugin
            // 
            this.dlgPlugin.DefaultExt = "dll";
            this.dlgPlugin.Filter = "DLL文件|*.dll";
            this.dlgPlugin.Title = "选择插件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "选择插件类：";
            // 
            // drpPlugin
            // 
            this.drpPlugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpPlugin.FormattingEnabled = true;
            this.drpPlugin.Location = new System.Drawing.Point(95, 12);
            this.drpPlugin.Name = "drpPlugin";
            this.drpPlugin.Size = new System.Drawing.Size(422, 20);
            this.drpPlugin.TabIndex = 7;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(661, 17);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(0, 12);
            this.lblState.TabIndex = 8;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(663, 128);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(121, 44);
            this.btnPreview.TabIndex = 6;
            this.btnPreview.Text = "预览数据";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 199);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowTemplate.Height = 23;
            this.dataGrid.Size = new System.Drawing.Size(813, 243);
            this.dataGrid.TabIndex = 9;
            // 
            // chkXmlFiles
            // 
            this.chkXmlFiles.FormattingEnabled = true;
            this.chkXmlFiles.Location = new System.Drawing.Point(14, 88);
            this.chkXmlFiles.Name = "chkXmlFiles";
            this.chkXmlFiles.Size = new System.Drawing.Size(584, 84);
            this.chkXmlFiles.TabIndex = 10;
            // 
            // ImportDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 454);
            this.Controls.Add(this.chkXmlFiles);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.drpPlugin);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectPlugin);
            this.Controls.Add(this.btnSelectXmlFile);
            this.Name = "ImportDataForm";
            this.Text = "导入数据";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectXmlFile;
        private System.Windows.Forms.Button btnSelectPlugin;
        private System.Windows.Forms.OpenFileDialog dlgXmlFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.OpenFileDialog dlgPlugin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox drpPlugin;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.CheckedListBox chkXmlFiles;
    }
}