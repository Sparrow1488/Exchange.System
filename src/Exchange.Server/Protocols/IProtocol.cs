using Exchange.System.Packages;
using Exchange.System.Protection;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public interface IProtocol
    {
        Task<Package> ReceivePackageAsync(TcpClient client);
        /// <summary>
        /// Используйте этот метод после метода "ReceivePackage()". 
        /// </summary>
        /// <returns>Null, если у пакета отсутсвует защита</returns>
        EncryptType GetProtocolEncryptType();
        Task SendResponseAsync(TcpClient client, ResponsePackage response);
    }
}
