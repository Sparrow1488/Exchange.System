using Exchange.System.Requests.Objects.Entities;

namespace Exchange.System.Requests.Packages.Default
{
    public class ReceiveSource : Package
    {
        /// <summary>
        /// Наговнокодил, потому что нужно как то передать коллекцию int серверу
        /// </summary>
        public ReceiveSource(Publication requestObject) : base(requestObject)
        {
            RequestType = RequestType.GetSource;
        }
    }
}
