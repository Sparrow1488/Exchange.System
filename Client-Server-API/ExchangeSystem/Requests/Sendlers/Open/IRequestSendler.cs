using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Sendlers.Open
{
    public interface IRequestSendler
    {
        ResponsePackage SendRequest(IPackage package);
    }
}
