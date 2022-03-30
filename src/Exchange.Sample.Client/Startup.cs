using Exchange.System.Entities;
using Exchange.System.Packages.Primitives;
using Exchange.System.Sendlers;
using ExchangeSystem.Packages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Client
{
    internal class Startup
    {
        public Startup(ILogger<Startup> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private readonly ILogger<Startup> _logger;
        private readonly IConfiguration _config;

        public async Task RunAsync()
        {
            _logger.LogInformation($"{nameof(Startup)} running");
            await DelayAsync();
            var connectionSettings = CreateConnectionSettings();
            var sendler = new RequestSendler(connectionSettings);
            var responsePackage = await sendler.SendRequest(CreateAuthorizationPackage());
            if (responsePackage.ResponseData is ResponseReport report) 
            {
                _logger.LogInformation("Get response report success");
                _logger.LogInformation($"STATUS => {report.Status.StatusName}; MESSAGE => {report.Message}");
            }
        }

        private ConnectionSettings CreateConnectionSettings() =>
            new ConnectionSettings(_config.GetValue<string>("EndpointHost"), 
                                    _config.GetValue<int>("EndpointPort"));

        private IPackage CreateAuthorizationPackage()
        {
            var passport = new UserPassport("asd", "1234");
            var auth = new Authorization(passport);
            _logger.LogInformation("Authorization package created");
            return auth;
        }

        private async Task DelayAsync() => await Task.Delay(TimeSpan.FromSeconds(3));

    }
}
