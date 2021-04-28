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
        public int ProfilePhotoId { get; private set; }
        public string Description { get; private set; }
        public UserPassport Info { get; private set; }
    }
}
