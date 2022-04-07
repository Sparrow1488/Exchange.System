using Encryptors;
using Exchange.System.Abstractions;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Protection;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public class AdvancedAesRsaSendler : RequestSender
    {
        public AdvancedAesRsaSendler(ConnectionSettings settings) : base(settings) { }

        protected override ProtocolProtectionInfo ProtocolProtection => 
            new ProtocolProtectionInfo(ProtectionType.AesRsa);

        private readonly AesEncryptor _aesEncryptor = CreateAesEncryptor();
        private readonly RsaEncryptor _rsaEncryptor = CreateRsaEncryptor();
        private AesKeysStringify _aesKeysStringify;
        private byte[] _serverPublicKey;

        public override async Task<Response> SendRequestAsync(Request request)
        {
            Request = request;
            await OpenConnectionAsync();
            await SendProtocolProtectionAsync();
            await GetServerPublicKeyAsync();
            EncryptOwnAesKeys();
            await SendEncryptedAesKeysAsync();
            await GetEncryptedResponseAsync();
            return Response;
        }

        private static AesEncryptor CreateAesEncryptor()
        {
            var aes = new AesEncryptor();
            aes.GenerateKeysBag();
            return aes;
        }

        private static RsaEncryptor CreateRsaEncryptor()
        {
            var rsa = new RsaEncryptor();
            rsa.GenerateKeysBag();
            return rsa;
        }

        private async Task GetServerPublicKeyAsync() =>
            _serverPublicKey = await Channel.ReadDataAsync(NetworkStream);

        private void EncryptOwnAesKeys()
        {
            var rsa = new RsaEncryptor();
            var keys = _aesEncryptor.GetKeys();
            rsa.UseKeys(new RsaKeysBag(_serverPublicKey, new byte[0]));
            var encKey = _rsaEncryptor.Encrypt(keys.Key);
            var encIV = _rsaEncryptor.Encrypt(keys.IV);
            _aesKeysStringify = new AesKeysStringify(new AesKeysBag(encKey, encIV));
        }

        private async Task SendEncryptedAesKeysAsync()
        {
            string keysStringify = JsonConvert.SerializeObject(_aesKeysStringify, JsonSettings);
            var dataToSend = Encoding.UTF8.GetBytes(keysStringify);
            await Channel.WriteAsync(NetworkStream, dataToSend);
        }

        private async Task GetEncryptedResponseAsync()
        {
            var encryptedDataResponse = await Channel.ReadDataAsync(NetworkStream);
            var responseData = _aesEncryptor.Encrypt(encryptedDataResponse);
            var jsonResponse = Channel.Encoding.GetString(responseData);
            Response = JsonConvert.DeserializeObject<Response>(jsonResponse, JsonSettings);
        }
    }
}
