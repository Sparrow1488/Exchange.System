using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.Protocols.Responders
{
    public abstract class Responder
    {
        public Responder(TcpClient client)
        {
            Client = client;
        }
        public TcpClient Client { get; }
        public abstract EncryptTypes EncryptType { get; }
        public abstract void SendResponse(object response);
    }
}
