using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Client
{
    internal sealed class Program
    {
        private static async Task Main()
        {
            Console.Title = "Exchange.Sample.Client";
            Log.Logger = CreateLogger();
            Log.Information("Client started");
            var hosting = Host.CreateDefaultBuilder().ConfigureServices((context, services) => {
                services.AddTransient<Startup>();
            }).UseSerilog().Build();
            var startupPoint = ActivatorUtilities.CreateInstance<Startup>(hosting.Services);
            await startupPoint.RunAsync();
            Log.Information("Client finished");
        }

        private static ILogger CreateLogger() =>
            new LoggerConfiguration().WriteTo.Console().MinimumLevel.Debug().CreateLogger();
    }
}
