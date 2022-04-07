using Exchange.System.Packages;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public interface IRequestSender<TRequest, TResponse> : IRequestSender
    {
        Task<Response> SendRequestAsync(Request request);
        Task<Response> SendRetryRequestAsync(Request request);
    }

    public interface IRequestSender
    {

    }
}
