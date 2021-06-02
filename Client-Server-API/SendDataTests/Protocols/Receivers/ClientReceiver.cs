using System;
using System.Net;
using System.Net.Sockets;

namespace ExchangeServer.Protocols.Receivers
{
    public class ClientReceiver : IClientReceiver
    {
        public ClientReceiver(string hostName, int portListen)
        {
            var successParse = IPAddress.TryParse(hostName, out IPAddress hostAddress);
            if (!successParse)
                throw new ArgumentException($"Не удалось превратить '{hostName}' в IPAddress");
            _listener = new TcpListener(hostAddress, portListen);
        }
        private TcpListener _listener;
        public void Start()
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
