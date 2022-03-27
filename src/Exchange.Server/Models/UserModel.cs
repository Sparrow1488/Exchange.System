using Exchange.Server.LocalDataBase;
using Exchange.Server.SQLDataBase;
using Exchange.System.Entities;
using Exchange.System.Requests.Objects;
using System.Linq;

namespace Exchange.Server.Models
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
                var findUser = db.Users.Where(user => user.UserPassport.Login == passport.Login &&
                                                                                                user.UserPassport.Password == passport.Password)
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
                    var findUser = db.Users.Where(user => user.UserPassport.Login == findPassport.Login && user.UserPassport.Password == findPassport.Password).FirstOrDefault();
                    findUser.UserPassport = findPassport;
                    return findUser;
                }
            }
            return null;
        }
        public bool Add(User newUser)
        {
            try
            {
                using (UsersDbContext db = new UsersDbContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }
    }
}
