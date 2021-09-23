namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbChat = new System.Windows.Forms.ListBox();
            this.tbMyAddress = new System.Windows.Forms.TextBox();
            this.tbSendAddress = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btSend = new System.Windows.Forms.Button();
            this.btFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbChat
            // 
            this.lbChat.FormattingEnabled = true;
            this.lbChat.Location = new System.Drawing.Point(40, 103);
            this.lbChat.Name = "lbChat";
            this.lbChat.Size = new System.Drawing.Size(556, 277);
            this.lbChat.TabIndex = 0;
            // 
            // tbMyAddress
            // 
            this.tbMyAddress.Location = new System.Drawing.Point(40, 28);
            this.tbMyAddress.Name = "tbMyAddress";
            this.tbMyAddress.Size = new System.Drawing.Size(133, 20);
            this.tbMyAddress.TabIndex = 1;
            this.tbMyAddress.Text = "127.0.0.1:3001";
            // 
            // tbSendAddress
            // 
            this.tbSendAddress.Location = new System.Drawing.Point(40, 77);
            this.tbSendAddress.Name = "tbSendAddress";
            this.tbSendAddress.Size = new System.Drawing.Size(133, 20);
            this.tbSendAddress.TabIndex = 2;
            this.tbSendAddress.Text = "127.0.0.1:3000";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(40, 395);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(168, 20);
            this.tbMessage.TabIndex = 3;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(475, 28);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(121, 19);
            this.btConnect.TabIndex = 4;
            this.btConnect.Text = "Подключиться";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.Connect);
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(475, 396);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(121, 19);
            this.btSend.TabIndex = 5;
            this.btSend.Text = "Отправить";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.Send);
            // 
            // btFile
            // 
            this.btFile.Location = new System.Drawing.Point(214, 395);
            this.btFile.Name = "btFile";
            this.btFile.Size = new System.Drawing.Size(121, 19);
            this.btFile.TabIndex = 6;
            this.btFile.Text = "Файл";
            this.btFile.UseVisualStyleBackColor = true;
            this.btFile.Click += new System.EventHandler(this.SelectFile);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // lFileName
            // 
            this.lFileName.AutoSize = true;
            this.lFileName.Location = new System.Drawing.Point(37, 418);
            this.lFileName.Name = "lFileName";
            this.lFileName.Size = new System.Drawing.Size(35, 13);
            this.lFileName.TabIndex = 7;
            this.lFileName.Text = "no file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Мой аддресс";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Аддресс сервера";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(214, 25);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(133, 20);
            this.tbPath.TabIndex = 10;
            this.tbPath.Text = "С:\\\\";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Путь автосохранения";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(214, 77);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(133, 20);
            this.tbName.TabIndex = 12;
            this.tbName.Text = "Jhoe";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Имя";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lFileName);
            this.Controls.Add(this.btFile);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.tbSendAddress);
            this.Controls.Add(this.tbMyAddress);
            this.Controls.Add(this.lbChat);
            this.Name = "Client";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbChat;
        private System.Windows.Forms.TextBox tbMyAddress;
        private System.Windows.Forms.TextBox tbSendAddress;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button btFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label4;
    }
}

