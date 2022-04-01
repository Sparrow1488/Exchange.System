using Exchange.System.Enums;
using Exchange.System.Protection;

namespace Exchange.Server.Protocols.Selectors
{
    public interface IProtocolSelector
    {
        Protocol SelectProtocol(EncryptType encryptType);
        NetworkProtocol SelectProtocol(ProtectionType protectionType);
    }
}
