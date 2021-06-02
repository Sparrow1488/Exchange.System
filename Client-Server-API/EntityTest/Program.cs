using EntityTestLibraryFramework;

namespace EntityTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userContext = new UserContext();
            var obj = userContext.Users.Add(new User() { Name = "Григорий", Login = "Sparrow" });
            userContext.SaveChanges();
        }
    }
}
