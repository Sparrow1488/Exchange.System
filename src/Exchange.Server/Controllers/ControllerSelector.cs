using Exchange.Server.Exceptions;
using Exchange.System.Enums;
using ExchangeSystem.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Exchange.Server.Controllers
{
    public class ControllerSelector : IControllerSelector
    {
        public Controller SelectController(string controllerName)
        {
            Ex.ThrowIfEmptyOrNull(controllerName);
            Type parent = typeof(Controller);
            Type[] findTypes = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                         .Where(type => parent.IsAssignableFrom(type) &&
                                                                !type.IsInterface &&
                                                                !type.IsAbstract).ToArray();
            if (findTypes.Length == 0)
                throw new NullReferenceException("Reflection can't found no one controller");
            var foundControllerType = findTypes.FirstOrDefault(type => 
                            type.Name.Replace("Controller", "") == controllerName);
            Ex.ThrowIfTrue<ControllerNotFoundException>(() => foundControllerType == null,
                            "Reflection can't found no one controller");
            return (Controller)Activator.CreateInstance(foundControllerType);
        }
    }
}
