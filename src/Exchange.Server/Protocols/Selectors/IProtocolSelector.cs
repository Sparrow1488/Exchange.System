using ExchangeSystem.SecurityData;

namespace ExchangeServer.Protocols.Selectors
{
    public interface IProtocolSelector
    {
        Protocol SelectProtocol(EncryptType encryptType);
    }
}
