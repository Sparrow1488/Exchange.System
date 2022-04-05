using Exchange.System.Enums;
using Exchange.System.Helpers;
using Exchange.System.Packages;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public class NewRequestSendler : IRequestSendler
    {
        public NewRequestSendler(ConnectionSettings settings)
        {
            Connection = settings;
            _retryPolicy = Policy.Handle<JsonSerializationException>()
                            .Or<SocketException>().WaitAndRetryAsync(_maxRetries, 
                                sleepSuration => TimeSpan.FromMilliseconds(250));
        }

        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private Informator _informator;
        private NetworkChannel _netChannel;
        private JsonSerializerSettings _jsonSettings;
        private AsyncRetryPolicy _retryPolicy;
        private readonly int _maxRetries = 3;

        public Request Request { get; private set; }
        public Response Response { get; private set; }
        public ConnectionSettings Connection { get; private set; }

        /// <summary>
        /// This method not supported yet
        /// </summary>
        /// <exception cref="global::System.NotImplementedException"></exception>
        public Task<ResponsePackage> SendRequest(Package package) =>
            throw new global::System.NotImplementedException();

        public async Task<Response> SendRequestAsync(Request request)
        {
            InitProperties();
            await OpenConnectionAsync();
            GetNetworkStream();
            CreateProtocolInfoInfo();
            await SendProtocolInfoAsync();
            await Task.Delay(50);
            await SendRequestAsync<Request>(request);
            await GetResponseAsync<Response>();
            await CloseConnectionAndStreamAsync();
            return Response;
        }

        public async Task<Response> SendRetryRequestAsync(Request request) =>
            await _retryPolicy.ExecuteAsync<Response>(() => SendRequestAsync(request));
            
        private void InitProperties()
        {
            _netChannel = new NetworkChannel();
            _jsonSettings = new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        private async Task OpenConnectionAsync()
        {
            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(Connection.HostName, Connection.Port);
        }

        private void GetNetworkStream() =>
            _stream = _tcpClient.GetStream();

        private void CreateProtocolInfoInfo() =>
            _informator = new Informator(ProtectionType.Default);

        private async Task SendProtocolInfoAsync()
        {
            string requestJsonInfo = JsonConvert.SerializeObject(_informator, _jsonSettings);
            byte[] infoData = _netChannel.Encoding.GetBytes(requestJsonInfo);
            await _netChannel.WriteAsync(_stream, infoData);
        }

        private async Task SendRequestAsync<TRequest>(TRequest request)
        {
            string jsonPackage = JsonConvert.SerializeObject(request, _jsonSettings);
            byte[] requestData = _netChannel.Encoding.GetBytes(jsonPackage);
            await _netChannel.WriteAsync(_stream, requestData);
        }

        private async Task GetResponseAsync<TResponse>()
            where TResponse : Response
        {
            string jsonResponse = await _netChannel.ReadAsync(_stream);
            Response = JsonConvert.DeserializeObject<TResponse>(jsonResponse, _jsonSettings);
        }

        private async Task CloseConnectionAndStreamAsync()
        {
            _tcpClient.Close();
            await _stream.DisposeAsync();
        }
    }
}
