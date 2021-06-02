using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeServer.Protocols;
using ExchangeServer.Protocols.Selectors;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
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
