using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Security
{
    public class AesRsaSecurity : SecurityData
    {
        public AesRsaSecurity(string rsaPublic, string aesKey, string aesIV)
        {
            RsaPublicKey = rsaPublic;
            AesIV = aesIV;
            AesKey = aesKey;
        }
        public string RsaPublicKey { get; }
        public string AesKey { get; }
        public string AesIV { get; }
    }
}
