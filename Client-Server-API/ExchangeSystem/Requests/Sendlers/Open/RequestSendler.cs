using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeSystem.Requests.Sendlers.Open
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
        private Informator _informator = new Informator(SecurityData.EncryptType.None);
        private byte[] _requestData;

        public async Task<ResponsePackage> SendRequest(IPackage package)
        {
            RequestPackage = package;
            Connect();
            SendInformator();
            PrepareRequestPackage();
            SendPackageSize();
            await SendRequest();

            byte[] receivedBuffer = await _networkHelper.ReadDataAsync(_stream, 256);

            _stream.Close();
            string jsonResponse = _networkHelper.Encoding.GetString(receivedBuffer);
            throw new ArgumentException("а как же прeобразование?");
        }
        private void Connect()
        {
            _client = new TcpClient();
            _client.Connect(ConnectionInfo.HostName, ConnectionInfo.Port);
            _stream = _client.GetStream();
        }
        private async Task ReceiveReport()
        {
            await _networkHelper.ReadDataAsync(_stream, 64);
        }
        private void SendInformator()
        {
            var requestJsonInfo = JsonConvert.SerializeObject(_informator, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            var infoData = _networkHelper.Encoding.GetBytes(requestJsonInfo);
            _networkHelper.WriteData(_stream, infoData);
        }
        private void PrepareRequestPackage()
        {
            string jsonPackage = RequestPackage.ToJson();
            _requestData = _networkHelper.Encoding.GetBytes(jsonPackage);
        }
        private void SendPackageSize()
        {
            string requestSize = _requestData.Length.ToString();
            var requestDataSize = _networkHelper.Encoding.GetBytes(requestSize);
            _networkHelper.WriteData(_stream, requestDataSize);
            //do { Task.Delay(150).Wait(); } while (_stream.Length > 0);
        }
        private async Task SendRequest()
        {
            await _networkHelper.WriteDataAsync(_stream, _requestData);
        }
    }
}
