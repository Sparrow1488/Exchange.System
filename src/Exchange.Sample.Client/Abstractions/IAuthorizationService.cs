using System;
using System.Threading.Tasks;

namespace Exchange.Sample.Client.Abstractions
{
    public interface IAuthorizationService
    {
        Guid GetToken();
        bool IsSuccess();
        Task AuthorizeAsync();
        Task AuthorizeAsync(string login, string password);
    }
}
