using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string Login { get; } = string.Empty;
        [JsonProperty]
        public string Password { get; } = string.Empty;
        public string Token { get; } = string.Empty;
        public int UserId { get; } = -1;
    }
}
