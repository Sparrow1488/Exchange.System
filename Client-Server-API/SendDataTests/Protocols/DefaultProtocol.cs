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

        public override EncryptType GetPackageEncryptType()
        {
            return EncryptType;
        }

        public override async Task<IPackage> ReceivePackage(TcpClient client)
        {
            var stream = client.GetStream();
            var requestData = await _networkHelper.ReadDataAsync(stream, 1024);
            var jsonPackage = _networkHelper.Encoding.GetString(requestData);
            IPackage receivedPack = (IPackage)JsonConvert.DeserializeObject(jsonPackage, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            return receivedPack;
        }
    }
}
