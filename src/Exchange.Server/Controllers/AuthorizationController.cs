using Exchange.Server.Extensions;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Packages.Primitives;
using ExchangeSystem.Helpers;
using ExchangeSystem.Packages;
using System;

namespace Exchange.Server.Controllers
{
    public class AuthorizationController : Controller
    {
        public ResponsePackage Authorization()
        {
            ResponsePackage response = default;
            var requestData = Context.Content.As<Package>().RequestObject;
            if (requestData is UserPassport userPassport)
            {
                Ex.ThrowIfEmptyOrNull(userPassport.Login, "Login wasn't be null or empty!");
                Ex.ThrowIfEmptyOrNull(userPassport.Password, "Password wasn't be null or empty!");
                if (CompleteUserAuthorization(userPassport))
                    response = CreateSuccessAuthResponsePackage();
                else response = CreateFailedAuthResponsePackage();
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

        private ResponsePackage CreateSuccessAuthResponsePackage() =>
            new ResponsePackage(new ResponseReport(AuthorizationStatus.Success.Message, AuthorizationStatus.Success), ResponseStatus.Ok);

        private ResponsePackage CreateFailedAuthResponsePackage() =>
            new ResponsePackage(new ResponseReport(AuthorizationStatus.Failed.Message, AuthorizationStatus.Failed), ResponseStatus.Bad);
    }
}
