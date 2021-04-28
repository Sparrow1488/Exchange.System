using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Sendlers.Open
{
    public interface IRequestSendler
    {
        string SendRequest(IPackage package);
    }
}
