using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols.Receivers;
using Exchange.Server.Routers;
using Exchange.System.Extensions;
using Exchange.System.Packages;
using System;
using System.Threading.Tasks;

namespace Exchange.Server
{
    internal sealed class Program
    {
        public const string Host = "127.0.0.1";
        public const int Port = 80;

        private static Router _router = new Router();
        
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

                    _router.AddInQueue(client); // задел на многопоточную обработку
                    await ProcessRouterQueueAsync();
                    await Task.Delay(150);
                }
            }
        }

        private static async Task ProcessRouterQueueAsync()
        {
            if(_router.GetQueueLength() > 0)
                await ProcessRequestByPackageTypeAsync();
            else
                Console.WriteLine("Queue is empty");
        }

        private static async Task ProcessRequestByPackageTypeAsync()
        {
            var requestContext = await _router.NewAcceptRequestAsync();

            if (requestContext.Request is Request requestImp)
            {
                Console.WriteLine("GET => {0}; EncryptType => {1}",
                    requestImp.Query,
                        requestContext.Protection.ToString());
                await NewProcessRequestAsync(requestContext);
            }
            else
            {
                Console.WriteLine("I don't know how to handle this package!");
            }
        }

        private static async Task NewProcessRequestAsync(RequestContext requestContext)
        {
            ControllerSelector controllerSelector = new ControllerSelector();
            Controller controller = controllerSelector.SelectController(requestContext.Request.Query);
            await controller.ProcessRequestAsync(requestContext);
        }

        private static async Task OldProcessRequestAsync(RequestContext requestContext)
        {
            ControllerSelector controllerSelector = new ControllerSelector();
            Controller controller = controllerSelector.SelectController(
                                        requestContext.Content.As<Package>().RequestType.ToString());
            await controller.ProcessRequestAsync(requestContext);
        }
    }
}
