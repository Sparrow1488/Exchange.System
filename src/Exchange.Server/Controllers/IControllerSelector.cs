namespace Exchange.Server.Controllers
{
    public interface IControllerSelector
    {
        Controller SelectController(string controllerName);
    }
}
