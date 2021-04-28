using ExchangeSystem.SecurityData;

namespace ExchangeSystem.Requests.Packages
{
    public abstract class RequestInformator
    {
        public EncryptTypes EncryptType { get; protected set; }
    }
}
