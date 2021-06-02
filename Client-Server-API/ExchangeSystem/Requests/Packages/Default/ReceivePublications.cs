using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Objects.Packages.Default
{
    public class ReceivePublications : Package
    {
        public ReceivePublications() : base()
        {
            RequestType = RequestTypes.GetPublication;
        }
    }
}
