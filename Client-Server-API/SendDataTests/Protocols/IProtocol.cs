using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public interface IProtocol
    {
        Task<IPackage> ReceivePackageAsync(TcpClient client);
        /// <summary>
        /// Используйте этот метод после метода "ReceivePackage()". 
        /// </summary>
        /// <returns>Null, если у пакета отсутсвует защита</returns>
        EncryptType GetProtocolEncryptType();
        Task SendResponseAsync(TcpClient client, ResponsePackage response);
    }
}
