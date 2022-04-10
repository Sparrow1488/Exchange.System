using Exchange.Server.Protocols;
using System;

namespace Exchange.Server.Routers
{
    public class RouterConfiguration
    {
        internal RouterConfiguration() { }

        public bool IsMultiProtocolSupport { get; private set; } = false;
        public NetworkProtocol OnlySupportProtocol { get; private set; }

        public RouterConfiguration MultiProtocolsSupport()
        {
            IsMultiProtocolSupport = true;
            OnlySupportProtocol = default;
            return this;
        }

        public RouterConfiguration OneProtocolSupport<TProtocol>()
            where TProtocol : NetworkProtocol
        {
            OnlySupportProtocol = Activator.CreateInstance<TProtocol>();
            IsMultiProtocolSupport = false;
            return this;
        }
    }
}
