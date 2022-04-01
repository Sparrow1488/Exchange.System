using Exchange.Server.Primitives;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public interface IRouter
    {
        void AddInQueue(TcpClient clientToProccess);
        int GetQueueLength();
        Task<RequestContext> AcceptRequestAsync();
    }
}
