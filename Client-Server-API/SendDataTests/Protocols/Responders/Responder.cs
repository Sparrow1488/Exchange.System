using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols.Responders
{
    public abstract class Responder
    {
        public TcpClient Client { get; }
        public abstract EncryptType EncryptType { get; }
        public abstract Task SendResponse(TcpClient toClient, object response);
    }
}
