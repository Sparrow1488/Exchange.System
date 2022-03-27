using ExchangeSystem.Helpers;
using System;
using System.Net;
using System.Net.Sockets;

namespace Exchange.Server.Protocols.Receivers
{
    public class ClientReceiver : IClientReceiver
    {
        private ClientReceiver(IPAddress address, int port) =>
            _listener = new TcpListener(address, port);

        private TcpListener _listener;

        public static ClientReceiver Create(string hostName, int portToListen)
        {
            Ex.ThrowIfEmptyOrNull(hostName);
            Ex.ThrowIfTrue<ArgumentException>(() => 
                portToListen < 1, "Port to listen was less than zero");
            var addressParsedSuccess = IPAddress.TryParse(hostName, out IPAddress hostAddress);
            Ex.ThrowIfTrue<ArgumentException>(!addressParsedSuccess, "Couldn't parse IPAddres using input arguments!");
            return new ClientReceiver(hostAddress, portToListen);
        }

        public void Start() => _listener.Start();

        public TcpClient AcceptClient()
        {
            var client = _listener.AcceptTcpClient();
            return client;
        }
    }
}
