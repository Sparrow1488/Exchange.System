using Exchange.System.Packages;
using Exchange.System.Packages.Primitives;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public interface IRequestSendler
    {
        Task<ResponsePackage> SendRequest(IPackage package);
    }
}
