using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Objects.Packages.Default
{
    public class NewPublication : Package
    {
        public NewPublication(Publication requestObject) : base(requestObject)
        {
            RequestType = RequestType.NewPublication;
        }
    }
}
