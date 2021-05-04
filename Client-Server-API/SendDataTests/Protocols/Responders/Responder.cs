using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.Protocols.Responders
{
    public abstract class Responder
    {
        public TcpClient Client { get; }
        public abstract EncryptType EncryptType { get; }
        public abstract void SendResponse(TcpClient toClient, object response);
    }
}
