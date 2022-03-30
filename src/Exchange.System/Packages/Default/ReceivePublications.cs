using Exchange.System.Packages.Default;
using Exchange.System.Enums;

namespace Exchange.System.Requests.Objects.Packages.Default
{
    public class ReceivePublications : Package
    {
        public ReceivePublications() : base()
        {
            RequestType = ControllerType.GetPublication;
        }
    }
}
