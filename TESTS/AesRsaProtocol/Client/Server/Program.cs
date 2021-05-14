using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        private static byte[] buffer1 = new byte[128];
        private static byte[] buffer2 = new byte[128];
        private static byte[] buffer3 = new byte[128];
        private static byte[] buffer4 = new byte[128];
        private static byte[] buffer5 = new byte[128];

        static void Main(string[] args)
        {
            byte[] requestdata = new byte[128];
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(127001, 80));
            var connectedSocket = socket.Accept();
            connectedSocket.Receive(requestdata);
            string message = Encoding.UTF8.GetString(requestdata);
            Console.WriteLine(message);
        }
    }
}
