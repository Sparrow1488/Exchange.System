using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols;
using Exchange.System.Entities;
using Exchange.System.Packages;
using ExchangeSystem.Helpers;
using ExchangeSystem.Packages;
using System;
using System.Threading.Tasks;
using ResponseStatus = Exchange.System.Enums.ResponseStatus;

namespace Exchange.Server.Controllers
{
    public abstract class Controller
    {
        internal Controller() { }

        public Response Response { get; private set; }
        public RequestContext Context { get; private set; }

        public async Task ProcessRequestAsync(RequestContext context)
        {
            Context = context;
            Response = ExecuteRequestMethod<Response>();
            await SendResponseAsync();
        }

        private T ExecuteRequestMethod<T>()
            where T : Response
        {
            Response responsePack;
            try
            {
                string requestMethodName = Context.Request.Query;
                var method = GetType().GetMethod(requestMethodName);
                responsePack = (T)method.Invoke(this, new object[0]);
                if (false) 
                {
                    // method.ReturnType.Equals(typeof(Task))
                    // TODO : сделать асинхронную реализацию
                    var task = (Task)method.Invoke(this, new object[] { });
                } 
            }
            catch (Exception ex)
            {
                var report = new ResponseReport(ex?.InnerException?.Message, ResponseStatus.Bad);
                responsePack = new Response<EmptyEntity>(report, new EmptyEntity());
            }
            return (T)responsePack ?? default;
        }

        private async Task SendResponseAsync()
        {
            Ex.ThrowIfTrue<ConnectionException>(() => !Context.Client.Connected, "Client was not connected!");
            var protocol = new NewDefaultProtocol(Context.Client);
            await protocol.SendResponseAsync(Response);
        }
    }
}
