using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public abstract class Protocol : IProtocol
    {
        public abstract EncryptType EncryptType { get; protected set; }

        public abstract EncryptType GetPackageEncryptType();

        public abstract Task<IPackage> ReceivePackage(TcpClient client);
    }
}
