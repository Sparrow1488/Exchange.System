using ExchangeServer.MVC.Controllers;
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


        //private static void TestUsersContext()
        //{
        //    using (AccountsDbContext context = new AccountsDbContext())
        //    {
        //        var newPas = new UserPassport("asd", "1234");
        //        context.Passports.Add(newPas) ;
        //        context.SaveChanges();
        //        var pas = context.Passports.FirstOrDefault();
        //        Console.WriteLine(pas);
        //    }
        //}
        private static void Test12Context()
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                var user = new User(new UserPassport("Sparrow", "asd") { AdminStatus = AdminStatus.Admin }) { Name = "Валентин", LastName = "Жма"};
                context.Users.Add(user);
                context.SaveChanges();
                var pas = context.Users.FirstOrDefault();
                Console.WriteLine(pas);
            }
        }
    }
}
