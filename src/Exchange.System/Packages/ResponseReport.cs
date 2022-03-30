using Exchange.System.Enums;

namespace ExchangeSystem.Packages
{
    public class ResponseReport
    {
        public ResponseReport(string message, ResponseStatus status)
        {
            Message = message;
            Status = status;
        }

        public string Message { get; }
        public ResponseStatus Status { get; }
    }
}
