using Exchange.System.Entities;
using Exchange.System.Enums;

namespace Exchange.System.Packages.Primitives
{
    public class AuthorizationPackage : Package
    {
        public AuthorizationPackage(ControllerType requestType, IRequestObject attachObject, string userToken) : base(requestType, attachObject, userToken)
        {
        }
    }
}
