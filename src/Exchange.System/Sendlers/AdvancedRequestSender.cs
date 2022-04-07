using Exchange.System.Abstractions;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Exchange.System.Sendlers
{
    public class AdvancedRequestSender : RequestSender
    {
        public AdvancedRequestSender(ConnectionSettings settings) : base(settings) { }

        protected override ProtocolProtectionInfo ProtocolProtection => 
            new ProtocolProtectionInfo(ProtectionType.Default);

        public override async Task<Response> SendRequestAsync(Request request)
        {
            Request = request;
            await OpenConnectionAsync();
            await SendProtocolProtectionAsync();
            await Task.Delay(50);
            await SendRequestAsync();
            await GetResponseAsync<Response>();
            CloseConnection();
            return Response;
        }

        private async Task SendRequestAsync() =>
            await Channel.WriteAsync(NetworkStream, GetRequestInBytes());

        private async Task GetResponseAsync<TResponse>()
            where TResponse : Response
        {
            string jsonResponse = await Channel.ReadAsync(NetworkStream);
            Response = JsonConvert.DeserializeObject<TResponse>(jsonResponse, JsonSettings);
        }
    }
}
