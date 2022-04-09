using Exchange.Sample.Client.Abstractions;
using Exchange.Sample.Client.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Senders;
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
        }

        private readonly ILogger<AuthorizationService> _logger;
        private readonly ConnectionSettings _connection;
        private readonly UserPassport _passport;
        private AuthorizationContext _context;
        private IRequestSender<Request, Response> _sender;

        public async Task AuthorizeAsync() =>
            await AuthorizeAsync(_passport.Login, _passport.Password);

        public async Task AuthorizeAsync(string login, string password)
        {
            _sender = new AdvancedRequestSender(_connection);
            _logger.LogDebug("GET => " + "Authorization");
            var response = await _sender.SendRetryRequestAsync(CreateAuthorizationRequest());

            if (IsAuthSuccess(response))
            {
                var correctResponse = response as Response<Guid>;
                Ex.ThrowIfNull(correctResponse);
                _context = CreateSuccessContext(correctResponse);
            }
            else if (IsAuthFailed(response))
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
            var request = new Request<HashedUserPassport>("Authorization/AuthorizationHashed", ProtectionType.Default);
            var hashedPassport = HashedUserPassport.CreateHashed(_passport);
            request.Body = new Body<HashedUserPassport>(hashedPassport);
            return request;
        }

        private bool IsAuthSuccess(Response response) =>
            response.Report.Status.Equals(AuthorizationStatus.Success);

        private bool IsAuthFailed(Response response) =>
            response.Report.Status.Equals(AuthorizationStatus.Failed);

        private AuthorizationContext CreateSuccessContext(Response<Guid> response)
        {
            AuthorizationContext context = new AuthorizationContext()
            {
                IsSuccess = true,
                Token = response.Content
            };
            return context;
        }
    }
}
