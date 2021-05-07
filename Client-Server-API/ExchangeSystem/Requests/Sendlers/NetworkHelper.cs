using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ExchangeSystem.Requests.Sendlers
{
    public class NetworkHelper
    {
        public Encoding Encoding { get; } = Encoding.UTF32;
        public byte[] ReadData(NetworkStream stream, int bufferSize)
        {
            byte[] receivedBuffer = new byte[bufferSize];
            do
            {
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            return receivedBuffer;
        }
        public void WriteData(NetworkStream stream, byte[] buffer)
        {
            do
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            while (stream.DataAvailable);
        }
    }
}
