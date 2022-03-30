using Exchange.System.Protection;

namespace Exchange.System.Packages
{
    public class Informator : RequestInformator
    {
        public Informator(EncryptType encryptType)
        {
            EncryptType = encryptType;
        }
    }
}
