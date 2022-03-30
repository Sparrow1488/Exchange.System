namespace Exchange.System.Enums
{
    public class AuthorizationStatus : ResponseStatus
    {
        protected AuthorizationStatus(string status, string message) : base(status, message) { }

        public static readonly AuthorizationStatus Success = new AuthorizationStatus("Success", "Authorization success");
        public static readonly AuthorizationStatus Failed = new AuthorizationStatus("Failed", "Authorization is failed");
    }
}
