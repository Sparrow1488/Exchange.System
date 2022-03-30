using Exchange.System.Entities;
using Exchange.System.Enums;

namespace Exchange.System.Packages.Primitives
{
    public class Authorization : Package
    {
        public Authorization(UserPassport reqObj) : base(reqObj)
        {
            RequestType = ControllerType.Authorization;
        }
    }
}
