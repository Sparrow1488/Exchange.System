using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.MVC.Routers
{
    public interface IRouter
    {
        Task<IPackage> IssueRequestAsync(TcpClient client);
        EncryptType GetPackageEncryptType();
    }
}
