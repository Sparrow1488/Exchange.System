using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using System;

namespace ExchangeSystem.Requests.Sendlers.Close
{
    public abstract class EncryptRequestSendler : IEncryptRequestSendler
    {
        public EncryptRequestSendler(ConnectionSettings settings)
        {
            ConnectionSettings = settings;
        }

        public Security Security { get; }
        public ConnectionSettings ConnectionSettings { get; }
        public ProtectedPackage ProtectedPackage { get; protected set; }

        public string SendRequest(IPackage package)
        {
            throw new NotImplementedException();
        }
        public string ToEncryptJson()
        {
            throw new NotImplementedException();
        }
    }
}
