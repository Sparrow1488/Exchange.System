using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Objects.Entities
{
    public class User
    {
        public User(UserPassport info)
        {
            Info = info;
        }
        public string Name { get; }
        public string LastName { get; }
        public string ParentName { get; }
        public string Login { get; }
        public int Id { get; }
        public string Password { get; }
        public int Room { get; }
        public UserToken Token { get; set; }
        public int AdminStatus { get; }
        public int ProfilePhotoId { get; private set; }
        public string Description { get; private set; }
        public UserPassport Info { get; private set; }
    }
}
