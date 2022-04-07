using Exchange.System.Helpers;
using Exchange.System.Packages;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public class RequestSendler : IRequestSender
    {
        public RequestSendler(ConnectionSettings settings) =>
            ConnectionInfo = settings;

        public Package RequestPackage { get; private set; }
        public ConnectionSettings ConnectionInfo { get; }
        private NetworkChannel _networkHelper = new NetworkChannel(180000);
        private NetworkStream _stream;
        private TcpClient _client;
        private ProtocolProtectionInfo _informator;
        private byte[] _requestData;
        private ResponsePackage _responsePackage;
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public async Task<ResponsePackage> SendRequest(Package package)
        {
            RequestPackage = package;
            Connect();
            await SendInformatorAsync();
            await Task.Delay(150);
            PrepareRequest<Package>(RequestPackage);
            await SendRequestAsync();

            _responsePackage = await ReceiveResponseAsync<ResponsePackage>();
            if (_responsePackage == null)
                throw new ArgumentNullException("Ответ не является валидным!");
            return _responsePackage;
        }

        public Task<Response> SendRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }

        private void Connect()
        {
            _client = new TcpClient();
            _client.Connect(ConnectionInfo.HostName, ConnectionInfo.Port);
            _stream = _client.GetStream();
        }

        private async Task SendInformatorAsync()
        {
            var requestJsonInfo = JsonConvert.SerializeObject(_informator, _jsonSettings);
            var infoData = _networkHelper.Encoding.GetBytes(requestJsonInfo);
            await _networkHelper.WriteAsync(_stream, infoData);
        }

        private void PrepareRequest<T>(T request)
        {
            string jsonPackage = JsonConvert.SerializeObject(request, _jsonSettings);
            _requestData = _networkHelper.Encoding.GetBytes(jsonPackage);
        }

        private async Task SendRequestAsync()
        {
            await _networkHelper.WriteAsync(_stream, _requestData);
        }

        private async Task<T> ReceiveResponseAsync<T>()
        {
            string jsonResponse = await _networkHelper.ReadAsync(_stream);
            return JsonConvert.DeserializeObject<T>(jsonResponse, _jsonSettings);
        }

        public Task<Response> SendRetryRequestAsync(Request request) =>
            throw new NotImplementedException();
    }
}
