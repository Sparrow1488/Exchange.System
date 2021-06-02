using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public interface INetworkChannelReader
    {
        Task<string> ReadAsync(NetworkStream stream);
        Task<byte[]> ReadDataAsync(NetworkStream stream);
    }
}
