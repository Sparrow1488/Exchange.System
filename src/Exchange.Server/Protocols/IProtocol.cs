using Exchange.System.Packages;
using Exchange.System.Protection;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public interface IProtocol
    {
        Task<Package> ReceivePackageAsync(TcpClient client);
        EncryptType GetProtocolEncryptType();
        Task SendResponseAsync(TcpClient client, ResponsePackage response);
    }
}
