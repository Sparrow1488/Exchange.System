using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public class DefaultProtocol : Protocol
    {
        public override EncryptType EncryptType { get; protected set; } = EncryptType.None;
        private NetworkHelper _networkHelper = new NetworkHelper();
        private NetworkStream _stream;

        private ResponsePackage _response;
        private byte[] _responseData;

        public override async Task<IPackage> ReceivePackageAsync(TcpClient client)
        {
            _stream = client.GetStream();
            return await ReceiveRequest();
        }
        private async Task<IPackage> ReceiveRequest()
        {
            var receivedRequest = await _networkHelper.ReadDataAsync(_stream, 25000000);
            var jsonPackage = _networkHelper.Encoding.GetString(receivedRequest);
            IPackage receivedPack = (IPackage)JsonConvert.DeserializeObject(jsonPackage, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            return receivedPack;
        }

        public override async Task SendResponseAsync(TcpClient client, ResponsePackage response)
        {
            _stream = client.GetStream();
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
