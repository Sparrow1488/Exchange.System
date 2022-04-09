using Exchange.Sample.Client.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Sample.Client
{
    internal class Startup
    {
        public Startup(
            ILogger<Startup> logger, 
            IConfiguration config, 
            IAuthorizationService authorization,
            IProfileManageService profileService)
        {
            _logger = logger;
            _config = config;
            _authorization = authorization;
            _profileService = profileService;
        }

        private readonly ILogger<Startup> _logger;
        private readonly IConfiguration _config;
        private readonly IAuthorizationService _authorization;
        private readonly IProfileManageService _profileService;

        public async Task RunAsync()
        {
            _logger.LogInformation($"{nameof(Startup)} running");
            
            await _authorization.AuthorizeAsync();
            if (_authorization.IsSuccess())
            {
                var authToken = _authorization.GetToken();
                _logger.LogInformation("Auth token => " + authToken.ToString());
                Console.WriteLine();

                var profile = await _profileService.GetUserProfileAsync(1);
                _logger.LogInformation("Profile received success");
                _logger.LogInformation("OpenLogin => " + profile.OpenLogin);
                _logger.LogInformation("Description => " + profile.Description);
                _logger.LogInformation("Tags count => " + profile.Tags.Count());
            }
        }
    }
}
