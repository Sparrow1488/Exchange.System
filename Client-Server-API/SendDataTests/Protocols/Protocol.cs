using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.Protocols
{
    public abstract class Protocol : IProtocol
    {
        public abstract EncryptTypes EncryptType { get; protected set; }
        public abstract IPackage ReceivePackage(TcpClient client);
    }
}
