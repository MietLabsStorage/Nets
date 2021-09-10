using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Client : Form
    {
        private string _ip = null;
        private int _port = 0;
        private bool _isConnected = false;
        private bool _isSending = false;

        public Client()
        {
            InitializeComponent();
        }

        private void ConnectClick(object sender, EventArgs e)
        {
            try
            {
                var address = EnsureServerAdressCorrect(tbServerAdress.Text);
                EnsureNicknameCorrect(tbNickname.Text);
                _isConnected = !_isConnected;

                _ip = _isConnected ? address.Key : null;
                _port = _isConnected ? address.Value : 0;
                btConnect.Text = _isConnected ? "Отключиться" : "Подключиться";
                lbChat.Items.Add(tbServerAdress.Text + tbNickname.Text);
                tbServerAdress.Enabled = !_isConnected;
                tbNickname.Enabled = !_isConnected;
            }
            catch(Exception exception)
            {
                lbChat.Items.Add(exception.Message);
            }
        }

        private KeyValuePair<string, int> EnsureServerAdressCorrect(string serverAdress)
        {
            if (Regex.IsMatch(serverAdress, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{0,3}:[0-9]{1,5}$"))
            {
                var address = serverAdress.Split(new string[] {":"}, StringSplitOptions.None);
                return new KeyValuePair<string, int>(address[0], Int32.Parse(address[1]));
            }
            throw new Exception("Неверный формат адресса");
        }

        private void EnsureNicknameCorrect(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                throw new Exception("Empty nickname");
            }
        }

        private void Run()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(ipPoint);

            Console.WriteLine($"Подключено к {_ip}:{_port}");

            Thread thread = new Thread(ReceiveData);
            thread.Start(clientSocket);

            string s;
            while (_isConnected)
            {
                if (_isSending)
                {
                    byte[] data = Encoding.Unicode.GetBytes(tbMessage.Text);
                    clientSocket.Send(data);
                    _isSending = false;
                }
            }

            clientSocket.Shutdown(SocketShutdown.Send);
            clientSocket.Close();
            thread.Join();

            lbChat.Items.Add($"Отключено от {_ip}:{_port}");
        }

        private void ReceiveData(object socket)
        {
            Socket clientSocket = (Socket)socket;
            byte[] data = new byte[256];
            StringBuilder str = new StringBuilder();
            int bytes;
            do
            {
                bytes = clientSocket.Receive(data, data.Length, 0);
                str.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (clientSocket.Available > 0);
            lbChat.Items.Add(str.ToString());
        }

        private void SendClick(object sender, EventArgs e)
        {
            _isSending = true;
        }
    }
}
