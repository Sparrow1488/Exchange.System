using ExchangeSystem.SecurityData;

namespace ExchangeSystem.Requests.Packages
{
    public class Informator : RequestInformator
    {
        public Informator(EncryptTypes encryptType)
        {
            EncryptType = encryptType;
        }
        
    }
}
