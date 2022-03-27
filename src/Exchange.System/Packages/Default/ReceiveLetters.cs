using Exchange.System.Packages.Default;
using Exchange.System.Enums;

namespace Exchange.System.Requests.Objects.Packages.Default
{
    public class ReceiveLetters : Package
    {
        public ReceiveLetters()
        {
            RequestType = RequestType.GetMessages;
        }
    }
}
