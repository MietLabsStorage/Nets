using IcmpLib;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Sniffer
{
    internal class Program
    {
        private static Socket _socket;
        private static byte[] _buffer;
        private static Config _config;

        private static void Main(string[] args)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

            // Привязываем сокет к выбранному IP
            _socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 100));

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

            _config = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"));

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

                    var rest = ipHeader.Rest;

                    switch (ipHeader.Proto)
                    {
                        case 1:
                            IcmpHeader icmpHeader = new IcmpHeader(rest, rest.Length);
                            stringBuilder.Append("--icmp--\n");
                            stringBuilder.Append($"Type: {icmpHeader.Type}\n");
                            stringBuilder.Append($"Code: {icmpHeader.Code}\n");
                            stringBuilder.Append($"ControlSum: {icmpHeader.ControlSum}\n");
                            stringBuilder.Append($"Rest: {icmpHeader.Rest[0]} {icmpHeader.Rest[1]} {icmpHeader.Rest[2]} {icmpHeader.Rest[3]}\n");

                            if (!_config.CheckIcmp)
                            {
                                stringBuilder.Clear();
                            }
                            break;

                        case 6:
                            TcpHeader tcpHeader = new TcpHeader(rest, rest.Length);
                            stringBuilder.Append("--tcp--\n");
                            stringBuilder.Append($"src_port: {tcpHeader.src_port}\n");
                            stringBuilder.Append($"dst_port: {tcpHeader.dst_port}\n");
                            stringBuilder.Append($"seq_n: {tcpHeader.seq_n}\n");
                            stringBuilder.Append($"ack_n: {tcpHeader.ack_n}\n");
                            stringBuilder.Append($"offset: {tcpHeader.offset}\n");
                            stringBuilder.Append($"flags: {tcpHeader.flags}\n");
                            stringBuilder.Append($"win: {tcpHeader.win}\n");
                            stringBuilder.Append($"crc: {tcpHeader.crc}\n");
                            stringBuilder.Append($"padding: {tcpHeader.padding}\n");
                            if (!_config.CheckTcp)
                            {
                                stringBuilder.Clear();
                            }
                            break;

                        case 17:
                            UdpHeader udpHeader = new UdpHeader(rest, rest.Length);
                            stringBuilder.Append("--udp--\n");
                            stringBuilder.Append($"SrcPort: {udpHeader.SrcPort}\n");
                            stringBuilder.Append($"DstPort: {udpHeader.DstPort}\n");
                            stringBuilder.Append($"Length: {udpHeader.Length}\n");
                            stringBuilder.Append($"ControlSum: {udpHeader.Crc}\n");
                            if (!_config.CheckUdp)
                            {
                                stringBuilder.Clear();
                            }
                            break;

                        default:
                            if (!_config.CheckIp)
                            {
                                stringBuilder.Clear();
                            }
                            break;
                    }

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