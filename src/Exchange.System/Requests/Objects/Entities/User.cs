using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.System.Requests.Objects.Entities
{
    public class User
    {
        public User(User user)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            ParentName = user.ParentName;
            Room = user.Room;
        }
        public User(UserPassport passport)
        {
            UserPassport = passport;
        }
        public User(string name)
        {
            Name = name;
        }
        public User() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public int? Room { get; set; }
        public UserPassport UserPassport { get; set; }
    }
}
