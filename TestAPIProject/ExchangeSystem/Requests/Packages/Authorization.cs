using ExchangeSystem.Requests.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Packages
{
    public class Authorization : Package
    {
        public Authorization(UserInfo reqObj) : base(reqObj)
        {
            RequestType = (int)RequestTypes.Auth;
        }
    }
}
