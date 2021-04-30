﻿using ExchangeSystem.Requests.Packages.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExchangeServer.MVC.Controllers
{
    public class ControllerSelector : IControllerSelector
    {
        public Controller SelectController(Package package)
        {
            Type parent = typeof(Controller);
            Type[] findTypes = Assembly.GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(type => parent.IsAssignableFrom(type) &&
                                                                !type.IsInterface &&
                                                                !type.IsAbstract).ToArray();
            if (findTypes.Length == 0)
                throw new NullReferenceException("Reflection can't found no one controller");

            foreach (var type in findTypes)
            {
                var instance = (Controller)type.Assembly.CreateInstance(type.FullName);
                if (instance.RequestType == package.RequestType)
                    return instance;
            }
            throw new NullReferenceException("Reflection can't found no one controller");
        }
    }
}