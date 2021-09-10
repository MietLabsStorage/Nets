using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static string _ip = "127.0.0.1";
        private static int _port = 3000;
        private static int[] _clientsPorts = {3001};

        static void Main()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);;
            try
            {
                Task receiveTask = new Task(() => Receive(socket));
                receiveTask.Start();

                while (true)
                {
                    string message = Console.ReadLine();

                    byte[] data = Encoding.Unicode.GetBytes(message);
                    foreach (var remotePort in _clientsPorts)
                    {
                        EndPoint receiverEndPoint = new IPEndPoint(IPAddress.Parse(_ip), remotePort);
                        socket.SendTo(data, receiverEndPoint);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Close(socket);
            }
        }

        private static void Receive(Socket socket)
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
                socket.Bind(ipEndPoint);

                while (true)
                {
                    StringBuilder str = new StringBuilder();
                    byte[] data = new byte[256];
                    EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    do
                    {
                        var bytes = socket.ReceiveFrom(data, ref senderEndPoint);
                        str.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);

                    var senderIpEndPoint = (IPEndPoint)senderEndPoint;
                    Console.WriteLine($"{senderIpEndPoint.Address}:{senderIpEndPoint.Port} {str}");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Close(socket);
            }
        }

        private static void Close(Socket socket)
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}