using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Packages
{
    public class NewMessage : Package
    {
        public NewMessage(IRequestObject requestObject) : base(requestObject)
        {
            RequestType = (int)RequestTypes.NewMessage;
        }
    }
}
