using Exchange.System.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.System.Helpers
{
    public class NetworkChannel : INetworkChannelWriter, INetworkChannelReader
    {
        public Encoding Encoding { get; } = Encoding.UTF8;
        public int BufferSize => _bufferSize;
        private int _bufferSize = 1024;

        public NetworkChannel(int bufferSize) =>
            _bufferSize = bufferSize;

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
            return CleanBytesArray(receivedBuffer);
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

        private byte[] CleanBytesArray(byte[] array)
        {
            int startEmptyArrayBytesIndex = array.Length - 1;
            for (int i = startEmptyArrayBytesIndex; i >= 0; i--)
            {
                if (array[i] == 0)
                    startEmptyArrayBytesIndex--;
                else break;
            }
            Array.Resize(ref array, startEmptyArrayBytesIndex + 1);
            return array;
        }
    }
}
