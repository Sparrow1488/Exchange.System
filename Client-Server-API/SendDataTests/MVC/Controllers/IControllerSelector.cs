using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeServer.MVC.Controllers
{
    public interface IControllerSelector
    {
        Controller SelectController(RequestTypes requestType);
    }
}
