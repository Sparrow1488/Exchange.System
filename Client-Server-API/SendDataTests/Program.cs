using ExchangeServer.MVC.Controllers;
using ExchangeServer.MVC.Models;
using ExchangeServer.MVC.Routers;
using ExchangeServer.Protocols.Receivers;
using ExchangeServer.SQLDataBase;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SendDataTests
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
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
                    Task.Delay(150).Wait(); // давайте не будем перегружать ЦП
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
            using (PublicationsDbContext db = new PublicationsDbContext())
            {
                var all = db.Publications.Add(new Publication() { Title = "УРА, ПРИВЯЗКА РОБИТ)0)", Text = "РУССКИЕ ВПЕРЕД ОЛЕ ОЛЕ ОЛЕ ОЛЕЕЕЕЕЕЕЕ", SenderId = 2, DateCreate = DateTime.Now, Sources = new Source[] { new Source() { Extension = ".p22ng", SenderId = 2, DateCreate = DateTime.Now } } });
                //db.Letters.Add(new Letter() { Title = "1212", DateCreate = DateTime.Now, Sources = new Source[] { new Source() { Extension = ".png", SenderId = 2, DateCreate = DateTime.Now } } });
                db.SaveChanges();
            }
        }
    }
}
