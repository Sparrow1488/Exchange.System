using ExchangeSystem.Requests.Objects;

namespace ExchangeSystem.Requests.Packages.Default
{
    public class NewMessage : Package
    {
        public NewMessage(Message requestObject) : base(requestObject)
        {
            RequestType = RequestTypes.NewMessage;
        }
    }
}
