using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using MessageLib;
using Newtonsoft.Json.Linq;

namespace Server
{
    class Program
    {
        private static string _ip = "127.0.0.1";
        private static int _port = 3000;

        private static Dictionary<(string ip, int port), string> _clients;

        private static Socket _socket;

        static void Main()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            _socket.Bind(ipEndPoint);
            _clients = new Dictionary<(string ip, int port), string>();

            Console.WriteLine($"Server start on {_ip}: {_port}");
            try
            {
                while (true)
                {
                    Receive();
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static void Receive()
        {
            try
            {
                while (true)
                {
                    StringBuilder str = new StringBuilder();
                    byte[] data = new byte[1024 * 8 * 7];
                    EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
                    do
                    {
                        var bytes = _socket.ReceiveFrom(data, ref senderEndPoint);
                        str.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (_socket.Available > 0);

                    JObject jsonMessage = (JObject)JsonConvert.DeserializeObject(str.ToString());

                    var senderIpEndPoint = (IPEndPoint) senderEndPoint;
                    Console.WriteLine($"{DateTime.Now.ToString("dd.MM HH:mm:ss")} | {senderIpEndPoint.Address}:{senderIpEndPoint.Port} ({jsonMessage?.Value<string>("Name")}) | {jsonMessage?.Value<string>("Message")} | {jsonMessage?.Value<string>("FileName")}");
                    var clientKey = (senderIpEndPoint.Address.ToString(), senderIpEndPoint.Port);
                    if (!_clients.ContainsKey(clientKey))
                    {
                        _clients.Add(clientKey, jsonMessage?.Value<string>("Name"));
                    }
                    else
                    {
                        _clients[clientKey] = jsonMessage?.Value<string>("Name");
                    }
                    
                    foreach (var client in _clients)
                    {
                        EndPoint receiverEndPoint = new IPEndPoint(IPAddress.Parse(client.Key.ip), client.Key.port);
                        _socket.SendTo(data, receiverEndPoint);
                    }
                }
            }
            catch 
            {
                
            }
        }
    }
}