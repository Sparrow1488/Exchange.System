using ExchangeSystem.SecurityData;

namespace ExchangeSystem.Requests.Packages
{
    public abstract class RequestInformator
    {
        public EncryptType EncryptType { get; protected set; }
    }
}
