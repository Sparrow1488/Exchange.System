using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;

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
