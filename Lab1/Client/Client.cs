using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Client : Form
    {
        private delegate void RunClient();

        private string _ip = null;
        private int _port = 0;
        private bool _isConnected = false;
        private bool _isSending = false;
        private Thread _thread;

        public Client()
        {
            InitializeComponent();
        }

        private void Update()
        {
            btConnect.Text = _isConnected ? "Отключиться" : "Подключиться";
            tbServerAdress.Enabled = !_isConnected;
            tbNickname.Enabled = !_isConnected;
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
                Update();

                if (_isConnected)
                {
                    _thread = new Thread(Run);
                    _thread.Start();
                }
                else
                {
                    _thread.Join();
                    RunClient disconnectMessage = () =>
                    {
                        lbChat.Items.Add($"Отключено от {tbServerAdress.Text}");
                    };
                    this.Invoke(disconnectMessage);
                }
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
                var address = serverAdress.Split(new [] {":"}, StringSplitOptions.None);
                return new KeyValuePair<string, int>(address[0], int.Parse(address[1]));
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
            try
            {
                clientSocket.Connect(ipPoint);
            }
            catch
            {
                RunClient notConnected = () =>
                {
                    _isConnected = false;
                    lbChat.Items.Add($"Не возможно подключиться к {_ip}:{_port}");
                    Update();
                };
                this.Invoke(notConnected);
                return;
            }

            Thread thread = new Thread(ReceiveData);
            thread.Start(clientSocket);

            clientSocket.Send(Encoding.Unicode.GetBytes($"Пользователь {tbNickname.Text} присоединился"));
            try
            {
                while (_isConnected)
                {
                    if (_isSending)
                    {
                        byte[] data = Encoding.Unicode.GetBytes(tbMessage.Text);
                        clientSocket.Send(data);
                        _isSending = false;
                        RunClient clearMessageBox = () =>
                        {
                            lbChat.Items.Add(tbMessage.Text);
                            tbMessage.Text = "";
                        };
                        this.Invoke(clearMessageBox);
                    }
                }
            }
            catch (Exception exception)
            {
                _isConnected = false;
                RunClient update = () =>
                {
                    Update();
                    lbChat.Items.Add($"Отключено от {tbServerAdress.Text}");
                };
                this.Invoke(update);
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Send);
                clientSocket.Close();
                thread.Join();
            }
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

            RunClient addMessageItem = () =>
            {
                lbChat.Items.Add(str.ToString());
            };
            this.Invoke(addMessageItem);
        }

        private void SendClick(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                _isSending = true;
            }
        }
    }
}
