using Encryptors.Aes;
using Encryptors.Rsa;
using ExchangeServer.MVC.Exceptions;
using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace ExchangeServer.Protocols
{
    public class AesRsaProtocol : Protocol
    {
        private byte[] _aesKey;
        private byte[] _aesIV;
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        private TcpClient _client;
        private NetworkHelper _networkHelper = new NetworkHelper();
        public override EncryptTypes EncryptType { get; protected set; } = EncryptTypes.AesRsa;
        public override IPackage ReceivePackage(TcpClient client)
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
            byte[] publicRsa = Encoding.UTF32.GetBytes(publicXmlKey);
            _networkHelper.WriteData(ref stream, publicRsa);
            byte[] _futureSecretPackageSize = _networkHelper.ReadData(ref stream, 128);
            string secretPackageSize = Encoding.UTF32.GetString(_futureSecretPackageSize);
            byte[] bufferForSecretPackage = _networkHelper.ReadData(ref stream, Convert.ToInt32(secretPackageSize));
            string _protectedJsonPackage = Encoding.UTF32.GetString(bufferForSecretPackage);
            ProtectedPackage pack = (ProtectedPackage)JsonConvert.DeserializeObject(_protectedJsonPackage, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            var finalyPack = AesRsaReceiver(pack);
            return finalyPack;
        }

        private IPackage AesRsaReceiver(ProtectedPackage protectedPackage)
        {
            if (protectedPackage?.Security?.EncryptType != EncryptTypes.AesRsa)
                throw new ProtocolTypeException();

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
    }
}
