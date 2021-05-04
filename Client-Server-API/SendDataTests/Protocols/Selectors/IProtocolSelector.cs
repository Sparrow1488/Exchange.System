using ExchangeSystem.SecurityData;

namespace ExchangeServer.Protocols.Selectors
{
    public interface IProtocolSelector
    {
        IProtocol SelectProtocol(EncryptType encryptType);
    }
}
