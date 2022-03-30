namespace ExchangeSystem.Enums
{
    public class AuthorizationStatus
    {
        private AuthorizationStatus(string status, string message)
        {
            Status = status;
            Message = message;
        }

        public static readonly AuthorizationStatus Success = new AuthorizationStatus("Success", "Authorization success");
        public static readonly AuthorizationStatus Failed = new AuthorizationStatus("Failed", "Authorization is failed");

        public string Status { get; }
        public string Message { get; }
    }
}
