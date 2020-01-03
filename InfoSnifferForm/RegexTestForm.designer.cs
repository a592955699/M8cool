namespace InfoSnifferForm
{
    partial class RegexTestForm
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
            this.beginTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.endTextBox = new System.Windows.Forms.TextBox();
            this.testButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.containCheckBox = new System.Windows.Forms.CheckBox();
            this.bodyTextBox = new System.Windows.Forms.RichTextBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbxUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // beginTextBox
            // 
            this.beginTextBox.Location = new System.Drawing.Point(83, 291);
            this.beginTextBox.Multiline = true;
            this.beginTextBox.Name = "beginTextBox";
            this.beginTextBox.Size = new System.Drawing.Size(672, 50);
            this.beginTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始正则：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 347);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束正则：";
            // 
            // endTextBox
            // 
            this.endTextBox.Location = new System.Drawing.Point(83, 347);
            this.endTextBox.Multiline = true;
            this.endTextBox.Name = "endTextBox";
            this.endTextBox.Size = new System.Drawing.Size(672, 50);
            this.endTextBox.TabIndex = 4;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(223, 403);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 5;
            this.testButton.Text = "测试";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(567, 414);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "共找到";
            // 
            // countTextBox
            // 
            this.countTextBox.Location = new System.Drawing.Point(614, 405);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.Size = new System.Drawing.Size(59, 21);
            this.countTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(679, 414);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "条";
            // 
            // containCheckBox
            // 
            this.containCheckBox.AutoSize = true;
            this.containCheckBox.Location = new System.Drawing.Point(83, 409);
            this.containCheckBox.Name = "containCheckBox";
            this.containCheckBox.Size = new System.Drawing.Size(120, 16);
            this.containCheckBox.TabIndex = 10;
            this.containCheckBox.Text = "包含开始结束正则";
            this.containCheckBox.UseVisualStyleBackColor = true;
            // 
            // bodyTextBox
            // 
            this.bodyTextBox.Location = new System.Drawing.Point(14, 41);
            this.bodyTextBox.Name = "bodyTextBox";
            this.bodyTextBox.Size = new System.Drawing.Size(741, 244);
            this.bodyTextBox.TabIndex = 11;
            this.bodyTextBox.Text = "";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(12, 431);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultTextBox.Size = new System.Drawing.Size(743, 171);
            this.resultTextBox.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(680, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "读取网页";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbxUrl
            // 
            this.tbxUrl.Location = new System.Drawing.Point(97, 14);
            this.tbxUrl.Name = "tbxUrl";
            this.tbxUrl.Size = new System.Drawing.Size(467, 21);
            this.tbxUrl.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "网址：";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            "GB2312",
            "UTF-8"});
            this.cmbEncoding.Location = new System.Drawing.Point(569, 14);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(103, 20);
            this.cmbEncoding.TabIndex = 17;
            // 
            // RegexTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 616);
            this.Controls.Add(this.cmbEncoding);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxUrl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.bodyTextBox);
            this.Controls.Add(this.containCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.countTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.endTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.beginTextBox);
            this.Name = "RegexTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegexTestForm";
            this.Load += new System.EventHandler(this.RegexTestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox beginTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox endTextBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox containCheckBox;
        private System.Windows.Forms.RichTextBox bodyTextBox;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbxUrl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEncoding;
    }
}