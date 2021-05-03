using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public abstract class Controller
    {
        protected abstract Responder Responder { get; set; }
        protected abstract IResponderSelector ResponderSelector { get; set; }
        public abstract RequestTypes RequestType { get; }
        public abstract void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptTypes encryptType);
    }
}
