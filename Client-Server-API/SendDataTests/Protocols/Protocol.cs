using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public abstract class Protocol : IProtocol
    {
        public abstract EncryptType EncryptType { get; protected set; }

        public EncryptType GetProtocolEncryptType()
        {
            return EncryptType;
        }
        public abstract Task<IPackage> ReceivePackageAsync(TcpClient client);

        public abstract Task SendResponseAsync(TcpClient client, ResponsePackage response);
    }
}
