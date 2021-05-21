using ExchangeServer.MVC.Controllers;
using ExchangeServer.MVC.Models;
using ExchangeServer.MVC.Routers;
using ExchangeServer.Protocols.Receivers;
using ExchangeServer.SQLDataBase;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystemCore.Requests.Objects.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SendDataTests
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            Test12Context();
            ClientReceiver receiver = new ClientReceiver("127.0.0.1", 80);
            receiver.StartReceive();
            while (true)
            {
                try
                {
                    Console.WriteLine("Server waiting requests...");
                    var client = receiver.AcceptClient();
                    Console.WriteLine("Client was connected");

                    await ServerProcessing(client);
                }
                catch { PrintError("ERROR"); }
            }
        }

        private static async  Task ServerProcessing(TcpClient client)
        {
            Router router = new Router();
            var requestPackage = await router.IssueRequestAsync(client) as Package;
            var packageEncryptType = router.GetPackageEncryptType();
            Console.WriteLine("Received package has '{0}' request type and encrypt type '{1}'", requestPackage.RequestType, packageEncryptType);

            ControllerSelector controllerSelector = new ControllerSelector();
            Controller controller = controllerSelector.SelectController(requestPackage.RequestType);
            controller.ProcessRequest(client, requestPackage, packageEncryptType);

            Console.WriteLine(requestPackage.RequestObject);
            return;
        }
        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void Test12Context()
        {
            using (LettersDbContext context = new LettersDbContext())
            {
                var news = new Letter() { Text = "УРА ЭНТИТИ!!!", SenderId = 1, DateCreate = DateTime.Now, Sources = new List<Source>() { new Source() { SenderId = 1, Extension = ".png", DateCreate = DateTime.Now} } };
                context.Letters.Add(news);
                context.SaveChanges();
                var pas = context.Letters.FirstOrDefault();
                Console.WriteLine(pas);
            }
        }
    }
}
