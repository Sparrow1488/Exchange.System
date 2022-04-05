using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Enums;

namespace Exchange.Server.Controllers
{
    public abstract class Message : Controller
    {
        public override abstract RequestType RequestType { get; }
        protected abstract override Protocol Protocol { get; set; }
        protected abstract override IProtocolSelector ProtocolSelector { get; set; }
    }
}
