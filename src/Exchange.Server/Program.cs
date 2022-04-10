using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols.Receivers;
using Exchange.Server.Routers;
using Exchange.System.Packages;
using System;
using System.Threading.Tasks;

namespace Exchange.Server
{
    internal sealed class Program
    {
        public const string Host = "127.0.0.1";
        public const int Port = 80;

        private static Router _router = new Router(Host, Port);
        
        private static async Task Main()
        {
            using (ClientReceiver receiver = ClientReceiver.Create(Host, Port))
            {
                receiver.Start();
                Console.WriteLine("Server started on ");
                while (true)
                {
                    Console.WriteLine("Server waiting requests");
                    var client = receiver.AcceptClient();
                    Console.WriteLine("Client was connected");


                    await Task.Delay(30);
                }
            }
        }

        private static async Task ProcessRequestAsync()
        {
            var requestContext = await _router.AcceptRequestAsync();

            if (requestContext.Request is Request requestImp)
            {
                Console.WriteLine("GET => {0}; Protection => {1}",
                    requestImp.Query,
                        requestContext.Protection.ToString());
                await ProcessRequestAsync(requestContext);
            }
            else
            {
                Console.WriteLine("I don't know how to handle this package!");
            }
        }

        private static async Task ProcessRequestAsync(RequestContext requestContext)
        {
            ControllerSelector controllerSelector = new ControllerSelector();
            Controller controller = controllerSelector.SelectController(requestContext.Request.Query);
            await controller.ProcessRequestAsync(requestContext);
        }
    }
}
