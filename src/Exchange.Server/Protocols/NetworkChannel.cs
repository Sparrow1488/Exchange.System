using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public class NetworkChannel : INetworkChannelWriter, INetworkChannelReader
    {
        public Encoding Encoding { get; } = Encoding.UTF8;
        public int BufferSize 
        { 
            get 
            { 
                return _bufferSize; 
            } 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Вы указали недопустимое значение для размера буфера");
                else
                    _bufferSize = value; 
            } 
        }
        private int _bufferSize = 1024;

        public NetworkChannel(int bufferSize)
        {
            BufferSize = bufferSize;
        }
        public NetworkChannel() { }

        public async Task<string> ReadAsync(NetworkStream stream)
        {
            StringBuilder builder = new StringBuilder();
            byte[] receivedBuffer = new byte[BufferSize];
            do
            {
                var bytes = await stream.ReadAsync(receivedBuffer, 0, receivedBuffer.Length);
                builder.Append(Encoding.GetString(receivedBuffer, 0, bytes));
            }
            while (stream.DataAvailable);
            stream.Flush();
            return builder.ToString();
        }
        public async Task<byte[]> ReadDataAsync(NetworkStream stream)
        {
            byte[] receivedBuffer = new byte[BufferSize];
            do
            {
                await stream.ReadAsync(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            stream.Flush();
            return receivedBuffer;
        }

        public async Task WriteAsync(NetworkStream stream, byte[] data)
        {
            stream.Flush();
            do
            {
                await stream.WriteAsync(data, 0, data.Length);
            }
            while (stream.DataAvailable);
        }
    }
}
