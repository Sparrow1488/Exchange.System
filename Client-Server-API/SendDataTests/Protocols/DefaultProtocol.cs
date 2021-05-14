using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols
{
    public class DefaultProtocol : Protocol
    {
        public override EncryptType EncryptType { get; protected set; } = EncryptType.None;
        private NetworkHelper _networkHelper = new NetworkHelper();
        private NetworkStream _stream;
        private int _requestDataSize;

        public override EncryptType GetPackageEncryptType()
        {
            return EncryptType;
        }
        public override async Task<IPackage> ReceivePackage(TcpClient client)
        {
            _stream = client.GetStream();
            await ReceiveRequestSize();
            return await ReceiveRequest();
        }
        private async Task ReceiveRequestSize()
        {
            var requestSizeData = await _networkHelper.ReadDataAsync(_stream, 1024);
            string requestSize = _networkHelper.Encoding.GetString(requestSizeData);
            bool canParse = Int32.TryParse(requestSize, out int successSize);
            if (!canParse)
                throw new Exception("Опять пришли каловые данные: не могу конвертнуть полученный размер запроса");
            _requestDataSize = successSize;
        }
        private async Task<IPackage> ReceiveRequest()
        {
            var receivedRequest = await _networkHelper.ReadDataAsync(_stream, _requestDataSize);
            var jsonPackage = _networkHelper.Encoding.GetString(receivedRequest);
            IPackage receivedPack = (IPackage)JsonConvert.DeserializeObject(jsonPackage, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            return receivedPack;
        }
        private async Task SendReport()
        {
            var report = _networkHelper.Encoding.GetBytes("OK");
            await _networkHelper.WriteDataAsync(_stream, report);
        }
    }
}
