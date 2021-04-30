using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public abstract class Message : Controller
    {
        public override abstract RequestTypes RequestType { get; protected set; }
        public override abstract void ProcessRequest(IPackage package, TcpClient client);
    }
}
