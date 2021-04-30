using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public abstract class Controller
    {
        public abstract RequestTypes RequestType { get; protected set; }
        public abstract void ProcessRequest(IPackage package, TcpClient client);
    }
}
