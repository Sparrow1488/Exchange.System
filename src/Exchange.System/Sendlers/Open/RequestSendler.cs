using Exchange.System.Packages;
using Exchange.System.Packages.Default;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Requests.Sendlers.Open
{
    public class RequestSendler : IRequestSendler
    {
        public RequestSendler(ConnectionSettings settings)
        {
            ConnectionInfo = settings;
        }
        public IPackage RequestPackage { get; private set; }
        public ConnectionSettings ConnectionInfo { get; }
        private NetworkHelper _networkHelper = new NetworkHelper();
        private NetworkStream _stream;
        private TcpClient _client;
        private Informator _informator = new Informator(Protection.EncryptType.None);
        private byte[] _requestData;
        private ResponsePackage _responsePackage;

        public async Task<ResponsePackage> SendRequest(IPackage package)
        {
            RequestPackage = package;
            Connect();
            await SendInformator();
            Task.Delay(150).Wait();
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
            var requestJsonInfo = JsonConvert.SerializeObject(_informator, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            var infoData = _networkHelper.Encoding.GetBytes(requestJsonInfo);
            await _networkHelper.WriteDataAsync(_stream, infoData);
        }
        private void PrepareRequestPackage()
        {
            string jsonPackage = RequestPackage.ToJson();
            _requestData = _networkHelper.Encoding.GetBytes(jsonPackage);
        }
        private async Task SendRequest()
        {
            await _networkHelper.WriteDataAsync(_stream, _requestData);
        }
        private async Task ReceiveResponse()
        {
            byte[] receivedBuffer = await _networkHelper.ReadDataAsync(_stream, 18000000);
            string jsonResponse = _networkHelper.Encoding.GetString(receivedBuffer);
            _responsePackage = (ResponsePackage)JsonConvert.DeserializeObject(jsonResponse, typeof(ResponsePackage), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
        }
    }
}
