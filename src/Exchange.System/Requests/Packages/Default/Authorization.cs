using Exchange.System.Requests.Objects;

namespace Exchange.System.Requests.Packages.Default
{
    public class Authorization : Package
    {
        public Authorization(UserPassport reqObj) : base(reqObj)
        {
            RequestType = Packages.RequestType.Authorization;
        }
    }
}
