using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = Encoding.UTF8.GetBytes("Всем русским хай");
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(new IPAddress(127001), 80));
            socket.Send(data);
            socket.Dispose();
            socket.Close();

        }
    }
}
