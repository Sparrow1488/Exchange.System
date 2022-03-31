using Exchange.System.Enums;
using Exchange.System.Helpers;
using Exchange.System.Packages;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public abstract class NetworkProtocol<TRequest, TResponse> : NetworkProtocol
        where TRequest : Request 
            where TResponse : Response
    {
        public NetworkProtocol(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
            NetworkChannel = new NetworkChannel();
            JsonSettings = new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public JsonSerializerSettings JsonSettings;
        public readonly TcpClient TcpClient;
        protected readonly NetworkChannel NetworkChannel;

        public abstract ProtectionType Protection { get; }
        public Response Response { get; protected set; }
        public Request Request { get; protected set; }

        public override Task AcceptRequest() => AcceptRequestAsync();
        public override Task SendResponse() => SendResponseAsync((TResponse)Response);
        public override T GetRequest<T>() => Request as T;
        public override T GetResponse<T>() => Response as T;

        public abstract Task<TRequest> AcceptRequestAsync();
        public abstract Task SendResponseAsync(TResponse response);
    }

    public abstract class NetworkProtocol
    {
        public abstract Task AcceptRequest();
        public abstract Task SendResponse();
        public abstract T GetRequest<T>() 
            where T : class;
        public abstract T GetResponse<T>() 
            where T : class;
    }
}
