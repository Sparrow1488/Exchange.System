using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Server .Controllers
{
    public interface IControllerSelector
    {
        Controller SelectController(RequestType requestType);
    }
}
