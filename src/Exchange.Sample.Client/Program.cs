using Exchange.Sample.Client.Abstractions;
using Exchange.Sample.Client.Services;
using Exchange.System.Entities;
using Exchange.System.Senders;
using Microsoft.Extensions.Configuration;
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
                services.AddTransient(provider => BuildConnectionSettings(provider));
                services.AddTransient(provider => BuildUserPassport(provider));
                services.AddTransient<IAuthorizationService, AuthorizationService>();
                services.AddTransient<IProfileManageService, ProfileManageService>();
                services.AddTransient<Startup>();
            }).UseSerilog().Build();

            var startupPoint = ActivatorUtilities.CreateInstance<Startup>(hosting.Services);
            await startupPoint.RunAsync();
            Log.Information("Client finished");
            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        private static ILogger CreateLogger() =>
            new LoggerConfiguration().WriteTo.Console().MinimumLevel.Debug().CreateLogger();


        private static ConnectionSettings BuildConnectionSettings(IServiceProvider provider) 
        {
            var config = provider.GetService<IConfiguration>();
            ConnectionSettings settings = new ConnectionSettings(config.GetValue<string>("EndpointHost"),
                                            config.GetValue<int>("EndpointPort"));
            return settings;
        }

        private static UserPassport BuildUserPassport(IServiceProvider provider)
        {
            var config = provider.GetService<IConfiguration>();
            var section = config.GetSection("Auth");
            var passport = new UserPassport(
                            section.GetValue<string>("Login"), 
                             section.GetValue<string>("Password"));
            return passport;
        }
    }
}
