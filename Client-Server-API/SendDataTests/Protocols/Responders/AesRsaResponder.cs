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

namespace ExchangeServer.Protocols.Responders
{
    public class AesRsaResponder : Responder
    {
        public override EncryptTypes EncryptType => EncryptTypes.AesRsa;
        private NetworkHelper _networkHelper = new NetworkHelper();
        private RSAParameters _clientPublicKey;
        private NetworkStream _stream;
        private ResponsePackage _responsePackage;
        private string _jsonResponsePackage;
        private ProtectedPackage _protectedPackage;
        private int _responsePackageSize = 0;
        private byte[] _responseData;

        public override void SendResponse(TcpClient toClient, object response)
        {
            if (response == null)
                throw new NullReferenceException("Похоже, вы передали null при отправке ответа");
            _responsePackage = (ResponsePackage)response; //ВРЕМЕННО

            _stream = toClient.GetStream();
            ReceiveClientKey();
            PrepareResponsePackage();
            EncryptAesRsaPackage(_responsePackage);
            PrepareData();
            SendResponseSize();
            SendResponseData();
        }
        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        private void ReceiveClientKey()
        {
            byte[] clientKeyData = _networkHelper.ReadData(ref _stream, 2100);
            var xmlKey = Encoding.UTF32.GetString(clientKeyData);
            _clientPublicKey = new RsaConverter().AsParameters(xmlKey);
        }
        private void PrepareResponsePackage()
        {
            _jsonResponsePackage = _responsePackage.ToJson();
        }
        private void SendResponseSize()
        {
            _responsePackageSize = _responseData.Length;
            _networkHelper.WriteData(ref _stream, Encoding.UTF32.GetBytes(_responsePackageSize.ToString()));
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
            _responseData = Encoding.UTF32.GetBytes(_protectedPackage.ToJson());
        }
        private void SendResponseData()
        {
            _networkHelper.WriteData(ref _stream, _responseData);
        }
    }
}
