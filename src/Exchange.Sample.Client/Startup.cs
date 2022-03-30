using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

        public void Run()
        {
            _logger.LogInformation($"{nameof(Startup)} running");
        }
    }
}
