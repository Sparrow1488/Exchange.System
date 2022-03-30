using Exchange.System.Packages;
using Exchange.System.Packages.Primitives;
using System;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public abstract class EncryptRequestSendler
    {
        public EncryptRequestSendler(ConnectionSettings settings)
        {
            ConnectionSettings = settings;
        }
        protected ConnectionSettings ConnectionSettings { get; }
        protected ProtectedPackage SecretPackage { get; set; }
        protected Informator _requestInfo;
        public abstract  Task<ResponsePackage> SendRequest(IPackage package);

        public string ToEncryptJson()
        {
            throw new NotImplementedException();
        }
    }
}
