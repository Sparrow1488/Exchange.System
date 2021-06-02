using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public interface INetworkChannelWriter
    {
        Task WriteAsync(NetworkStream stream, byte[] data);
    }
}
