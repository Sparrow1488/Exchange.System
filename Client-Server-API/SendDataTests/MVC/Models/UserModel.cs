using ExchangeSystem.Requests.Objects;
using System.Collections.Generic;

namespace ExchangeServer.MVC.Models
{
    public class UserModel
    {
        private List<UserPassport> _userPassports = new List<UserPassport>();
        public UserModel()
        {
            CreateUserCollection();
        }
        private void CreateUserCollection()
        {
            _userPassports.Add(new UserPassport("Sparrow", "1488"));
            _userPassports.Add(new UserPassport("Nigger", "228"));
        }
        public UserPassport Receive(string login)
        {
            return _userPassports.Find(pass => pass.Login == login);
        }
    }
}
