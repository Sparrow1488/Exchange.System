using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Exceptions;
using Exchange.System.Packages;
using ExchangeSystem.Helpers;
using ExchangeSystem.Packages;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Exchange.Server.Controllers
{
    public abstract class Controller
    {
        public Controller() { }
        public Controller(RequestContext context = default) =>
            Context = context;

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
            Response response;
            try
            {
                string requestAction = GetRequestControllerAction();
                var method = GetType().GetMethod(requestAction);
                var methodParams = method.GetParameters();
                if (methodParams.Length == 0)
                    response = InvokeMethodWithoutParameter<T>(method);
                else if (methodParams.Length == 1)
                    response = InvokeMethodWithParameterFromBody<T>(method);
                else
                    throw new InvalidRequestException("This request cannot be handle, because processing controller have not any method what can use in this situation");
                // TODO : сделать асинхронную реализацию
            }
            catch (Exception ex)
            {
                response = HandleException(ex);
            }
            return (T)response ?? default;
        }

        private string GetRequestControllerAction()
        {
            string action;
            var queryChapters = Context.Request.Query.Split("/");
            if (queryChapters.Length > 1)
                action = queryChapters[1];
            else
                action = queryChapters[0];
            return action;
        }

        private TReturn InvokeMethodWithoutParameter<TReturn>(MethodInfo method) =>
            (TReturn)method.Invoke(this, new object[0]);

        private TReturn InvokeMethodWithParameterFromBody<TReturn>(MethodInfo method)
        {
            var bodyContent = Context.Request.GetBodyContent();
            return (TReturn)method.Invoke(this, new object[] { bodyContent });
        }

        private async Task SendResponseAsync()
        {
            Ex.ThrowIfTrue<ConnectionException>(() => !Context.Client.Connected, "Client was not connected!");
            Context.Protocol.SetResponse(Response);
            await Context.Protocol.SendResponse();
        }

        private Response HandleException(Exception ex)
        {
            ResponseReport report;
            if (ex?.InnerException is InvalidCastException)
            {
                report = new ResponseReport(ResponseStatus.Invalid.Message, ResponseStatus.Invalid);
            }
            else
            {
                report = new ResponseReport(ex?.InnerException?.Message, ResponseStatus.Bad);
            }
            return new Response<EmptyEntity>(report, new EmptyEntity());
        }
    }
}
