using Newtonsoft.Json;

namespace ExchangeSystem.SecurityData
{
    public class AesRsaSecurity : Security
    {
        public AesRsaSecurity(string xmlPublicRsa, string xmlPrivateRsa, string aesBase64Key, string aesBase64IV)
        {
            RsaPublicKey = xmlPublicRsa;
            RsaPrivateKey = xmlPrivateRsa;
            AesIV = aesBase64IV;
            AesKey = aesBase64Key;
        }
        [JsonProperty("xmlPrivateRsa")]
        public string RsaPrivateKey { get; } = string.Empty;
        [JsonProperty("xmlPublicRsa")]
        public string RsaPublicKey { get; } = string.Empty;
        [JsonProperty("aesBase64Key")]
        public string AesKey { get; } = string.Empty;
        [JsonProperty("aesBase64IV")]
        public string AesIV { get; } = string.Empty;
        public override EncryptTypes EncryptType { get; } = EncryptTypes.AesRsa;
    }
}
