using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Sendlers.Close
{
    public class AesRsaSendler : EncryptRequestSendler
    {
        public AesRsaSendler(ConnectionSettings settings) : base(settings)
        {
            
        }
    }
}
