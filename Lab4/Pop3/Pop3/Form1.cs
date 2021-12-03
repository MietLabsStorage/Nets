using MailKit.Net.Pop3;
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

namespace Pop3
{
    public partial class Pop3Client : Form
    {
        public Pop3Client()
        {
            InitializeComponent();
        }

        List<MimeMessage> messages;

        private void btGetLetters_Click(object sender, EventArgs e)
        {
            using (var client = new MailKit.Net.Pop3.Pop3Client())
            {
                client.Connect("pop.mail.ru", 995, true);

                client.Authenticate(tbLogin.Text, tbPassword.Text);

                messages = new List<MimeMessage>();
                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    messages.Add(message);
                    lbMails.Items.Add(message.Subject);
                }

                client.Disconnect(true);
            }
        }

        public void ChoiseMail(object sender, EventArgs e)
        {
            tbMail.Text = messages.ElementAtOrDefault(lbMails.SelectedIndex).TextBody;
        }
    }
}
