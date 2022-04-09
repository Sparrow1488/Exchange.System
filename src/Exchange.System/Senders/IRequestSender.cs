using Exchange.System.Packages;
using System.Threading.Tasks;

namespace Exchange.System.Senders
{
    public interface IRequestSender<TRequest, TResponse> : IRequestSender
    {
        Task<Response> SendRequestAsync(Request request);
        Task<Response> SendRetryRequestAsync(Request request);
    }

    public interface IRequestSender
    {
        Task SendRequestAsync();
        Task SendRetryRequestAsync();
        void SetRequest<TRequest>(TRequest requestObj)
            where TRequest : class;
        TResponse GetResponse<TResponse>()
            where TResponse : class;
    }
}
