using ExchangeSystem.Enums;

namespace ExchangeSystem.Packages
{
    public class ResponseReport
    {
        public ResponseReport(string message, AuthorizationStatus status)
        {
            Message = message;
            StatusSuccess = status;
        }

        public string Message { get; }
        public AuthorizationStatus StatusSuccess { get; }
    }
}
