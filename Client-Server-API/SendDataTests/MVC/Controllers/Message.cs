using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public abstract class Message : Controller
    {
        protected Message(TcpClient client) : base(client)
        {
        }
        public override abstract RequestTypes RequestType { get; }
        protected override abstract Responder Responder { get; set; }
        protected override abstract IResponderSelector ResponderSelector { get; set; }

        public override abstract void ProcessRequest(IPackage package, EncryptTypes encryptType);
    }
}
