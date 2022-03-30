using Exchange.System.Entities;
using Exchange.System.Enums;
using Newtonsoft.Json;

namespace Exchange.System.Packages.Primitives
{
    public class Authorization : Package
    {
        [JsonConstructor]
        public Authorization(UserPassport reqObj) : base(reqObj)
        {
            RequestType = ControllerType.Authorization;
        }
    }
}
