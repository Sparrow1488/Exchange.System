using Encryptors.Aes;
using Encryptors.Rsa;
using Exchange.Server.Exceptions;
using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.System.Packages.Primitives;
using Exchange.System.Packages;
using Exchange.System.Protection;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public class AesRsaProtocol : Protocol
    {
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        private TcpClient _client;
        private EncryptType _ecnryptType;
        private RSAParameters _clientPublicKey;
        private NetworkStream _stream;
        private ResponsePackage _responsePackage;
        private string _jsonResponsePackage;
        private ProtectedPackage _protectedPackage;
        private int _responsePackageSize = 0;
        private byte[] _responseData;
        private NetworkChannel _networkChannel = new NetworkChannel(128);
        public override EncryptType EncryptType { get; protected set; } = EncryptType.AesRsa;

        public override async Task<IPackage> ReceivePackageAsync(TcpClient client)
        {
            if (!client.Connected)
                throw new ConnectionException();
            _client = client;
            var stream = client.GetStream();
            RsaEncryptor rsa = new RsaEncryptor();
            _publicKey = rsa.PublicKey;
            _privateKey = rsa.PrivateKey;
            RsaConverter converter = new RsaConverter();
            var publicXmlKey = converter.AsXML(rsa.PublicKey);
            byte[] publicRsa = new NetworkChannel().Encoding.GetBytes(publicXmlKey);
            await new NetworkChannel().WriteAsync(stream, publicRsa);
            stream.Flush();
            byte[] _futureSecretPackageSize = await _networkChannel.ReadDataAsync(stream);
            string secretPackageSize = _networkChannel.Encoding.GetString(_futureSecretPackageSize); //TODO: херня с получением длины
            bool can = Int32.TryParse(secretPackageSize, out int correctSecretPack);
            if (!can)
                throw new Exception("Пришла хуета, а не размер пакета");
            _networkChannel.BufferSize = correctSecretPack;
            byte[] bufferForSecretPackage = await _networkChannel.ReadDataAsync(stream);
            string _protectedJsonPackage = _networkChannel.Encoding.GetString(bufferForSecretPackage);
            ProtectedPackage pack = (ProtectedPackage)JsonConvert.DeserializeObject(_protectedJsonPackage, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            var finalyPack = AesRsaDecryptor(pack);
            return finalyPack;
        }

        public override async Task SendResponseAsync(TcpClient client, ResponsePackage response)
        {
            if (response == null)
                throw new NullReferenceException("Похоже, вы передали null при отправке ответа");
            _responsePackage = response;

            _stream = client.GetStream();
            await ReceiveClientKey();
            PrepareResponsePackage();
            EncryptAesRsaPackage(_responsePackage);
            PrepareData();
            Task.Delay(100).Wait(); // я не знаю почему, но без этого не работает корректная передача :/
            await SendResponseSize();
            Task.Delay(100).Wait();
            await SendResponseData();
        }

        private IPackage AesRsaDecryptor(ProtectedPackage protectedPackage)
        {
            if (protectedPackage?.Security?.EncryptType != EncryptType.AesRsa)
                throw new ProtocolTypeException();
            _ecnryptType = protectedPackage.Security.EncryptType;

            AesRsaSecurity aesRsa = protectedPackage.Security as AesRsaSecurity;
            RsaEncryptor rsa = new RsaEncryptor();
            byte[] encAesKey = Convert.FromBase64String(aesRsa.AesKey);
            byte[] encAesIV = Convert.FromBase64String(aesRsa.AesIV);

            RsaConverter converter = new RsaConverter();
            RSAParameters clientPublicKey = converter.AsParameters(aesRsa.RsaPublicKey);
            byte[] decryptAesKey = rsa.Decrypt(encAesKey, _privateKey);
            byte[] decryptAesIV = rsa.Decrypt(encAesIV, _privateKey);
            AesEncryptor newAesRsa = new AesEncryptor(decryptAesKey, decryptAesIV);

            string jsonPack = newAesRsa.DecryptString(Convert.FromBase64String(protectedPackage.SecretPackage));
            IPackage deryptPack = (IPackage)JsonConvert.DeserializeObject(jsonPack, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            return deryptPack;
        }

        #region FOR RESPONSE
        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        private async Task ReceiveClientKey()
        {
            _networkChannel.BufferSize = 2100;
            byte[] clientKeyData = await _networkChannel.ReadDataAsync(_stream);
            var xmlKey = new NetworkChannel().Encoding.GetString(clientKeyData);
            _clientPublicKey = new RsaConverter().AsParameters(xmlKey);
        }
        private void PrepareResponsePackage()
        {
            _jsonResponsePackage = _responsePackage.ToJson();
        }
        private async Task SendResponseSize()
        {
            _responsePackageSize = _responseData.Length;
            await new NetworkChannel().WriteAsync(_stream, new NetworkChannel().Encoding.GetBytes(_responsePackageSize.ToString()));
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
            _responseData = new NetworkChannel().Encoding.GetBytes(_protectedPackage.ToJson());
        }
        private async Task SendResponseData()
        {
            await new NetworkChannel().WriteAsync(_stream, _responseData);
        }
        #endregion

    }
}
