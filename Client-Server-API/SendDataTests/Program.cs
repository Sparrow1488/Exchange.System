using ExchangeServer.MVC.Controllers;
using ExchangeServer.MVC.Routers;
using ExchangeServer.Protocols.Receivers;
using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SendDataTests
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ClientReceiver receiver = new ClientReceiver("127.0.0.1", 80);
            receiver.StartReceive();
            while (true)
            {
                Console.WriteLine("Server waiting requests...");
                var client = receiver.AcceptClient();
                Console.WriteLine("Client was connected");

                Task.Factory.StartNew(() => ServerProcessing(client));
            }
        }
        private static void ServerProcessing(TcpClient client)
        {
            Router router = new Router();
            var requestPackage = router.IssueRequest(client) as Package;
            var packageEncryptType = router.GetPackageEncryptType();
            Console.WriteLine("Received package has '{0}' request type and encrypt type '{1}'", requestPackage.RequestType, packageEncryptType);

            ControllerSelector controllerSelector = new ControllerSelector();
            Controller controller = controllerSelector.SelectController(requestPackage.RequestType);
            controller.ProcessRequest(client, requestPackage, packageEncryptType);

            Console.WriteLine(requestPackage.RequestObject);
        }
     }
}
