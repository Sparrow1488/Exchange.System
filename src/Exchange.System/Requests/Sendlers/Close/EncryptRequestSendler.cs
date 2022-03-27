using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;
using Exchange.System.Requests.Packages.Protected;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Requests.Sendlers.Close
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
