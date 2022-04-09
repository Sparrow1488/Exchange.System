using Exchange.Sample.Client.Abstractions;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Senders;
using ExchangeSystem.Helpers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Exchange.Sample.Client.Services
{
    public class ProfileManageService : IProfileManageService
    {
        public ProfileManageService(
            ILogger<ProfileManageService> logger,
            ConnectionSettings connection)
        {
            _logger = logger;
            _connection = connection;
            _sender = new AdvancedRequestSender(_connection);
        }

        private readonly ILogger<ProfileManageService> _logger;
        private readonly ConnectionSettings _connection;
        private IRequestSender<Request, Response> _sender;

        public async Task<Profile> GetUserProfileAsync(int userId)
        {
            var response = await _sender.SendRetryRequestAsync(CreateProfileGetRequest(userId));
            var responseWithProfile = response as Response<Profile>;
            Ex.ThrowIfNull(responseWithProfile);
            return responseWithProfile.Content;
        }

        private Request<int> CreateProfileGetRequest(int userId)
        {
            _logger.LogInformation("GET => Profile/Get");
            return new Request<int>("Profile/Get", ProtectionType.Default)
            {
                Body = new Body<int>(userId)
            };
        }
    }
}
