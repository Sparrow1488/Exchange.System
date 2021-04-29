using ExchangeSystem.SecurityData;
using System;
using System.Linq;
using System.Reflection;

namespace ExchangeServer.Protocols.Selectors
{
    public class ProtocolSelector : IProtocolSelector
    {
        public IProtocol SelectProtocol(EncryptTypes encryptType)
        {
            Type parent = typeof(IProtocol);
            Type[] findTypes = Assembly.GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(t => parent.IsAssignableFrom(t) &&
                                                                !t.IsInterface &&
                                                                !t.IsAbstract).ToArray();
            if (findTypes.Length == 0)
                throw new NullReferenceException("Reflection can't found no one protocols");

            foreach (var type in findTypes)
            {
                var instance = (Protocol)type.Assembly.CreateInstance(type.FullName);
                if (instance.EncryptType == encryptType)
                    return instance;
            }
            throw new NullReferenceException("Рефлексией не было выявлено ни одного протокола");
        }
    }
}
