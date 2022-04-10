using System.Threading.Tasks;

namespace Exchange.Sample.Server.Abstractions
{
    public interface IStartableAsync
    {
        Task StartAsync();
    }
}
