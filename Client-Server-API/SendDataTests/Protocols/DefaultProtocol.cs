using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;

namespace ExchangeServer.Protocols
{
    public class DefaultProtocol : Protocol
    {
        public override EncryptTypes EncryptType { get; protected set; } = EncryptTypes.None;

        public override Security GetPackageSecurity()
        {
            throw new NotImplementedException();
        }

        public override IPackage ReceivePackage(TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
