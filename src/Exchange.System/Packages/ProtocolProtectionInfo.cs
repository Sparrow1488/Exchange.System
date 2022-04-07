using Exchange.System.Enums;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class ProtocolProtectionInfo
    {
        [JsonConstructor]
        public ProtocolProtectionInfo(ProtectionType protection) =>
            ProtectionType = protection;

        [JsonProperty] public ProtectionType ProtectionType { get; protected set; }
    }
}
