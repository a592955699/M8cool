namespace InfoSnifferForm
{
    partial class LogoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLoginUrl = new System.Windows.Forms.TextBox();
            this.txtPostData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVerifyCodeUrl = new System.Windows.Forms.TextBox();
            this.btnRequest = new System.Windows.Forms.Button();
            this.picVerifyCode = new System.Windows.Forms.PictureBox();
            this.btnGetVerifyCodeImage = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picVerifyCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "登录网址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据：";
            // 
            // txtLoginUrl
            // 
            this.txtLoginUrl.Location = new System.Drawing.Point(97, 21);
            this.txtLoginUrl.Name = "txtLoginUrl";
            this.txtLoginUrl.Size = new System.Drawing.Size(732, 21);
            this.txtLoginUrl.TabIndex = 3;
            this.txtLoginUrl.Text = "http://www.592zn.com/logging.php?action=login&loginsubmit=yes&inajax=1";
            // 
            // txtPostData
            // 
            this.txtPostData.Location = new System.Drawing.Point(97, 135);
            this.txtPostData.Multiline = true;
            this.txtPostData.Name = "txtPostData";
            this.txtPostData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPostData.Size = new System.Drawing.Size(732, 107);
            this.txtPostData.TabIndex = 4;
            this.txtPostData.Text = "username=plumsea&password=plumsea";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "验证码地址：";
            // 
            // txtVerifyCodeUrl
            // 
            this.txtVerifyCodeUrl.Location = new System.Drawing.Point(97, 62);
            this.txtVerifyCodeUrl.Name = "txtVerifyCodeUrl";
            this.txtVerifyCodeUrl.Size = new System.Drawing.Size(581, 21);
            this.txtVerifyCodeUrl.TabIndex = 6;
            // 
            // btnRequest
            // 
            this.btnRequest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRequest.Location = new System.Drawing.Point(835, 214);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(104, 28);
            this.btnRequest.TabIndex = 7;
            this.btnRequest.Text = "开始";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // picVerifyCode
            // 
            this.picVerifyCode.Location = new System.Drawing.Point(684, 62);
            this.picVerifyCode.Name = "picVerifyCode";
            this.picVerifyCode.Size = new System.Drawing.Size(255, 60);
            this.picVerifyCode.TabIndex = 8;
            this.picVerifyCode.TabStop = false;
            // 
            // btnGetVerifyCodeImage
            // 
            this.btnGetVerifyCodeImage.Location = new System.Drawing.Point(835, 142);
            this.btnGetVerifyCodeImage.Name = "btnGetVerifyCodeImage";
            this.btnGetVerifyCodeImage.Size = new System.Drawing.Size(104, 30);
            this.btnGetVerifyCodeImage.TabIndex = 9;
            this.btnGetVerifyCodeImage.Text = "获取验证码";
            this.btnGetVerifyCodeImage.UseVisualStyleBackColor = true;
            this.btnGetVerifyCodeImage.Click += new System.EventHandler(this.btnGetVerifyCodeImage_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(16, 282);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(923, 227);
            this.webBrowser1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "登录结果：";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            "GB2312",
            "UTF-8"});
            this.cmbEncoding.Location = new System.Drawing.Point(835, 21);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(104, 20);
            this.cmbEncoding.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "请求网址：";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(97, 101);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(581, 21);
            this.txtUrl.TabIndex = 14;
            this.txtUrl.Text = "http://www.592zn.com/index.php";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(835, 178);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(104, 30);
            this.btnLogin.TabIndex = 15;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LogoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 521);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbEncoding);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnGetVerifyCodeImage);
            this.Controls.Add(this.picVerifyCode);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.txtVerifyCodeUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPostData);
            this.Controls.Add(this.txtLoginUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "LogoForm";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LogoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picVerifyCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLoginUrl;
        private System.Windows.Forms.TextBox txtPostData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVerifyCodeUrl;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.PictureBox picVerifyCode;
        private System.Windows.Forms.Button btnGetVerifyCodeImage;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnLogin;
    }
}