using Exchange.System.Entities;
using Exchange.System.Enums;

namespace Exchange.System.Packages.Default
{
    public class Authorization : Package
    {
        public Authorization(UserPassport reqObj) : base(reqObj)
        {
            RequestType = ControllerType.Authorization;
        }
    }
}
