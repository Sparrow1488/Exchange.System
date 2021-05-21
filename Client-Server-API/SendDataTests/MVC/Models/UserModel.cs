using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System.Collections.Generic;

namespace ExchangeServer.MVC.Models
{
    public class UserModel
    {
        private List<UserPassport> _userPassports = new List<UserPassport>();
        private List<User> _users = new List<User>();
        public UserModel()
        {
            CreateUserCollection();
        }
        private void CreateUserCollection()
        {
        }
        public UserPassport ReceivePassportBy(string login, string password)
        {
            return _userPassports.Find(pass => pass.Login == login && pass.Password == password);
        }
        public UserPassport ReceivePassportBy(string token)
        {
            return _userPassports.Find(pass => pass.Token == token);
        }
        public User ReceiveUserBy(int id)
        {
            return _users.Find(user => user?.Passport.Id == id);
        }
    }
}
