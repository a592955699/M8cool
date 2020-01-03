namespace InfoSnifferForm
{
    partial class SnifferForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openRegexTextFormButton = new System.Windows.Forms.Button();
            this.continueCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.documentFormatComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rootPageComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.threadCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.showDataFormButton = new System.Windows.Forms.Button();
            this.rootPageGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getRootPageButton = new System.Windows.Forms.Button();
            this.txtPageCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnCancelSelect = new System.Windows.Forms.Button();
            this.btnSetPageCount = new System.Windows.Forms.Button();
            this.lbeGetRootPages = new System.Windows.Forms.Label();
            this.cbxSnfFile = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnImportData = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnOpenConfigFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rootPageGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // openRegexTextFormButton
            // 
            this.openRegexTextFormButton.Location = new System.Drawing.Point(504, 508);
            this.openRegexTextFormButton.Name = "openRegexTextFormButton";
            this.openRegexTextFormButton.Size = new System.Drawing.Size(125, 23);
            this.openRegexTextFormButton.TabIndex = 37;
            this.openRegexTextFormButton.Text = "正则表达式测试";
            this.openRegexTextFormButton.UseVisualStyleBackColor = true;
            this.openRegexTextFormButton.Click += new System.EventHandler(this.openRegexTextFormButton_Click);
            // 
            // continueCheckBox
            // 
            this.continueCheckBox.Location = new System.Drawing.Point(12, 478);
            this.continueCheckBox.Name = "continueCheckBox";
            this.continueCheckBox.Size = new System.Drawing.Size(112, 24);
            this.continueCheckBox.TabIndex = 36;
            this.continueCheckBox.Text = "继续上一次采集";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(174, 508);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 34;
            this.saveButton.Text = "保存";
            this.saveButton.Visible = false;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.ForeColor = System.Drawing.Color.Red;
            this.messageLabel.Location = new System.Drawing.Point(268, 513);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(89, 12);
            this.messageLabel.TabIndex = 32;
            this.messageLabel.Text = "正在采集......";
            this.messageLabel.Visible = false;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(93, 508);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 31;
            this.stopButton.Text = "停止";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 508);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 30;
            this.startButton.Text = "开始";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // documentFormatComboBox
            // 
            this.documentFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.documentFormatComboBox.Items.AddRange(new object[] {
            "Xml 格式",
            "Excel 格式"});
            this.documentFormatComboBox.Location = new System.Drawing.Point(81, 452);
            this.documentFormatComboBox.Name = "documentFormatComboBox";
            this.documentFormatComboBox.Size = new System.Drawing.Size(121, 20);
            this.documentFormatComboBox.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 455);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "输出格式：";
            // 
            // rootPageComboBox
            // 
            this.rootPageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rootPageComboBox.Location = new System.Drawing.Point(462, 6);
            this.rootPageComboBox.Name = "rootPageComboBox";
            this.rootPageComboBox.Size = new System.Drawing.Size(195, 20);
            this.rootPageComboBox.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(391, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "采集网站：";
            // 
            // threadCountTextBox
            // 
            this.threadCountTextBox.Location = new System.Drawing.Point(81, 418);
            this.threadCountTextBox.Name = "threadCountTextBox";
            this.threadCountTextBox.Size = new System.Drawing.Size(64, 21);
            this.threadCountTextBox.TabIndex = 20;
            this.threadCountTextBox.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "线程数：";
            // 
            // showDataFormButton
            // 
            this.showDataFormButton.Location = new System.Drawing.Point(746, 405);
            this.showDataFormButton.Name = "showDataFormButton";
            this.showDataFormButton.Size = new System.Drawing.Size(116, 23);
            this.showDataFormButton.TabIndex = 38;
            this.showDataFormButton.Text = "显示当前线程数据";
            this.showDataFormButton.UseVisualStyleBackColor = true;
            this.showDataFormButton.Click += new System.EventHandler(this.showDataFormButton_Click);
            // 
            // rootPageGridView
            // 
            this.rootPageGridView.AllowUserToAddRows = false;
            this.rootPageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rootPageGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.rootPageGridView.Location = new System.Drawing.Point(12, 32);
            this.rootPageGridView.Name = "rootPageGridView";
            this.rootPageGridView.RowTemplate.Height = 23;
            this.rootPageGridView.Size = new System.Drawing.Size(850, 120);
            this.rootPageGridView.TabIndex = 33;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "采集";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "起始页";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "采集页数";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "页名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 130;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "页网址";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 400;
            // 
            // getRootPageButton
            // 
            this.getRootPageButton.Location = new System.Drawing.Point(663, 4);
            this.getRootPageButton.Name = "getRootPageButton";
            this.getRootPageButton.Size = new System.Drawing.Size(75, 23);
            this.getRootPageButton.TabIndex = 39;
            this.getRootPageButton.Text = "读取根级页";
            this.getRootPageButton.UseVisualStyleBackColor = true;
            this.getRootPageButton.Click += new System.EventHandler(this.getRootPageButton_Click);
            // 
            // txtPageCount
            // 
            this.txtPageCount.Location = new System.Drawing.Point(688, 160);
            this.txtPageCount.Name = "txtPageCount";
            this.txtPageCount.Size = new System.Drawing.Size(68, 21);
            this.txtPageCount.TabIndex = 40;
            this.txtPageCount.Text = "15";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(617, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "采集页数：";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column8});
            this.dataGridView.Location = new System.Drawing.Point(12, 189);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(850, 210);
            this.dataGridView.TabIndex = 42;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "网址";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 480;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "当前记录";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "当前页";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(13, 155);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 43;
            this.btnSelectAll.Text = "全部选择";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCancelSelect
            // 
            this.btnCancelSelect.Location = new System.Drawing.Point(95, 155);
            this.btnCancelSelect.Name = "btnCancelSelect";
            this.btnCancelSelect.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSelect.TabIndex = 44;
            this.btnCancelSelect.Text = "取消选择";
            this.btnCancelSelect.UseVisualStyleBackColor = true;
            this.btnCancelSelect.Click += new System.EventHandler(this.btnCancelSelect_Click);
            // 
            // btnSetPageCount
            // 
            this.btnSetPageCount.Location = new System.Drawing.Point(772, 158);
            this.btnSetPageCount.Name = "btnSetPageCount";
            this.btnSetPageCount.Size = new System.Drawing.Size(90, 23);
            this.btnSetPageCount.TabIndex = 45;
            this.btnSetPageCount.Text = "设置采集页数";
            this.btnSetPageCount.UseVisualStyleBackColor = true;
            this.btnSetPageCount.Click += new System.EventHandler(this.btnSetPageCount_Click);
            // 
            // lbeGetRootPages
            // 
            this.lbeGetRootPages.AutoSize = true;
            this.lbeGetRootPages.ForeColor = System.Drawing.Color.Red;
            this.lbeGetRootPages.Location = new System.Drawing.Point(744, 9);
            this.lbeGetRootPages.Name = "lbeGetRootPages";
            this.lbeGetRootPages.Size = new System.Drawing.Size(107, 12);
            this.lbeGetRootPages.TabIndex = 46;
            this.lbeGetRootPages.Text = "正在读取根级页...";
            this.lbeGetRootPages.Visible = false;
            // 
            // cbxSnfFile
            // 
            this.cbxSnfFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSnfFile.FormattingEnabled = true;
            this.cbxSnfFile.Location = new System.Drawing.Point(180, 6);
            this.cbxSnfFile.Name = "cbxSnfFile";
            this.cbxSnfFile.Size = new System.Drawing.Size(194, 20);
            this.cbxSnfFile.TabIndex = 47;
            this.cbxSnfFile.SelectedIndexChanged += new System.EventHandler(this.cbxSnfFile_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "采集配置：";
            // 
            // btnImportData
            // 
            this.btnImportData.Location = new System.Drawing.Point(654, 508);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(75, 23);
            this.btnImportData.TabIndex = 49;
            this.btnImportData.Text = "导入数据";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // btnOpenConfigFolder
            // 
            this.btnOpenConfigFolder.Location = new System.Drawing.Point(12, 4);
            this.btnOpenConfigFolder.Name = "btnOpenConfigFolder";
            this.btnOpenConfigFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenConfigFolder.TabIndex = 50;
            this.btnOpenConfigFolder.Text = "配置目录";
            this.btnOpenConfigFolder.UseVisualStyleBackColor = true;
            this.btnOpenConfigFolder.Click += new System.EventHandler(this.btnOpenConfigFolder_Click);
            // 
            // SnifferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 543);
            this.Controls.Add(this.btnOpenConfigFolder);
            this.Controls.Add(this.btnImportData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxSnfFile);
            this.Controls.Add(this.lbeGetRootPages);
            this.Controls.Add(this.btnSetPageCount);
            this.Controls.Add(this.btnCancelSelect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPageCount);
            this.Controls.Add(this.documentFormatComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.threadCountTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getRootPageButton);
            this.Controls.Add(this.rootPageGridView);
            this.Controls.Add(this.showDataFormButton);
            this.Controls.Add(this.openRegexTextFormButton);
            this.Controls.Add(this.continueCheckBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.rootPageComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.MaximizeBox = false;
            this.Name = "SnifferForm";
            this.Text = "海潮信息采集软件";
            ((System.ComponentModel.ISupportInitialize)(this.rootPageGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openRegexTextFormButton;
        private System.Windows.Forms.CheckBox continueCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox documentFormatComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox rootPageComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox threadCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showDataFormButton;
        private System.Windows.Forms.DataGridView rootPageGridView;
        private System.Windows.Forms.Button getRootPageButton;
        private System.Windows.Forms.TextBox txtPageCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnCancelSelect;
        private System.Windows.Forms.Button btnSetPageCount;
        private System.Windows.Forms.Label lbeGetRootPages;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ComboBox cbxSnfFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnOpenConfigFolder;
    }
}