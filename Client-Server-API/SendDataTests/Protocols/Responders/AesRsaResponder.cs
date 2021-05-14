using Encryptors.Aes;
using Encryptors.Rsa;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeServer.Protocols.Responders
{
    public class AesRsaResponder : Responder
    {
        public override EncryptType EncryptType => EncryptType.AesRsa;
        private RSAParameters _clientPublicKey;
        private NetworkStream _stream;
        private ResponsePackage _responsePackage;
        private string _jsonResponsePackage;
        private ProtectedPackage _protectedPackage;
        private int _responsePackageSize = 0;
        private byte[] _responseData;

        public override async Task SendResponse(TcpClient toClient, object response)
        {
            if (response == null)
                throw new NullReferenceException("Похоже, вы передали null при отправке ответа");
            _responsePackage = (ResponsePackage)response; //ВРЕМЕННО

            _stream = toClient.GetStream();
            await ReceiveClientKey();
            PrepareResponsePackage();
            EncryptAesRsaPackage(_responsePackage);
            PrepareData();
            Task.Delay(100).Wait(); // я не знаю почему, но без этого не работает корректная передача :/
            await SendResponseSize();
            Task.Delay(100).Wait();
            await SendResponseData();
        }
        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        private async Task ReceiveClientKey()
        {
            byte[] clientKeyData = await new NetworkHelper().ReadDataAsync(_stream, 2100);
            var xmlKey = new NetworkHelper().Encoding.GetString(clientKeyData);
            _clientPublicKey = new RsaConverter().AsParameters(xmlKey);
        }
        private void PrepareResponsePackage()
        {
            _jsonResponsePackage = _responsePackage.ToJson();
        }
        private async Task SendResponseSize()
        {
            _responsePackageSize = _responseData.Length;
            await new NetworkHelper().WriteDataAsync(_stream, new NetworkHelper().Encoding.GetBytes(_responsePackageSize.ToString()));
        }
        private void EncryptAesRsaPackage(IPackage package)
        {
            AesEncryptor aes = new AesEncryptor();
            RsaEncryptor rsa = new RsaEncryptor();
            byte[] aesEncryptPackage = aes.EncryptString(package.ToJson());
            string encryptBase64Package = Convert.ToBase64String(aesEncryptPackage);

            byte[] encryptAesKey = rsa.Encrypt(aes.Key, _clientPublicKey);
            byte[] encryptAesIV = rsa.Encrypt(aes.IV, _clientPublicKey);
            string encryptAesKeyAsBase64 = Convert.ToBase64String(encryptAesKey);
            string encryptAesIVAsBase64 = Convert.ToBase64String(encryptAesIV);

            Security security = new AesRsaSecurity(string.Empty, string.Empty, encryptAesKeyAsBase64, encryptAesIVAsBase64);
            _protectedPackage = new ProtectedPackage(encryptBase64Package, security);
        }
        private void PrepareData()
        {
            _responseData = new NetworkHelper().Encoding.GetBytes(_protectedPackage.ToJson());
        }
        private async Task SendResponseData()
        {
            await new NetworkHelper().WriteDataAsync(_stream, _responseData);
        }
    }
}
