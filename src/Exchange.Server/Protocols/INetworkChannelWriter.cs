using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public interface INetworkChannelWriter
    {
        Task WriteAsync(NetworkStream stream, byte[] data);
    }
}
