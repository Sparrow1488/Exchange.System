using ExchangeSystem.SecurityData;
using System;
using System.Linq;
using System.Reflection;

namespace ExchangeServer.Protocols.Selectors
{
    public class ProtocolSelector : IProtocolSelector
    {
        public IProtocol SelectProtocol(EncryptType encryptType)
        {
            Type parent = typeof(IProtocol);
            Type[] findTypes = Assembly.GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(type => parent.IsAssignableFrom(type) &&
                                                                !type.IsInterface &&
                                                                !type.IsAbstract).ToArray();
            if (findTypes.Length == 0)
                throw new NullReferenceException("Reflection can't found no one protocols");

            foreach (var type in findTypes)
            {
                var instance = (Protocol)type.Assembly.CreateInstance(type.FullName);
                if (instance.EncryptType == encryptType)
                    return instance;
            }
            throw new NullReferenceException("Reflection can't found no one protocols");
        }
    }
}
