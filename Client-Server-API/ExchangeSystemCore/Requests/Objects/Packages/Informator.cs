using ExchangeSystem.SecurityData;

namespace ExchangeSystem.Requests.Packages
{
    public class Informator : RequestInformator
    {
        public Informator(EncryptType encryptType)
        {
            EncryptType = encryptType;
        }
        
    }
}
