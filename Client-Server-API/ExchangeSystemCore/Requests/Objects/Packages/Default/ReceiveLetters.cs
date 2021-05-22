using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Objects.Packages.Default
{
    public class ReceiveLetters : Package
    {
        public ReceiveLetters(Letter requestObject) : base(requestObject)
        {
            RequestType = RequestTypes.GetPublication;
        }
    }
}
