using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;

namespace ExchangeSystem.Requests.Packages.Default
{
    public class NewLetter : Package
    {
        public NewLetter(Letter requestObject) : base(requestObject)
        {
            RequestType = RequestTypes.NewMessage;
        }
    }
}
