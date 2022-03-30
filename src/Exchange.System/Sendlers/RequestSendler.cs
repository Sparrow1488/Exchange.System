using Exchange.System.Helpers;
using Exchange.System.Packages;
using Exchange.System.Packages.Primitives;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public class RequestSendler : IRequestSendler
    {
        public RequestSendler(ConnectionSettings settings)
        {
            ConnectionInfo = settings;
        }

        public IPackage RequestPackage { get; private set; }
        public ConnectionSettings ConnectionInfo { get; }
        private NetworkChannel _networkHelper = new NetworkChannel(180000);
        private NetworkStream _stream;
        private TcpClient _client;
        private Informator _informator = new Informator(Protection.EncryptType.None);
        private byte[] _requestData;
        private ResponsePackage _responsePackage;
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public async Task<ResponsePackage> SendRequest(IPackage package)
        {
            RequestPackage = package;
            Connect();
            await SendInformator();
            await Task.Delay(150);
            PrepareRequestPackage();
            await SendRequest();

            await ReceiveResponse();
            if (_responsePackage == null)
                throw new ArgumentNullException("Ответ не является валидным!");
            return _responsePackage;
        }

        private void Connect()
        {
            _client = new TcpClient();
            _client.Connect(ConnectionInfo.HostName, ConnectionInfo.Port);
            _stream = _client.GetStream();
        }

        private async Task SendInformator()
        {
            var requestJsonInfo = JsonConvert.SerializeObject(_informator, _jsonSettings);
            var infoData = _networkHelper.Encoding.GetBytes(requestJsonInfo);
            await _networkHelper.WriteAsync(_stream, infoData);
        }

        private void PrepareRequestPackage()
        {
            string jsonPackage = RequestPackage.ToJson();
            _requestData = _networkHelper.Encoding.GetBytes(jsonPackage);
        }

        private async Task SendRequest()
        {
            await _networkHelper.WriteAsync(_stream, _requestData);
        }

        private async Task ReceiveResponse()
        {
            //byte[] receivedBuffer = await _networkHelper.ReadAsync(_stream);
            //string jsonResponse = _networkHelper.Encoding.GetString(receivedBuffer);
            string jsonResponse = await _networkHelper.ReadAsync(_stream);
            _responsePackage = JsonConvert.DeserializeObject<ResponsePackage>(jsonResponse, _jsonSettings);
        }
    }
}
