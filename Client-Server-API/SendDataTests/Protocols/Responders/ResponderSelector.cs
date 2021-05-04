using ExchangeSystem.SecurityData;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;

namespace ExchangeServer.Protocols.Responders
{
    public class ResponderSelector : IResponderSelector
    {
        public Responder SelectResponder(EncryptType encryptType)
        {
            Type responderParent = typeof(Responder);
            Type[] findTypes = Assembly.GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(type => responderParent.IsAssignableFrom(type) &&
                                                                !type.IsInterface &&
                                                                !type.IsAbstract).ToArray();
            if (findTypes.Length == 0)
                throw new NullReferenceException("Reflection can't found no one responder");

            foreach (var responder in findTypes)
            {
                var instance = (Responder)Activator.CreateInstance(Type.GetType(responder.FullName));
                if (instance.EncryptType == encryptType)
                    return instance;
            }
            throw new NullReferenceException("Reflection can't found no one responder");
        }
    }
}
