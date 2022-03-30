using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Packages;
using Exchange.System.Packages.Primitives;
using Exchange.System.Protection;
using ExchangeSystem.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public sealed class Router : IRouter
    {
        private Queue<TcpClient> _queue = new Queue<TcpClient>();

        private IProtocolSelector _protocolSelector = new ProtocolSelector();
        private NetworkChannel _networkChannel = new NetworkChannel();
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.All
        };
        private IProtocol _selectedProtocol;

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

            using (var stream = client.GetStream())
            {
                string requestInfoStringify = await _networkChannel.ReadAsync(stream);
                var requestInfo = JsonConvert.DeserializeObject<RequestInformator>(requestInfoStringify, _jsonSettings);
                _selectedProtocol = LookForProtocol(requestInfo.EncryptType);
                var requestPackage = await _selectedProtocol.ReceivePackageAsync(client) as Package;
                var encryptType = _selectedProtocol.GetProtocolEncryptType();
                context = RequestContext.ConfigureContext(context => 
                                    context.SetContent(requestPackage)
                                            .SetClient(client)
                                             .SetEncription(encryptType));
            }
            return context;
        }

        public int GetQueueLength() => _queue.Count;
        private IProtocol LookForProtocol(EncryptType encryptType) =>
            _protocolSelector.SelectProtocol(encryptType);
    }
}
