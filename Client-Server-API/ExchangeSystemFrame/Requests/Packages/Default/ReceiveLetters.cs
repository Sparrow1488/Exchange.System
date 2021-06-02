using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;

namespace ExchangeSystem.Requests.Objects.Packages.Default
{
    public class ReceiveLetters : Package
    {
        public ReceiveLetters()
        {
            RequestType = RequestType.GetMessages;
        }
    }
}
