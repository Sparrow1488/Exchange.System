using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;

namespace ExchangeServer.Protocols.Responders
{
    public class DefaultResponder : Responder
    {
        public DefaultResponder(TcpClient client) : base(client)
        {
            _client = client;
        }
        private TcpClient _client;
        public override EncryptTypes EncryptType => EncryptTypes.None;

        public override void SendResponse(object response)
        {
            throw new NotImplementedException();
        }
    }
}
