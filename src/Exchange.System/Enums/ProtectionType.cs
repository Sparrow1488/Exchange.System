namespace Exchange.System.Enums
{
    public class ProtectionType
    {
        private ProtectionType(string typeName)
        {
            Name = typeName;
        }

        public readonly string Name;

        public static readonly ProtectionType Default = new ProtectionType("Default");
        public static readonly ProtectionType AesRsa = new ProtectionType("AesRsa");
    }
}
