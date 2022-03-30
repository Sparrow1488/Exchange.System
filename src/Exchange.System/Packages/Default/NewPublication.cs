using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages.Default;

namespace Exchange.System.Requests.Objects.Packages.Default
{
    public class NewPublication : Package
    {
        public NewPublication(Publication requestObject) : base(requestObject)
        {
            RequestType = ControllerType.NewPublication;
        }
    }
}
