using Exchange.System.Protection;

namespace Exchange.System.Requests.Packages
{
    public abstract class RequestInformator
    {
        public EncryptType EncryptType { get; protected set; }
    }
}
