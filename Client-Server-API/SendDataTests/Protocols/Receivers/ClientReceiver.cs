using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols.Receivers
{
    public class ClientReceiver : IClientReceiver
    {
        public ClientReceiver(string hostName, int portListen)
        {
            _listener = new TcpListener(IPAddress.Parse(hostName), portListen);
        }
        private TcpListener _listener;
        public void StartReceive()
        {
            _listener.Start();
        }
        public TcpClient AcceptClient()
        {
            var client = _listener.AcceptTcpClient();
            return client;
        }
    }
}
