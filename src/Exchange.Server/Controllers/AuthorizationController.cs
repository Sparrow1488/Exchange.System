using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Extensions;
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
            Ex.ThrowIfNull(passport);
            Response response = default;
            if (passport is UserPassport userPassport)
            {
                Ex.ThrowIfEmptyOrNull(userPassport.Login, "Login wasn't be null or empty!");
                Ex.ThrowIfEmptyOrNull(userPassport.Password, "Password wasn't be null or empty!");
                if (CompleteUserAuthorization(userPassport))
                    response = CreateSuccessAuthResponse();
                else response = CreateFailedAuthResponse();
            }
            else
            {
                throw new ArgumentException($"Input data is not a {nameof(UserPassport)}");
            }
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
    }
}
