using Exchange.System.Helpers;
using Exchange.System.Packages;
using Exchange.System.Protection;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public class DefaultProtocol : Protocol
    {
        public override EncryptType EncryptType { get; protected set; } = EncryptType.None;
        private NetworkChannel _networkHelper = new NetworkChannel();
        private NetworkStream _stream;
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private ResponsePackage _response;
        private byte[] _responseData;

        public override async Task<Package> ReceivePackageAsync(TcpClient client)
        {
            _stream = client.GetStream();
            return await ReceiveRequestAsync();
        }

        private async Task<Package> ReceiveRequestAsync()
        {
            var jsonPackage = await _networkHelper.ReadAsync(_stream);
            return JsonConvert.DeserializeObject<Package>(jsonPackage, _jsonSettings);
        }

        public override async Task SendResponseAsync(TcpClient client, ResponsePackage response)
        {
            if (response == null)
                throw new ArgumentNullException($"Переданный {nameof(response)} является null");
            _stream = client.GetStream();
            _response = response;
            PrepareResponseData();
            await SendResponseAsync();
        }

        private void PrepareResponseData() =>
            _responseData = _networkHelper.Encoding.GetBytes(_response.ToJson());

        private async Task SendResponseAsync() =>
            await _networkHelper.WriteAsync(_stream, _responseData);
    }
}
