using IcmpLib;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sniffer
{
    internal class Program
    {
        private static Socket _socket;
        private static byte[] _buffer;

        private static void Main(string[] args)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

            // Привязываем сокет к выбранному IP
            _socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));

            //Устанавливаем опции у сокета
            _socket.SetSocketOption(SocketOptionLevel.IP, //Принимать только IP пакеты
                SocketOptionName.HeaderIncluded, //Включать заголовок
                true);

            byte[] byTrue = {1, 0, 0, 0};
            var byOut = new byte[4];

            //Socket.IOControl это аналог метода WSAIoctl в Winsock 2
            _socket.IOControl(IOControlCode.ReceiveAll, //SIO_RCVALL of Winsock
                byTrue, byOut);

            _buffer = new byte[4096];

            //Начинаем приём асинхронный приём пакетов
            while (true)
            {
                try
                {
                    _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None,
                        OnReceive, null);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public static void OnReceive(IAsyncResult ar)
        {
            try
            {
                var nReceived = _socket.EndReceive(ar);

                IpHeader ipHeader = new IpHeader(_buffer, nReceived);
                if (ipHeader.VerIhl != 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    stringBuilder.Append($"VerIhl: {ipHeader.VerIhl}\n");
                    stringBuilder.Append($"Tos: {ipHeader.Tos}\n");
                    stringBuilder.Append($"Tlen: {ipHeader.Tlen}\n");
                    stringBuilder.Append($"Id: {ipHeader.Id}\n");
                    stringBuilder.Append($"FlagsFo: {ipHeader.FlagsFo}\n");
                    stringBuilder.Append($"Ttl: {ipHeader.Ttl}\n");
                    stringBuilder.Append($"Proto: {ipHeader.Proto}\n");
                    stringBuilder.Append($"Crc: {ipHeader.Crc}\n");
                    stringBuilder.Append($"SrcAddr: {ipHeader.SrcAddr}\n");
                    stringBuilder.Append($"DstAddr: {ipHeader.DstAddr}\n");

                    Console.WriteLine(stringBuilder);
                }

                _buffer = new byte[4096];

            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}