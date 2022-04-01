using Newtonsoft.Json;

namespace Exchange.System.Entities
{
    public class UserPassport
    {
        [JsonConstructor]
        public UserPassport(string login, string password, string token) : this(login, password)
        {
            Token = token;
        }

        public UserPassport(string login, string password)
        {
            Login = login;
            Password = password;
        }

        [JsonProperty] public string Login { get; set; }
        [JsonProperty] public string Password { get; set; }
        [JsonProperty] public string Token { get; set; }
    }
}
