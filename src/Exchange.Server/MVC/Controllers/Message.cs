using ExchangeServer.Protocols;
using ExchangeServer.Protocols.Selectors;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public abstract class Message : Controller
    {
        public override abstract RequestType RequestType { get; }
        protected abstract override Protocol Protocol { get; set; }
        protected abstract override IProtocolSelector ProtocolSelector { get; set; }

        public override abstract void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType);
    }
}
