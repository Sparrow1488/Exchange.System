using Newtonsoft.Json;

namespace ExchangeSystem.Requests.Objects
{
    public class UserPassport : IRequestObject
    {
        public UserPassport(string login, string password)
        {
            Login = login;
            Password = password;
        }
        [JsonConstructor]
        public UserPassport(string login, string password, string token)
        {
            Login = login;
            Password = password;
            Token = token;
        }
        [Key]
        public int Id { get; } = -1;
        public string Login { get; } = string.Empty;
        [JsonProperty]
        public string Password { get; } = string.Empty;
        [JsonProperty]
        public string Token { get; } = string.Empty;
        [JsonProperty]
        public AdminStatus AdminStatus { get; } = AdminStatus.User;
    }
}
