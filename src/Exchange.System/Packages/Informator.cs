using Exchange.System.Enums;
using Exchange.System.Protection;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class Informator : RequestInformator
    {
        public Informator(EncryptType encryptType) : base(encryptType) { }
        [JsonConstructor]
        public Informator(ProtectionType protection) : base(protection) { }
    }
}
