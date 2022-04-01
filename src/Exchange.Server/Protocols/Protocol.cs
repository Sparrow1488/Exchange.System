﻿using Exchange.System.Packages;
using Exchange.System.Protection;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public abstract class Protocol : IProtocol
    {
        public virtual EncryptType EncryptType { get; protected set; }
        public EncryptType GetProtocolEncryptType() => EncryptType;
        public abstract Task<Package> ReceivePackageAsync(TcpClient client);
        public abstract Task SendResponseAsync(TcpClient client, ResponsePackage response);
    }
}
