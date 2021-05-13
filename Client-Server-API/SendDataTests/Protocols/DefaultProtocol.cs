using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public class DefaultProtocol : Protocol
    {
        public override EncryptType EncryptType { get; protected set; } = EncryptType.None;

        public override EncryptType GetPackageEncryptType()
        {
            throw new NotImplementedException();
        }

        public override Task<IPackage> ReceivePackage(TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
