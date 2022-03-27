using System.Net.Sockets;

namespace Exchange.Server.Protocols.Receivers
{
    public interface IClientReceiver
    {
        TcpClient AcceptClient();
        void Start();
    }
}
