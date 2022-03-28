using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public interface IRouter
    {
        void AddInQueue(TcpClient clientToProccess);
        int GetQueueLength();
        Task<IPackage> ExtractRequestPackageAsync();
        EncryptType GetPackageEncryptType(); // TODO: refactoring - delete
    }
}
