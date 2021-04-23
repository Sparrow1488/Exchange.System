using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;

namespace ExchangeSystem.Requests.Packages.Protected
{
    public class ProtectedPackage : IProtectedPackage
    {
        public ProtectedPackage(IPackage package, Security security)
        {
            SecretPackage = package;
            Security = security;
        }
        [JsonProperty]
        public Security Security { get; }
        private IPackage SecretPackage { get; }
        public string SecretJson { get; }

        public string ToJson()
        {
            throw new System.NotImplementedException();
        }
    }
}
