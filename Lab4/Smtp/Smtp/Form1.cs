using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smtp
{
    public partial class SmtpClient : Form
    {
        public SmtpClient()
        {
            InitializeComponent();
        }

        string attachPath = "";

		private void btSend_Click(object sender, EventArgs e)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(tbFrom.Text));
			message.To.Add(new MailboxAddress(tbTo.Text));
			message.Subject = tbSubject.Text;

            var builder = new BodyBuilder();
            builder.TextBody = tbMessage.Text;
            if (!string.IsNullOrEmpty(attachPath))
            { 
                builder.Attachments.Add(attachPath); 
            }

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.mail.ru", 465, true);

                client.Authenticate("mjajksj.test@mail.ru", "password");

                client.Send(message);
                client.Disconnect(true);
                MessageBox.Show("Sended");
            }
		}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btAttach_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = "D:";
            dialog.Title = "Выбор архива";
            var result = dialog.ShowDialog();
            lbAttach.Text = dialog.SafeFileName;
            attachPath = dialog.FileName;
        }
    }
}
