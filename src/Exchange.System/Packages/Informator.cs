using Exchange.System.Protection;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class Informator : RequestInformator
    {
        [JsonConstructor]
        public Informator(EncryptType encryptType) : base(encryptType) { }
    }
}
