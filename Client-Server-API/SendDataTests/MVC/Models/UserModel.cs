using ExchangeServer.LocalDataBase;
using ExchangeServer.SQLDataBase;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System.Linq;

namespace ExchangeServer.MVC.Models
{
    public class UserModel
    {
        /// <summary>
        /// Получает паспорт по логину и паролю
        /// </summary>
        /// <param name="token"></param>
        /// <returns>UserPassport or Null</returns>
        public UserPassport ReceivePassportBy(string login, string password)
        {
            using (UsersDbContext db = new UsersDbContext())
            {
                var findPassport = db.Passports.Where(passport => passport.Login == login && passport.Password == password).FirstOrDefault();
                return findPassport;
            }
        }
        public UserPassport ReceivePassportBy(User user)
        {
            using (UsersDbContext db = new UsersDbContext())
            {
                var findPassport = db.Passports.Where(passport => passport.User == user).FirstOrDefault();
                return findPassport;
            }
        }
        public UserPassport ReceivePassportBy(string token)
        {
            var findPassport = ServerLocalDb.FindPassportBy(token);
            return findPassport;
        }
        /// <summary>
        /// Получает User по его паспорту
        /// </summary>
        /// <param name="token"></param>
        /// <returns>User or Null</returns>
        public User ReceiveUserBy(UserPassport passport)
        {
            using (UsersDbContext db = new UsersDbContext())
            {
                var findUser = db.Users.Where(user => user.Passport.Login == passport.Login &&
                                                                                                user.Passport.Password == passport.Password)
                                                              .FirstOrDefault();
                return findUser;
            }
        }
        /// <summary>
        /// Получает User по id пользователя
        /// </summary>
        /// <param name="token"></param>
        /// <returns>User or Null</returns>
        public User ReceiveUserBy(int id)
        {
            using (UsersDbContext db = new UsersDbContext())
            {
                var findUser = db.Users.Where(pass => pass.Id == id).FirstOrDefault();
                return findUser;
            }
        }
        public User ReceiveUserBy(string token)
        {
            var findPassport = ReceivePassportBy(token);
            if(findPassport != null)
            {
                using (UsersDbContext db = new UsersDbContext())
                {
                    var findUser = db.Users.Where(user => user.Passport.Login == findPassport.Login && user.Passport.Password == findPassport.Password).FirstOrDefault();
                    findUser.Passport = findPassport;
                    return findUser;
                }
            }
            return null;
        }
    }
}
