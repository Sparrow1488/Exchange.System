using ExchangeSystem.Requests.Packages.Default;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Routers
{
    public interface IRouter
    {
        IPackage IssueRequest(TcpClient client);
    }
}
