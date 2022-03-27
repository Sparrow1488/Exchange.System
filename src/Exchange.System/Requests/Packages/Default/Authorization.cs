using ExchangeSystem.Requests.Objects;

namespace ExchangeSystem.Requests.Packages.Default
{
    public class Authorization : Package
    {
        public Authorization(UserPassport reqObj) : base(reqObj)
        {
            RequestType = Packages.RequestType.Authorization;
        }
    }
}
