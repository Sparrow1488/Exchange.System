using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;

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
