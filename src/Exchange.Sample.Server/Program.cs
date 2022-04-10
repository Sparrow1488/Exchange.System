using Exchange.Sample.Server.Abstractions;
using Exchange.Server.Routers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Server
{
    internal sealed class Program
    {
        private static async Task Main()
        {
            Console.Title = "Exchange.Sample.Server - Dependency Injection";
            var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                CreateLogger();
                services.AddSingleton<IRouter, Router>();
            }).UseSerilog()
              .Build();

            var startup = ActivatorUtilities.CreateInstance<Startup>(host.Services);
            await startup.StartAsync();
        }

        private static void CreateLogger() =>
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                            .WriteTo.Console().CreateLogger();
    }
}
