using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class NewMessage : Message
    {
        public override RequestTypes RequestType { get; protected set; } = RequestTypes.NewMessage;
        public override void ProcessRequest(IPackage package, TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
