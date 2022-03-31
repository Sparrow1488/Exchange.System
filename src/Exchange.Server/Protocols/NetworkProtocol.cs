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

        public abstract Task<TRequest> AcceptRequestAsync();
        public abstract Task SendResponseAsync(TResponse response);
    }

    public class NetworkProtocol
    {

    }
}
