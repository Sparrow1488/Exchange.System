using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class Authorization : Controller
    {
        public override RequestTypes RequestType { get; protected set; } = RequestTypes.Authorization;

        public override void ProcessRequest(IPackage package, TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
