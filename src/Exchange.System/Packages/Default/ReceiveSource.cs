using Exchange.System.Entities;
using Exchange.System.Enums;

namespace Exchange.System.Packages.Default
{
    public class ReceiveSource : Package
    {
        /// <summary>
        /// Наговнокодил, потому что нужно как то передать коллекцию int серверу
        /// </summary>
        public ReceiveSource(Publication requestObject) : base(requestObject)
        {
            RequestType = ControllerType.GetSource;
        }
    }
}
