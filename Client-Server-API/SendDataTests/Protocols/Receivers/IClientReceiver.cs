using System.Net.Sockets;

namespace ExchangeServer.Protocols.Receivers
{
    public interface IClientReceiver
    {
        TcpClient AcceptClient();
    }
}
