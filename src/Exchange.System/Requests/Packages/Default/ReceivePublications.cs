using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;

namespace Exchange.System.Requests.Objects.Packages.Default
{
    public class ReceivePublications : Package
    {
        public ReceivePublications() : base()
        {
            RequestType = RequestType.GetPublication;
        }
    }
}
