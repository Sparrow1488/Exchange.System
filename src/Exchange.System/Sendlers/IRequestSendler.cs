using Exchange.System.Packages;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public interface IRequestSendler
    {
        Task<ResponsePackage> SendRequest(Package package);
        Task<Response> SendRequest(Request request);
    }
}
