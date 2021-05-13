using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols.Responders
{
    public class DefaultResponder : Responder
    {
        private TcpClient _client;
        public override EncryptType EncryptType => EncryptType.None;

        public override async Task SendResponse(TcpClient toClient, object response)
        {
            throw new NotImplementedException();
        }
    }
}
