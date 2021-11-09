using IcmpLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IcmpGenerator
{
    public partial class Generator : Form
    {
        public Generator()
        {
            InitializeComponent();
            cbType.Items.Add(IcmpType.IcmpEchoReply);
            cbType.Items.Add(IcmpType.IcmpUnreachable);
            cbType.Items.Add(IcmpType.IcmpQuench);
            cbType.Items.Add(IcmpType.IcmpRedirect);
            cbType.Items.Add(IcmpType.IcmpEcho);
            cbType.Items.Add(IcmpType.IcmpTime);
            cbType.Items.Add(IcmpType.IcmpParameter);
            cbType.Items.Add(IcmpType.IcmpTimestamp);
            cbType.Items.Add(IcmpType.IcmpTimestampReply);
            cbType.Items.Add(IcmpType.IcmpInformation);
            cbType.Items.Add(IcmpType.IcmpInformationReply);

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
        }

        private void cbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCode.Items.Clear();

            if (cbType.SelectedItem == null)
                return;

            if ((IcmpType)cbType.SelectedItem == IcmpType.IcmpUnreachable)
            {
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachableNet);
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachableHost);
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachableProtocol);
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachablePort);
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachableFragmentation);
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachableSource);
                cbCode.Items.Add(IcmpUnreachableCode.IcmpUnreachableSize);

                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
            }

            if ((IcmpType)cbType.SelectedItem == IcmpType.IcmpTime)
            {
                cbCode.Items.Add(IcmpTimeCode.IcmpTimeTransit);
                cbCode.Items.Add(IcmpTimeCode.IcmpTimeFragment);
            }

            if ((IcmpType)cbType.SelectedItem == IcmpType.IcmpRedirect)
            {
                cbCode.Items.Add(IcmpRedirectCode.IcmpRedirectNetwork);
                cbCode.Items.Add(IcmpRedirectCode.IcmpRedirectHost);
                cbCode.Items.Add(IcmpRedirectCode.IcmpRedirectServiceNetwork);
                cbCode.Items.Add(IcmpRedirectCode.IcmpRedirectServiceHost);
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCode.Items.Clear();
            cbCode.SelectedItem = null;
            cbCode.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket _socket = null;
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                _socket.Bind(new IPEndPoint(IPAddress.Parse(textBox5.Text), int.Parse(textBox6.Text)));
                //Устанавливаем опции у сокета
                _socket.SetSocketOption(SocketOptionLevel.IP, //Принимать только IP пакеты
                    SocketOptionName.HeaderIncluded, //Включать заголовок
                    true);

                var coded = cbCode?.SelectedItem;
                int code = 0;
                if (coded is IcmpUnreachableCode) code = (int?) (IcmpUnreachableCode?) coded ?? 0;
                if (coded is IcmpTimeCode) code = (int?)(IcmpTimeCode?)coded ?? 0;
                if (coded is IcmpRedirectCode) code = (int?)(IcmpRedirectCode?)coded ?? 0;

                byte[] rest = new byte[4];
                rest[0] = (byte)(string.IsNullOrEmpty(textBox1.Text) ? 0 : byte.Parse(textBox1.Text));
                rest[1] = (byte)(string.IsNullOrEmpty(textBox2.Text) ? 0 : byte.Parse(textBox2.Text));
                rest[2] = (byte)(string.IsNullOrEmpty(textBox3.Text) ? 0 : byte.Parse(textBox3.Text));
                rest[3] = (byte)(string.IsNullOrEmpty(textBox4.Text) ? 0 : byte.Parse(textBox4.Text));
                var blob = IcmpHeader.SendIcmp(_socket, new IpHeader(), new IcmpHeader()
                {
                    Type = (byte?)(IcmpType?)cbType?.SelectedItem ?? 0,
                    Code = (byte)code,
                    Rest = rest
                }, Array.Empty<byte>());

                _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(textBox7.Text), int.Parse(textBox8.Text)),
                    new AsyncCallback(ConnectCallback), _socket);

                _socket.BeginSend(blob, 0, blob.Length, 0, new AsyncCallback(ConnectCallback), _socket);

                /*                _socket.Connect(new IPEndPoint(IPAddress.Parse(textBox7.Text), int.Parse(textBox8.Text)));
                _socket.Send(blob, 0, blob.Length, SocketFlags.None);*/
 
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                _socket?.Close();
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

            }
            catch (Exception e)
            {
                //ignore
            }
        }
    }
}
