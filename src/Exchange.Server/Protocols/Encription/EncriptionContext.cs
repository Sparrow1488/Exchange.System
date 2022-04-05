using Encryptors;
using Exchange.Server.Enums;

namespace Exchange.Server.Protocols.Encription
{
    public abstract class EncriptionContext
    {
        public abstract EncriptionType EncriptionType { get; }
        public abstract IEncryptor GetEncriptor();
    }
}
