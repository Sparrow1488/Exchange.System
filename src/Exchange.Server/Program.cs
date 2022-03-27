using Exchange.Server.Controllers;
using Exchange.Server.Models;
using Exchange.Server.Protocols.Receivers;
using Exchange.Server.Routers;
using Exchange.Server.SQLDataBase;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages.Default;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server
{
    internal sealed class Program
    {
        private static async Task Main()
        {
            //AddLetterInDB();
            ClientReceiver receiver = ClientReceiver.Create("127.0.0.1", 80);
            receiver.Start();
            while (true)
            {
                //try
                //{
                    Console.WriteLine("Server waiting requests...");
                    var client = receiver.AcceptClient();
                    Console.WriteLine("Client was connected");

                    await ServerProcessing(client);
                    await Task.Delay(150); // давайте не будем перегружать ЦП
                //}
                //catch { PrintError("ERROR"); }
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
            Console.WriteLine("Was received request object witch has type of " + requestPackage.RequestObject);
            controller.ProcessRequest(client, requestPackage, packageEncryptType);

            return;
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void AddUserInDB()
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                var user = new User(new UserPassport("asd", "1234") { AdminStatus = AdminStatus.Admin}) { Name = "Валентин", LastName = "Геркулесович", ParentName = "Жмышен"};
                context.Users.Add(user);
                context.SaveChanges();
                var pas = context.Users.FirstOrDefault();
                Console.WriteLine(pas);
            }
        }

        private static void AddLetterInDB()
        {
            var model = new PublicationModel();
            var sources = new Source[] { new Source() { Extension = ".mp4", SenderId = 2, DateCreate = DateTime.Now }, new Source() { Extension = ".mp190", SenderId = 2, DateCreate = DateTime.Now } };
            //var res = model.Add(new Publication() { Text = "Добрейший вечерочек с двумя вложениями", Title = "Вечерочка", Sources = sources, DateCreate = DateTime.Now });
            var res = model.Add(new Publication() { DateCreate = DateTime.Now, Text = "Че", Title = "Капче", Type = NewsType.Important});
        }
    }
}
