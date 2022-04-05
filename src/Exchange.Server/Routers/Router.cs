using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols;
using Exchange.System.Enums;
using Exchange.System.Helpers;
using Exchange.System.Packages;
using ExchangeSystem.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public sealed class Router : IRouter
    {
        private Queue<TcpClient> _queue = new Queue<TcpClient>();

        private NetworkChannel _networkChannel = new NetworkChannel();
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.All
        };

        public void AddInQueue(TcpClient clientToProccess)
        {
            Ex.ThrowIfNull(clientToProccess);
            _queue.Enqueue(clientToProccess);
        }

        public async Task<RequestContext> AcceptRequestAsync()
        {
            var client = _queue.Dequeue();
            Ex.ThrowIfTrue<ConnectionException>(!client.Connected, "Client is not connected");
            RequestContext context = default;

            var stream = client.GetStream();
            string requestInfoStringify = await _networkChannel.ReadAsync(stream);
            var requestInfo = JsonConvert.DeserializeObject<RequestInformator>(requestInfoStringify, _jsonSettings);
            var protocol = CreateProtocol(requestInfo.ProtectionType, client);
            await protocol.AcceptRequest();
            var request = protocol.GetRequest<Request>();
            context = RequestContext.ConfigureContext(context =>
                                context.SetRequest(request)
                                        .SetClient(client)
                                            .SetProtection(request.Protection));
            return context;
        }

        public int GetQueueLength() => _queue.Count;

        private NetworkProtocol CreateProtocol(ProtectionType protection, TcpClient tcpClient)
        {
            NetworkProtocol selectedProtocol = default;
            if(protection.ToString() == ProtectionType.Default.ToString())
                selectedProtocol = new NewDefaultProtocol(tcpClient);
            if (protection == ProtectionType.AesRsa)
                throw new NotImplementedException();
            return selectedProtocol;
        }
    }
}
