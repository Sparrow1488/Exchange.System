using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using ExchangeSystem.Packages;

namespace Exchange.Server.Controllers
{
    public class ProfileController : Controller
    {
        public virtual Response Get(int userId)
        {
            var report = new ResponseReport("Profile found success", ResponseStatus.Ok);
            var profile = new Profile("asd-openLogin", "Сначала пицца, потом колла", new string[] { "pizza", "coca"});
            return new Response<Profile>(report, profile);
        }
    }
}
