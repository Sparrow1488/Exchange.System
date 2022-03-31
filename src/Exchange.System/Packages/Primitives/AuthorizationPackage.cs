using Exchange.System.Entities;
using Exchange.System.Enums;
using Newtonsoft.Json;

namespace Exchange.System.Packages.Primitives
{
    public class AuthorizationPackage : Package
    {
        [JsonConstructor]
        public AuthorizationPackage(UserPassport requestObject) : base(requestObject)
        {
            RequestType = ControllerType.Authorization;
        }
    }
}
