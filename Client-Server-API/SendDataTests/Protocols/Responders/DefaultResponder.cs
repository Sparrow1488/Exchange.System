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
        private NetworkHelper _networkHelper = new NetworkHelper();

        public override async Task SendResponse(TcpClient toClient, ResponsePackage response)
        {
            var stream = toClient.GetStream();
            var jsonResponse = response.ToJson();
            var responseData = _networkHelper.Encoding.GetBytes(jsonResponse);
            await _networkHelper.WriteDataAsync(stream, responseData);
        }
    }
}
