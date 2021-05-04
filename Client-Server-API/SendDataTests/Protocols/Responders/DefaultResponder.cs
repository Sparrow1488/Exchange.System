using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;

namespace ExchangeServer.Protocols.Responders
{
    public class DefaultResponder : Responder
    {
        private TcpClient _client;
        public override EncryptType EncryptType => EncryptType.None;

        public override void SendResponse(TcpClient toClient, object response)
        {
            throw new NotImplementedException();
        }
    }
}
