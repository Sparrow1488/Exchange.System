using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Objects.Packages.Default
{
    public class ReceivePublications : Package
    {
        public ReceivePublications() : base()
        {
            RequestType = RequestTypes.GetMessages;
        }
    }
}
