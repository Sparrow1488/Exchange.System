using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Objects.Packages.Default
{
    public class TokenAuthorization : Package
    {
        public TokenAuthorization(UserPassport requestObject) : base(requestObject)
        {
            RequestType = RequestTypes.TokenAuthorization;
        }
    }
}
