
namespace Pop3
{
    partial class Pop3Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btGetLetters = new System.Windows.Forms.Button();
            this.lbMails = new System.Windows.Forms.ListBox();
            this.tbMail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(12, 12);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(100, 23);
            this.tbLogin.TabIndex = 0;
            this.tbLogin.Text = "mjajksj.test@mail.ru";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(118, 12);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(100, 23);
            this.tbPassword.TabIndex = 1;
            // 
            // btGetLetters
            // 
            this.btGetLetters.Location = new System.Drawing.Point(224, 11);
            this.btGetLetters.Name = "btGetLetters";
            this.btGetLetters.Size = new System.Drawing.Size(104, 23);
            this.btGetLetters.TabIndex = 2;
            this.btGetLetters.Text = "Connect";
            this.btGetLetters.UseVisualStyleBackColor = true;
            this.btGetLetters.Click += new System.EventHandler(this.btGetLetters_Click);
            // 
            // lbMails
            // 
            this.lbMails.FormattingEnabled = true;
            this.lbMails.HorizontalScrollbar = true;
            this.lbMails.ItemHeight = 15;
            this.lbMails.Location = new System.Drawing.Point(12, 41);
            this.lbMails.Name = "lbMails";
            this.lbMails.Size = new System.Drawing.Size(100, 244);
            this.lbMails.TabIndex = 3;
            this.lbMails.SelectedIndexChanged += new System.EventHandler(this.ChoiseMail);
            // 
            // tbMail
            // 
            this.tbMail.Location = new System.Drawing.Point(118, 41);
            this.tbMail.Multiline = true;
            this.tbMail.Name = "tbMail";
            this.tbMail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMail.Size = new System.Drawing.Size(210, 244);
            this.tbMail.TabIndex = 4;
            // 
            // Pop3Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 298);
            this.Controls.Add(this.tbMail);
            this.Controls.Add(this.lbMails);
            this.Controls.Add(this.btGetLetters);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbLogin);
            this.Name = "Pop3Client";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btGetLetters;
        private System.Windows.Forms.ListBox lbMails;
        private System.Windows.Forms.TextBox tbMail;
    }
}

