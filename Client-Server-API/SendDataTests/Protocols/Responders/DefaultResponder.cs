using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols.Responders
{
    public class DefaultResponder : Responder
    {
        public override EncryptType EncryptType => EncryptType.None;
        private NetworkStream _stream;
        private NetworkHelper _networkHelper = new NetworkHelper();
        private ResponsePackage _response;
        private byte[] _responseData;

        public override async Task SendResponse(TcpClient toClient, ResponsePackage response)
        {
            _stream = toClient.GetStream();
            _response = response;
            PrepareResponseData();
            await SendResponse();
        }
        private void PrepareResponseData()
        {
            var jsonResponse = _response.ToJson();
            _responseData = _networkHelper.Encoding.GetBytes(jsonResponse);
        }
        private async Task SendResponse()
        {
            await _networkHelper.WriteDataAsync(_stream, _responseData);
        }
    }
}
