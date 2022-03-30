using Exchange.Server.Controllers;
using Exchange.Server.Extensions;
using Exchange.Server.Primitives;
using Exchange.Server.Protocols.Receivers;
using Exchange.Server.Routers;
using Exchange.System.Packages.Primitives;
using System;
using System.Threading.Tasks;

namespace Exchange.Server
{
    internal sealed class Program
    {
        private Program() =>
            _router = new Router();

        public const string Host = "127.0.0.1";
        public const int Port = 80;

        private static IRouter _router;
        
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
            var requestContext = await _router.AcceptRequestAsync();

            if (requestContext.Content is Package requestPackageImp)
            {
                Console.WriteLine("Get => {0}; EncryptType => {1}",
                    requestPackageImp.RequestType.ToString(),
                        requestContext.EncryptType.ToString());
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
            Controller controller = controllerSelector.SelectController(
                                        requestContext.Content.As<Package>().RequestType.ToString());
            await controller.ProcessRequestAsync(requestContext);
        }
    }
}
