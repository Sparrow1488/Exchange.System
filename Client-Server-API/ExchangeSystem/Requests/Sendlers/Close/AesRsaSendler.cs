using Encryptors.Aes;
using Encryptors.Rsa;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace ExchangeSystem.Requests.Sendlers.Close
{
    public class AesRsaSendler : EncryptRequestSendler
    {
        public AesRsaSendler(ConnectionSettings settings) : base(settings)
        {
            RsaEncryptor rsa = new RsaEncryptor();
            _privateKey = rsa.PrivateKey;
            _publicKey = rsa.PublicKey;
        }
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        private RSAParameters _serverKey;
        private NetworkStream _stream;
        private byte[] _readySecretPackage;

        public override string SendRequest(IPackage package)
        {
            ConnectToServer();
            SendRequestInformation();
            ReceiveServerPublicKey();
            EncryptAesRsaPackage(package);
            PrepareSecretPackage();
            SendSecretPackageSize();
            SendSecretPackage();

            throw new ArgumentNullException("Where response from server ???????????????????");
        }
        private void ConnectToServer()
        {
            var client = new TcpClient();
            client.Connect(ConnectionSettings.HostName, ConnectionSettings.Port);
            _stream = client.GetStream();
        }
        private void SendRequestInformation()
        {
            PrepareRequestInformation();
            string requestJson = Serialize(_requestInfo);
            byte[] requestInfoBuffer = Encoding.UTF32.GetBytes(requestJson);
            WriteData(ref _stream, requestInfoBuffer);
        }
        private void ReceiveServerPublicKey()
        {
            byte[] publicServerRsa = ReadData(ref _stream, 2100);
            _serverKey = DecodeServerRsa(publicServerRsa);
        }
        private void EncryptAesRsaPackage(IPackage package)
        {
            AesEncryptor aes = new AesEncryptor();
            RsaEncryptor rsa = new RsaEncryptor();
            byte[] aesEncryptPackage = aes.EncryptString(package.ToJson());
            string encryptBase64Package = Convert.ToBase64String(aesEncryptPackage);

            byte[] encryptAesKey = rsa.Encrypt(aes.Key, _serverKey);
            byte[] encryptAesIV = rsa.Encrypt(aes.IV, _serverKey);
            string encryptAesKeyAsBase64 = Convert.ToBase64String(encryptAesKey);
            string encryptAesIVAsBase64 = Convert.ToBase64String(encryptAesIV);

            string clientXmlKey = new RsaConverter().AsXML(_publicKey);
            Security security = new AesRsaSecurity(clientXmlKey, string.Empty, encryptAesKeyAsBase64, encryptAesIVAsBase64);
            SecretPackage = new ProtectedPackage(encryptBase64Package, security);
        }
        private void SendSecretPackageSize()
        {
            string size = _readySecretPackage.Length.ToString();
            byte[] secretPackageSize = Encoding.UTF32.GetBytes(size);
            WriteData(ref _stream, secretPackageSize);
        }
        private RSAParameters DecodeServerRsa(byte[] serverRsaBuffer)
        {
            string jsonServerRsa = Encoding.UTF32.GetString(serverRsaBuffer);
            RsaConverter converter = new RsaConverter();
             
            return converter.AsParameters(jsonServerRsa);
        }
        private void PrepareRequestInformation()
        {
            _requestInfo = new Informator(EncryptTypes.AesRsa);
        }
        private void SendSecretPackage()
        {
            WriteData(ref _stream, _readySecretPackage);
        }
        private void PrepareSecretPackage()
        {
            string jsonSecretPackage = SecretPackage.ToJson();
            _readySecretPackage = Encoding.UTF32.GetBytes(jsonSecretPackage);
        }
        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
