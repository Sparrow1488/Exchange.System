using Exchange.System.Protection;
using Newtonsoft.Json;

namespace Exchange.System.Requests.Packages.Protected
{
    public class ProtectedPackage : IProtectedPackage
    {
        public ProtectedPackage(string secretPackage, Security security)
        {
            SecretPackage = secretPackage;
            Security = security;
        }
        [JsonProperty]
        public Security Security { get; }
        [JsonProperty]
        public string SecretPackage { get; } // json + encrypt + base64

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
