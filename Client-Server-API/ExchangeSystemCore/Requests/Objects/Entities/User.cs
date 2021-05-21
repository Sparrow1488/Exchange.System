using System.ComponentModel.DataAnnotations;

namespace ExchangeSystem.Requests.Objects.Entities
{
    public class User
    {
        public User(UserPassport passport)
        {
            Passport = passport;
        }
        public User(string name)
        {
            Name = name;
        }
        public User() { }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public int? Room { get; set; }
        public int PassportId { get; set; }
        public virtual UserPassport Passport { get; set; }
    }
}
