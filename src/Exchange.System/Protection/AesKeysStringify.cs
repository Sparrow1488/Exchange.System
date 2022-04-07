using Encryptors;
using Newtonsoft.Json;
using System;

namespace Exchange.System.Protection
{
    public class AesKeysStringify
    {
        public AesKeysStringify(AesKeysBag aesKeys)
        {
            _aesKeys = aesKeys;
            _keyStringy = KeyToBase64();
            _ivStringy = IVToBase64();
        }

        [JsonConstructor]
        internal AesKeysStringify(string keyStringy, string ivStringy)
        {
            _keyStringy = keyStringy;
            _ivStringy = ivStringy;
        }

        [JsonProperty] private string _keyStringy;
        [JsonProperty] private string _ivStringy;
        [JsonIgnore] private AesKeysBag _aesKeys;

        public string KeyToBase64() =>
            Convert.ToBase64String(_aesKeys.Key);

        public string IVToBase64() =>
            Convert.ToBase64String(_aesKeys.IV);

        public AesKeysBag FromBase64()
        {
            var key = Convert.FromBase64String(_keyStringy);
            var iv = Convert.FromBase64String(_ivStringy);
            return new AesKeysBag(key, iv);
        }
    }
}
