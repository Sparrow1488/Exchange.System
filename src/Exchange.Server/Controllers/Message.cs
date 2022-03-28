﻿using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using System.Net.Sockets;
using Exchange.System.Enums;

namespace Exchange.Server .Controllers
{
    public abstract class Message : Controller
    {
        public override abstract RequestType RequestType { get; }
        protected abstract override Protocol Protocol { get; set; }
        protected abstract override IProtocolSelector ProtocolSelector { get; set; }

        public override abstract void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType);
    }
}