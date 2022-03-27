using Exchange.System.Enums;

namespace Exchange.Server.Controllers
{
    public interface IControllerSelector
    {
        Controller SelectController(RequestType requestType);
    }
}
