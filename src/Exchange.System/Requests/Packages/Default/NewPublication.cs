using Exchange.System.Requests.Objects.Entities;
using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;

namespace Exchange.System.Requests.Objects.Packages.Default
{
    public class NewPublication : Package
    {
        public NewPublication(Publication requestObject) : base(requestObject)
        {
            RequestType = RequestType.NewPublication;
        }
    }
}
