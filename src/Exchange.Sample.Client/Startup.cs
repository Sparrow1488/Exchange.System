using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Sendlers;
using ExchangeSystem.Helpers;
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
                _logger.LogInformation("STATUS => " + response.Report.Status.ToString());
                _logger.LogInformation($"MESSAGE => {response.Report.Message}");

                if (response.Report.Status.Equals(AuthorizationStatus.Success))
                {
                    var correctResponse = response as Response<Guid>;
                    Ex.ThrowIfNull(correctResponse);
                    _logger.LogInformation("GUID => " + correctResponse.Content.ToString());
                }
                else if (response.Report.Status.Equals(AuthorizationStatus.Failed))
                {
                    _logger.LogWarning("Authorization Failed");
                }
                else
                {
                    _logger.LogError("Error");
                }
                await DelayAsync();
            }
        }

        private ConnectionSettings CreateConnectionSettings() =>
            new ConnectionSettings(_config.GetValue<string>("EndpointHost"), 
                                    _config.GetValue<int>("EndpointPort"));

        private Request CreateAuthorizationRequest()
        {
            var request = new Request<UserPassport>("Authorization", ProtectionType.Default);
            request.Body = new Body<UserPassport>(new UserPassport("asfd", "1234"));
            return request;
        }

        private async Task DelayAsync() => await Task.Delay(TimeSpan.FromMilliseconds(50));
    }
}
