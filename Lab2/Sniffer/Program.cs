using System;
using System.Net;
using System.Net.Sockets;

namespace Sniffer
{
    class Program
    {
        private static Socket mainSocket;
        private static byte[] buffer;

        static void Main(string[] args)
        {
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

            // Привязываем сокет к выбранному IP
            mainSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));

            //Устанавливаем опции у сокета
            mainSocket.SetSocketOption(SocketOptionLevel.IP,  //Принимать только IP пакеты
                SocketOptionName.HeaderIncluded, //Включать заголовок
                true);

            byte[] byTrue = { 1, 0, 0, 0};
            byte[] byOut = new byte[4];

            //Socket.IOControl это аналог метода WSAIoctl в Winsock 2
            mainSocket.IOControl(IOControlCode.ReceiveAll,  //SIO_RCVALL of Winsock
                byTrue, byOut);

            buffer=new byte[4096];

            //Начинаем приём асинхронный приём пакетов
            while (true)
            {
                mainSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), null);
            }
        }

        public static void OnReceive(IAsyncResult ar)
        {
            int nReceived = mainSocket.EndReceive(ar);

            /*ParseDataIcmp(buffer, nReceived);

            buffer = new byte[4096];

            icmpListener.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
                new AsyncCallback(OnReceiveIcmp), null);*/

            Console.WriteLine(nReceived);
        }
    }
}
