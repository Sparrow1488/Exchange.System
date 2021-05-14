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
        private Informator _informator = new Informator(SecurityData.EncryptType.None);

        public async Task<ResponsePackage> SendRequest(IPackage package)
        {
            RequestPackage = package;
            Connect();

            string jsonPackage = RequestPackage.ToJson();
            byte[] buffer = _networkHelper.Encoding.GetBytes(jsonPackage);

           await _networkHelper.WriteDataAsync(_stream, buffer);
            byte[] receivedBuffer = await _networkHelper.ReadDataAsync(_stream, 256);

            _stream.Close();
            string jsonResponse = _networkHelper.Encoding.GetString(receivedBuffer);
            throw new ArgumentException("а как же пробразование?");
        }
        private void Connect()
        {
            var client = new TcpClient();
            client.Connect(ConnectionInfo.HostName, ConnectionInfo.Port);
            _stream = client.GetStream();
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
    }
}
