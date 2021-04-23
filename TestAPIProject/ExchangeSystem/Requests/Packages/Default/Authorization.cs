using ExchangeSystem.Requests.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Packages.Default
{
    public class Authorization : Package
    {
        public Authorization(UserPassport reqObj) : base(reqObj)
        {
            RequestType = (int)RequestTypes.Auth;
        }
    }
}
