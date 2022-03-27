using Exchange.Server.MVC.Controllers;
using Exchange.Server.MVC.Models;
using Exchange.Server.MVC.Routers;
using Exchange.Server.Protocols.Receivers;
using Exchange.Server.SQLDataBase;
using Exchange.System.Requests.Objects;
using Exchange.System.Requests.Objects.Entities;
using Exchange.System.Requests.Packages.Default;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
//Data Source=(local)\SQLEXPRESS;Initial Catalog=Exchange.SystemDb_1;Integrated Security=True
namespace SendDataTests
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            //AddLetterInDB();
            ClientReceiver receiver = new ClientReceiver("127.0.0.1", 80);
            receiver.Start();
            while (true)
            {
                //try
                //{
                    Console.WriteLine("Server waiting requests...");
                    var client = receiver.AcceptClient();
                    Console.WriteLine("Client was connected");

                    await ServerProcessing(client);
                    Task.Delay(150).Wait(); // давайте не будем перегружать ЦП
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
