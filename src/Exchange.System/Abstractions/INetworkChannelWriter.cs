using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Abstractions
{
    public interface INetworkChannelWriter
    {
        Task WriteAsync(NetworkStream stream, byte[] data);
    }
}
