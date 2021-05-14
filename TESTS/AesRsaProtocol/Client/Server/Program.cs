using System;
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
            var listener = new TcpListener(80);
            listener.Start();
            var client = listener.AcceptTcpClient();
            var stream = client.GetStream();

            stream.Read(buffer1, 0, buffer1.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer1));

            stream.Read(buffer2, 0, buffer2.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer1));

            stream.Read(buffer3, 0, buffer3.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer1));

            stream.Read(buffer4, 0, buffer4.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer1));

            stream.Read(buffer5, 0, buffer5.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer1));
        }
    }
}
