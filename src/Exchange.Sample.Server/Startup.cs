using Exchange.Sample.Server.Abstractions;
using Exchange.Server.Primitives;
using Exchange.Server.Routers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Server
{
    public class Startup : IStartableAsync
    {
        public Startup(
            ILogger<Startup> logger, 
            IRouter router)
        {
            _logger = logger;
            _router = router;
            _router.Configure(config => config.MultiProtocolsSupport());
        }

        private readonly ILogger<Startup> _logger;
        private readonly IRouter _router;

        public async Task StartAsync()
        {
            _logger.LogInformation("Exchange.Sample.Server started");

            _router.Start();
            using (_router)
            {
                await Forever(async () =>
                {
                    var requestContext = await AcceptRequestAsync();
                });
            }
            
            _logger.LogInformation("Exchange.Sample.Server finished");
        }

        private async Task Forever(Func<Task> action)
        {
            while (true)
            {
                await action?.Invoke();
            }
        }

        private async Task<RequestContext> AcceptRequestAsync()
        {
            var context = await _router.AcceptRequestAsync();
            _logger.LogInformation("Accept. Query => {0}; Protection => {1}", context.Request.Query, context.Protection.ToString());
            return context;
        }
    }
}
