using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Extensions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Packages.Default;
using ExchangeSystem.Helpers;
using ExchangeSystem.Packages;
using System;
using System.Threading.Tasks;

namespace Exchange.Server.Controllers
{
    public abstract class Controller
    {
        internal Controller() { }

        public ResponsePackage Response { get; private set; }
        public RequestContext Context { get; private set; }

        public async Task ProcessRequestAsync(RequestContext context)
        {
            Context = context;
            Response = ExecuteRequestMethod<ResponsePackage>();
            await SendResponseAsync();
        }

        private T ExecuteRequestMethod<T>()
            where T : ResponsePackage
        {
            try
            {
                string requestMethodName = Context.Content.As<Package>().RequestType.ToString();
                var methodResponse = (T)GetType().GetMethod(requestMethodName).Invoke(this, null);
            }
            catch (Exception ex)
            {
                var report = new ResponseReport(ex.Message, false);
                Response = new ResponsePackage();
            }
        }
        
        private async Task SendResponseAsync()
        {
            Ex.ThrowIfTrue<ConnectionException>(() => !Context.Client.Connected, "Client was not connected!");
            var protocol = new ProtocolSelector().SelectProtocol(Context.EncryptType);
            await protocol.SendResponseAsync(Context.Client, Response);
        }
    }
}
