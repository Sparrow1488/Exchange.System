using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeServer.MVC.Controllers
{
    public class ControllerSelector : IControllerSelector
    {
        public Controller SelectController(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
