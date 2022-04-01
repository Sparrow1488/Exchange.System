using Exchange.Sample.Client.Abstractions;
using Exchange.Sample.Client.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Sendlers;
using ExchangeSystem.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Client.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        public AuthorizationService(
            ILogger<AuthorizationService> logger, 
            ConnectionSettings connection,
            UserPassport passport)
        {
            Ex.ThrowIfNull(connection);
            Ex.ThrowIfNull(passport);
            _logger = logger;
            _connection = connection;
            _passport = passport;
            _context = new AuthorizationContext();
        }

        private readonly ILogger<AuthorizationService> _logger;
        private readonly ConnectionSettings _connection;
        private readonly UserPassport _passport;
        private readonly AuthorizationContext _context;

        public async Task AuthorizeAsync() =>
            await AuthorizeAsync(_passport.Login, _passport.Password);

        public async Task AuthorizeAsync(string login, string password)
        {
            var sendler = new NewRequestSendler(_connection);
            _logger.LogDebug("GET => " + "Authorization");
            var response = await sendler.SendRequestAsync(CreateAuthorizationRequest());
            _logger.LogInformation("STATUS => " + response.Report.Status.ToString());
            _logger.LogInformation($"MESSAGE => {response.Report.Message}");

            if (response.Report.Status.Equals(AuthorizationStatus.Success))
            {
                var correctResponse = response as Response<Guid>;
                Ex.ThrowIfNull(correctResponse);
                _logger.LogDebug("GUID => " + correctResponse.Content.ToString());
                _context.IsSuccess = true;
                _context.Token = correctResponse.Content;
            }
            else if (response.Report.Status.Equals(AuthorizationStatus.Failed))
            {
                _logger.LogWarning("Authorization Failed");
            }
            else
            {
                _logger.LogError("Error");
            }
        }

        public Guid GetToken() => _context.Token;
        public bool IsSuccess() => _context.IsSuccess;

        private Request CreateAuthorizationRequest()
        {
            _logger.LogDebug("Created authorization request");
            var request = new Request<UserPassport>("Authorization", ProtectionType.Default);
            request.Body = new Body<UserPassport>(_passport);
            return request;
        }
    }
}
