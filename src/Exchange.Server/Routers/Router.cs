using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Receivers;
using Exchange.System.Enums;
using Exchange.System.Helpers;
using Exchange.System.Packages;
using ExchangeSystem.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public sealed class Router : IRouter
    {
        public Router(IConfiguration config)
        {
            var hostSection = config.GetSection("Router").GetSection("Hosting");
            var host = hostSection.GetSection("Host").Value;
            var port = int.Parse(hostSection.GetSection("Port").Value);
            _receiver = ClientReceiver.Create(host, port);
            _configuration = new RouterConfiguration();
        }

        public Router(string host, int port)
        {
            _receiver = ClientReceiver.Create(host, port);
            _configuration = new RouterConfiguration();
        }

        private NetworkChannel _networkChannel = new NetworkChannel();
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.All
        };
        private readonly IClientReceiver _receiver;
        private readonly RouterConfiguration _configuration;

        public void Start() =>
            _receiver.Start();

        public void Stop() { }

        public async Task<RequestContext> AcceptRequestAsync()
        {
            var client = _receiver.AcceptClient();
            Ex.ThrowIfTrue<ConnectionException>(!client.Connected, "Client is not connected");
            RequestContext context = default;

            var stream = client.GetStream();
            string requestInfoStringify = await _networkChannel.ReadAsync(stream);
            var requestInfo = JsonConvert.DeserializeObject<ProtocolProtectionInfo>(requestInfoStringify, _jsonSettings);
            var protocol = CreateProtocol(requestInfo.ProtectionType, client);
            await protocol.AcceptRequest();
            var request = protocol.GetRequest<Request>();
            context = RequestContext.ConfigureContext(context =>
                                context.SetRequest(request)
                                        .SetClient(client)
                                         .SetProtection(request.Protection))
                                          .SetProtocol(protocol);
            return context;
        }

        private NetworkProtocol CreateProtocol(ProtectionType protection, TcpClient tcpClient)
        {
            NetworkProtocol selectedProtocol = default;
            if(ProtectionType.Default.Equals(protection))
                selectedProtocol = new AdvancedDefaultProtocol(tcpClient);
            if (ProtectionType.AesRsa.Equals(protection))
                selectedProtocol = new AdvancedAesRsaProtocol(tcpClient);
            return selectedProtocol;
        }

        public IRouter Configure(Action<RouterConfiguration> config)
        {
            Ex.ThrowIfNull(config);
            config?.Invoke(_configuration);
            return this;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
