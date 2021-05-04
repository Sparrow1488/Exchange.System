using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.Protocols
{
    public interface IProtocol
    {
        IPackage ReceivePackage(TcpClient client);
        /// <summary>
        /// Используйте этот метод после метода "ReceivePackage()". 
        /// </summary>
        /// <returns>Null, если у пакета отсутсвует защита</returns>
        EncryptType GetPackageEncryptType();
    }
}
