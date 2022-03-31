using Newtonsoft.Json;

namespace Exchange.System.Enums
{
    public class ProtectionType
    {
        [JsonConstructor]
        private ProtectionType(string name)
        {
            Name = name;
        }

        [JsonProperty] public readonly string Name;

        public static readonly ProtectionType Default = new ProtectionType("Default");
        public static readonly ProtectionType AesRsa = new ProtectionType("AesRsa");
    }
}
