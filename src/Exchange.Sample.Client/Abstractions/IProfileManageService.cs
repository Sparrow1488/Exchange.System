using Exchange.System.Entities;
using System.Threading.Tasks;

namespace Exchange.Sample.Client.Abstractions
{
    public interface IProfileManageService
    {
        Task<Profile> GetUserProfileAsync(int userId);
    }
}
