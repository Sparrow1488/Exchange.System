using ExchangeSystem.Requests.Packages.Default;
using System.Threading.Tasks;

namespace ExchangeSystem.Requests.Sendlers.Open
{
    public interface IRequestSendler
    {
        Task<ResponsePackage> SendRequest(IPackage package);
    }
}
