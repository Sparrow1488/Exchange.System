using Exchange.System.Protection;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class RequestInformator
    {
        [JsonConstructor]
        public RequestInformator(EncryptType encryptType) =>
            EncryptType = encryptType;

        public EncryptType EncryptType { get; protected set; }
    }
}
