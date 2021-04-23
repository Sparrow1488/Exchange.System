using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests.Objects.Entities
{
    public class User
    {
        public User(UserInfo info)
        {
            Info = info;
        }
        public int ProfilePhotoId { get; private set; }
        public string Description { get; private set; }
        public UserInfo Info { get; private set; }
    }
}
