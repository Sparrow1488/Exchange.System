using Exchange.System.Enums;
using Exchange.System.Packages;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    internal class AdvancedDefaultProtocol : NetworkProtocol<Request, Response>, IAsyncDisposable
    {
        public AdvancedDefaultProtocol(TcpClient tcpClient) : base(tcpClient) { }

        public override ProtectionType Protection => ProtectionType.Default;
        private NetworkStream _stream => TcpClient.GetStream();

        public override async Task<Request> AcceptRequestAsync()
        {
            var jsonPackage = await NetworkChannel.ReadAsync(_stream);
            Request = JsonConvert.DeserializeObject<Request>(jsonPackage, JsonSettings);
            return Request;
        }

        public override async Task SendResponseAsync(Response response)
        {
            Response = response;
            string responseStringify = JsonConvert.SerializeObject(Response, JsonSettings);
            byte[] responseData = NetworkChannel.Encoding.GetBytes(responseStringify);
            await NetworkChannel.WriteAsync(_stream, responseData);
        }

        public async ValueTask DisposeAsync()
        {
            await _stream.DisposeAsync();
            TcpClient.Close();
            TcpClient.Dispose();
        }
    }
}
