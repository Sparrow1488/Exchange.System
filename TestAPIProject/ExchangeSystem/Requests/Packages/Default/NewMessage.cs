using ExchangeSystem.Requests.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Packages.Default
{
    public class NewMessage : Package
    {
        public NewMessage(IRequestObject requestObject) : base(requestObject)
        {
            RequestType = RequestTypes.NewMessage;
        }
    }
}
