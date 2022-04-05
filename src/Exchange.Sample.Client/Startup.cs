using Exchange.Sample.Client.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Client
{
    internal class Startup
    {
        public Startup(
            ILogger<Startup> logger, 
            IConfiguration config, 
            IAuthorizationService authorization)
        {
            _logger = logger;
            _config = config;
            _authorization = authorization;
        }

        private readonly ILogger<Startup> _logger;
        private readonly IConfiguration _config;
        private readonly IAuthorizationService _authorization;

        public async Task RunAsync()
        {
            _logger.LogInformation($"{nameof(Startup)} running");
            for (int i = 0; i < 25; i++)
            {
                await _authorization.AuthorizeAsync();
                if (_authorization.IsSuccess())
                {
                    var authToken = _authorization.GetToken();
                    _logger.LogInformation("Auth token => " + authToken.ToString());
                }
            }
        }
    }
}
