using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;
using System.Text;

namespace ExchangeServer.Protocols.Responders
{
    public class AesRsaResponder : Responder
    {
        public override EncryptTypes EncryptType => EncryptTypes.AesRsa;
        private NetworkHelper _networkHelper = new NetworkHelper();

        public override void SendResponse(TcpClient toClient, object response)
        {
            var clientStream = toClient.GetStream();
            byte[] responseSize = Encoding.UTF32.GetBytes(Convert.ToString(228));
            _networkHelper.WriteData(ref clientStream, responseSize);
        }
    }
}
