using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;

namespace ExchangeServer.Protocols.Responders
{
    public class AesRsaResponder : Responder
    {
        public AesRsaResponder(TcpClient client) : base(client)
        {
        }

        public override EncryptTypes EncryptType => EncryptTypes.AesRsa;

        public override void SendResponse(object response)
        {
            throw new NotImplementedException();
        }
    }
}
