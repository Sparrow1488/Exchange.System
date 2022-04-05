using Exchange.System.Enums;
using Exchange.System.Packages;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Protocols
{
    public class NewAesRsaProtocol : NetworkProtocol<Request, Response>
    {
        public NewAesRsaProtocol(TcpClient tcpClient) : base(tcpClient) { }

        public override ProtectionType Protection => ProtectionType.AesRsa;

        public override Task<Request> AcceptRequestAsync()
        {
            throw new global::System.NotImplementedException();
        }

        public override Task SendResponseAsync(Response response)
        {
            throw new global::System.NotImplementedException();
        }
    }
}
