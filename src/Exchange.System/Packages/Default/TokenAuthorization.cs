using Exchange.System.Packages.Default;
using Exchange.System.Enums;
using Exchange.System.Entities;

namespace Exchange.System.Requests.Objects.Packages.Default
{
    public class TokenAuthorization : Package
    {
        public TokenAuthorization(UserPassport requestObject) : base(requestObject)
        {
            RequestType = RequestType.TokenAuthorization;
        }
    }
}
