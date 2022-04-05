using Exchange.System.Enums;
using Exchange.System.Protection;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class RequestInformator
    {
        public RequestInformator(EncryptType encryptType) =>
            EncryptType = encryptType;

        [JsonConstructor]
        public RequestInformator(ProtectionType protection) =>
            ProtectionType = protection;

        public EncryptType EncryptType { get; protected set; }
        [JsonProperty]
        public ProtectionType ProtectionType { get; protected set; }
    }
}
