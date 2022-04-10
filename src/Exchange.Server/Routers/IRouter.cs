using Exchange.Server.Primitives;
using System;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public interface IRouter : IDisposable
    {
        void Start();
        void Stop();
        Task<RequestContext> AcceptRequestAsync();
        IRouter Configure(Action<RouterConfiguration> config);
    }
}
