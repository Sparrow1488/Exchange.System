namespace ExchangeSystem.Requests.Objects.Entities
{
    public class User
    {
        public User(UserPassport passport)
        {
            Passport = passport;
        }
        public User() { }
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; }
        public string ParentName { get; }
        public int Room { get; }
        public UserPassport Passport { get; private set; }
    }
}
