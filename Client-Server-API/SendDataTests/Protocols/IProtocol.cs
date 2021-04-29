using ExchangeSystem.Requests.Packages.Default;
using System.Net.Sockets;

namespace ExchangeServer.Protocols
{
    public interface IProtocol
    {
        IPackage ReceivePackage(TcpClient client);
    }
}
