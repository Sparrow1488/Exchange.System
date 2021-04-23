using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Objects
{
    public class UserInfo : IRequestObject
    {
        public UserInfo(string login, string password)
        {
            Login = login;
            Password = password;
        }
        [JsonConstructor]
        public UserInfo(string login, string password, string token)
        {
            Login = login;
            Password = password;
            Token = token;
        }
        public string Login { get; }
        [JsonProperty]
        private string Password { get; }
        public string Token { get; }
        public int UserId { get; }
    }
}
