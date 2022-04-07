using Exchange.System.Packages;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public class NewAesRsaSendler : IRequestSender
    {
        public Task<Response> SendRequestAsync(Request request)
        {
            throw new global::System.NotImplementedException();
        }

        public Task<Response> SendRetryRequestAsync(Request request)
        {
            throw new global::System.NotImplementedException();
        }

        public Task<ResponsePackage> SendRequest(Package package) =>
            throw new global::System.NotImplementedException();
    }
}
