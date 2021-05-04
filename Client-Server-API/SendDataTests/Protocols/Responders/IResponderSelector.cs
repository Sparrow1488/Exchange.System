using ExchangeSystem.SecurityData;

namespace ExchangeServer.Protocols.Responders
{
    public interface IResponderSelector
    {
        Responder SelectResponder(EncryptType encryptType);
    }
}
