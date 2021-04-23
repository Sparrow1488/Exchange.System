using ExchangeSystem.Requests;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SendDataTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(80);
            listener.Start();
            Console.WriteLine("Listen...");

            var client = listener.AcceptTcpClient();
            Console.WriteLine("Connected");
            var stream = client.GetStream();

            byte[] receiveArray = new byte[1024];
            StringBuilder builder = new StringBuilder();
            do
            {
                int bytes = stream.Read(receiveArray, 0, receiveArray.Length);
                builder.Append(Encoding.UTF32.GetString(receiveArray, 0, bytes));
            }
            while (stream.DataAvailable);

            Package pack = (Package)JsonConvert.DeserializeObject(builder.ToString(), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All, 
            });
            Console.WriteLine(pack);

            
        }
    }
}
