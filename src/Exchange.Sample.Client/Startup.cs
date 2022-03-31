using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
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
            for (int i = 0; i < 20; i++)
            {
                var connectionSettings = CreateConnectionSettings();
                var sendler = new NewRequestSendler(connectionSettings);
                _logger.LogInformation("GET => " + "Authorization");
                var response = await sendler.SendRequestAsync(CreateAuthorizationRequest());
                if (response is Response<ResponseReport> correctResponse)
                {
                    _logger.LogInformation("Success");
                    _logger.LogInformation($"MESSAGE => {correctResponse.Content.Message}");
                }
                else
                {
                    _logger.LogError("Error");
                }
                await DelayAsync();
            }
        }

        #region OLD

        public async Task RunOldAsync()
        {
            _logger.LogInformation($"{nameof(Startup)} OLD running");
            for (int i = 0; i < 20; i++)
            {
                var connectionSettings = CreateConnectionSettings();
                var sendler = new NewRequestSendler(connectionSettings);
                _logger.LogInformation("GET => " + ControllerType.Authorization.ToString());
                var responsePackage = await sendler.SendRequest(CreateAuthorizationPackage());
                if (responsePackage.ResponseData is ResponseReport report)
                {
                    _logger.LogInformation("Success");
                    _logger.LogInformation($"STATUS => {report.Status.StatusName}; MESSAGE => {report.Message}");
                }
                else
                {
                    _logger.LogError("Error");
                }
                await DelayAsync();
            }
        }
        #endregion

        private ConnectionSettings CreateConnectionSettings() =>
            new ConnectionSettings(_config.GetValue<string>("EndpointHost"), 
                                    _config.GetValue<int>("EndpointPort"));

        private Package CreateAuthorizationPackage()
        {
            var passport = new UserPassport("asd", "1234");
            var auth = new AuthorizationPackage(passport);
            _logger.LogInformation("Authorization package created");
            return auth;
        }

        private Request CreateAuthorizationRequest()
        {
            var request = new Request<UserPassport>("Authorization", ProtectionType.Default);
            request.Body = new RequestBody<UserPassport>(new UserPassport("asd", "1234"));
            return request;
        }

        private async Task DelayAsync() => await Task.Delay(TimeSpan.FromMilliseconds(50));
    }
}
