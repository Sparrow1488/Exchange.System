using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Extensions;
using Exchange.System.Helpers;
using Exchange.System.Packages;
using ExchangeSystem.Helpers;
using ExchangeSystem.Packages;
using System;

namespace Exchange.Server.Controllers
{
    public class AuthorizationController : Controller
    {
        public AuthorizationController() : base() { }
        public AuthorizationController(RequestContext context) : base(context) { }

        public virtual Response Authorization(UserPassport passport)
        {
            Response response;
            ThrowIfPassportInvalid(passport);

            if (CompleteUserAuthorization(passport))
                response = CreateSuccessAuthResponse();
            else response = CreateFailedAuthResponse();
            return response;
        }

        private bool CompleteUserAuthorization(UserPassport passport)
        {
            bool authSuccess = false;
            if (passport.Login == "asd" && passport.Password == "1234")
                authSuccess = true;
            return authSuccess;
        }

        private Response CreateSuccessAuthResponse() =>
            new Response<Guid>(new ResponseReport("Success authorization", AuthorizationStatus.Success), Guid.NewGuid());

        private Response CreateFailedAuthResponse() =>
            new Response<EmptyEntity>(new ResponseReport("Failed authorization", AuthorizationStatus.Failed), new EmptyEntity());

        private void ThrowIfPassportInvalid(UserPassport passport)
        {
            Ex.ThrowIfNull(passport);
            Ex.ThrowIfEmptyOrNull(passport.Login, "Login wasn't be null or empty!");
            Ex.ThrowIfEmptyOrNull(passport.Password, "Password wasn't be null or empty!");
        }

        public virtual Response AuthorizationHashed(HashedUserPassport hashedPassport)
        {
            ThrowIfPassportInvalid(hashedPassport);
            Response response;
            var hasher = new Hasher();
            if (hasher.VerifyHash(hashedPassport.Login, "asd")
                && hasher.VerifyHash(hashedPassport.Password, "1234"))
            {
                response = CreateSuccessAuthResponse();
            }
            else response = CreateFailedAuthResponse();
            return response;
        }
    }
}
