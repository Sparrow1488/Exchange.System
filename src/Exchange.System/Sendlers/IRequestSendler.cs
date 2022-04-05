using Exchange.System.Packages;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public interface IRequestSendler
    {
        Task<ResponsePackage> SendRequest(Package package);
        Task<Response> SendRequestAsync(Request request);
        Task<Response> SendRetryRequestAsync(Request request);
    }
}
