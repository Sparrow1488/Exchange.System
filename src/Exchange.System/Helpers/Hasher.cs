using System;
using System.Security.Cryptography;
using System.Text;

namespace Exchange.System.Helpers
{
    public class Hasher
    {
        /// <summary>
        /// Created <see cref="Hasher"/> with concrete <see cref="Encoding"/><para/>
        /// Default: <see cref="Encoding.UTF8"/>
        /// </summary>
        /// <param name="encoding">Values will encoded in this encoding</param>
        public Hasher(Encoding encoding = null) =>
            _encoding = encoding ?? Encoding.UTF8;

        private readonly Encoding _encoding;

        public string HashValue(string value)
        {
            var valueInBytes = _encoding.GetBytes(value);
            var builder = new StringBuilder();
            using (var sha256 = SHA256.Create())
            {
                var computedBytes = sha256.ComputeHash(valueInBytes);
                for (int i = 0; i < computedBytes.Length; i++)
                    builder.Append(computedBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public bool VerifyHash(string hash, string originalValue)
        {
            var inputValueHash = HashValue(originalValue);
            var comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hash, inputValueHash) == 0;
        }
    }
}
