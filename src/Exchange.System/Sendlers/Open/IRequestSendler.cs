using Exchange.System.Packages.Default;
using System.Threading.Tasks;

namespace Exchange.System.Requests.Sendlers.Open
{
    public interface IRequestSendler
    {
        Task<ResponsePackage> SendRequest(IPackage package);
    }
}
