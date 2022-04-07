using Exchange.System.Helpers;
using Exchange.System.Packages;
using Exchange.System.Sendlers;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.System.Abstractions
{
    public abstract class RequestSender : IRequestSender<Request, Response>
    {
        public RequestSender(ConnectionSettings settings)
        {
            Connection = settings;
            _retryPolicy = Policy.Handle<JsonSerializationException>()
                            .Or<SocketException>().WaitAndRetryAsync(_maxRetries,
                                sleepSuration => TimeSpan.FromMilliseconds(250));
            _tcpClient = new TcpClient();
            Channel = new NetworkChannel();
            JsonSettings = CreateJsonSerializationSettings();
        }

        protected readonly NetworkChannel Channel;
        protected readonly ConnectionSettings Connection;
        protected readonly JsonSerializerSettings JsonSettings;
        private readonly int _maxRetries = 3;
        private readonly AsyncRetryPolicy _retryPolicy;
        private TcpClient _tcpClient;

        protected TcpClient TcpClient => _tcpClient;
        protected NetworkStream NetworkStream => TcpClient.GetStream();
        protected abstract ProtocolProtectionInfo ProtocolProtection { get; }
        public Request Request { get; protected set; }
        public Response Response { get; protected set; }

        public abstract Task<Response> SendRequestAsync(Request request);
        public virtual async Task<Response> SendRetryRequestAsync(Request request) =>
            await _retryPolicy.ExecuteAsync(() => SendRequestAsync(request));

        protected async Task OpenConnectionAsync() =>
            await TcpClient.ConnectAsync(Connection.HostName, Connection.Port);

        protected string GetProtocolProtecStringify() =>
            JsonConvert.SerializeObject(ProtocolProtection, JsonSettings);

        protected byte[] GetProtocolProtecInBytes() =>
            Channel.Encoding.GetBytes(GetProtocolProtecStringify());

        protected string GetRequestStringify() =>
            JsonConvert.SerializeObject(Request, JsonSettings);

        protected byte[] GetRequestInBytes() =>
            Channel.Encoding.GetBytes(GetRequestStringify());

        protected void CloseConnection() =>
            TcpClient.Close();

        protected virtual JsonSerializerSettings CreateJsonSerializationSettings() =>
            new JsonSerializerSettings(){
                TypeNameHandling = TypeNameHandling.All };
    }
}
