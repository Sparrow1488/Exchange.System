using Exchange.System.Requests.Objects;
using Exchange.System.Requests.Objects.Entities;

namespace Exchange.System.Requests.Packages.Default
{
    public class NewLetter : Package
    {
        public NewLetter(Letter requestObject) : base(requestObject)
        {
            RequestType = RequestType.NewMessage;
        }
    }
}
