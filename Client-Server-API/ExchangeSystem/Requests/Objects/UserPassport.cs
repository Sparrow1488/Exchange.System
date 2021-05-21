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
        public int Id { get; set; }
        public string Login { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string Token { get; set; }
        [JsonProperty]
        public AdminStatus AdminStatus { get; set; }
    }
}
