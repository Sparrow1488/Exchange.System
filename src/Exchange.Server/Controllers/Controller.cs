using Exchange.Server .Exceptions.NetworkExceptions;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using System.Net.Sockets;
using Exchange.System.Enums;

namespace Exchange.Server .Controllers
{
    public abstract class Controller
    {
        protected abstract Protocol Protocol { get; set; }
        protected abstract IProtocolSelector ProtocolSelector { get; set; }
        public abstract RequestType RequestType { get; }
        public EncryptType EncryptType { get; protected set; }

        protected TcpClient Client;
        protected ResponsePackage Response;

        public abstract void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType);
        protected void SendResponse()
        {
            Protocol = ProtocolSelector.SelectProtocol(EncryptType);
            if (Client.Connected)
                Protocol.SendResponseAsync(Client, Response);
            else
                throw new ConnectionException("Клиент не был подключен");
        }
    }
}
