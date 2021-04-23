using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.SecurityData
{
    public class AesRsaSecurity : Security
    {
        public AesRsaSecurity(string rsaPublic, string rsaPrivate, string aesKey, string aesIV)
        {
            RsaPublicKey = rsaPublic;
            RsaPrivateKey = rsaPrivate;
            AesIV = aesIV;
            AesKey = aesKey;
        }
        public string RsaPrivateKey { get; }
        public string RsaPublicKey { get; }
        public string AesKey { get; }
        public string AesIV { get; }
        public override EncryptTypes EncryptType { get; } = EncryptTypes.AesRsa;
    }
}
