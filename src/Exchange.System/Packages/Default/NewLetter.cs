using Exchange.System.Entities;
using Exchange.System.Enums;

namespace Exchange.System.Packages.Default
{
    public class NewLetter : Package
    {
        public NewLetter(Letter requestObject) : base(requestObject)
        {
            RequestType = ControllerType.NewMessage;
        }
    }
}
