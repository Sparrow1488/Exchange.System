using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Sendlers;
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
                if (response is Response<Guid> correctResponse)
                {
                    _logger.LogInformation("STATUS => " + correctResponse.Report.Status.ToString());
                    _logger.LogInformation($"MESSAGE => {correctResponse.Report.Message}; " +
                        $"GUID => {correctResponse.Content.ToString()}");
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
            request.Body = new Body<UserPassport>(new UserPassport("asd", "1234"));
            return request;
        }

        private async Task DelayAsync() => await Task.Delay(TimeSpan.FromMilliseconds(50));
    }
}
