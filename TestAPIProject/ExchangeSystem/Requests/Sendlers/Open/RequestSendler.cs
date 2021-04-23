using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ExchangeSystem.Requests.Sendlers.Open
{
    public class RequestSendler : IRequestSendler
    {
        public RequestSendler(IPackage package, ConnectionSettings settings)
        {
            RequestPackage = package;
            ConnectionInfo = settings;
        }
        public IPackage RequestPackage { get; }
        public ConnectionSettings ConnectionInfo { get; }

        public string SendRequest()
        {
            var client = new TcpClient();
            client.Connect(ConnectionInfo.HostName, ConnectionInfo.Port);
            string jsonPackage = RequestPackage.ToJson();
            byte[] buffer = Encoding.UTF32.GetBytes(jsonPackage);
            client.SendBufferSize = buffer.Length;

            var stream = client.GetStream();

            WriteData(ref stream, buffer);
            byte[] receivedBuffer = ReadData(ref stream, 256);
            
            stream.Close();

            string jsonResponse = Encoding.UTF32.GetString(receivedBuffer);
            return jsonResponse;
        }
        private byte[] ReadData(ref NetworkStream stream, int bufferSize)
        {
            byte[] receivedBuffer = new byte[bufferSize];
            do
            {
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            return receivedBuffer;
        }

        private void WriteData(ref NetworkStream stream, byte[] buffer)
        {
            do
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            while (stream.DataAvailable);
        }
    }
}
