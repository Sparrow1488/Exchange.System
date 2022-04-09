using Exchange.System.Helpers;
using Newtonsoft.Json;

namespace Exchange.System.Entities
{
    public sealed class HashedUserPassport : UserPassport
    {
        [JsonConstructor]
        private HashedUserPassport(
            string login, 
            string password, 
            string token) : base(login, password, token)
        {
        }

        public static HashedUserPassport CreateHashed(UserPassport passport)
        {
            var hasher = new Hasher();
            var hashedLogin = hasher.HashValue(passport?.Login ?? string.Empty);
            var hashedPassword = hasher.HashValue(passport?.Password ?? string.Empty);
            return new HashedUserPassport(
                hashedLogin, hashedPassword, passport?.Token ?? string.Empty);
        }
    }
}
