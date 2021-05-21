namespace ExchangeSystem.Requests.Objects.Entities
{
    public class User
    {
        public User(UserPassport passport)
        {
            Passport = passport;
        }
        public string Name { get; }
        public string LastName { get; }
        public string ParentName { get; }
        public int Room { get; }
        public UserPassport Passport { get; private set; }
    }
}
