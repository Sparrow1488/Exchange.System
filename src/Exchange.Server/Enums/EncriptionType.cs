namespace Exchange.Server.Enums
{
    public class EncriptionType
    {
        private EncriptionType(string type) =>
            Type = type;

        public readonly string Type;

        public static readonly EncriptionType Aes256 = new EncriptionType(nameof(Aes256));
        public static readonly EncriptionType Rsa256 = new EncriptionType(nameof(Rsa256));
        public static readonly EncriptionType None = new EncriptionType(nameof(None));
    }
}
