namespace Exchange.System.Enums
{
    public class ResponseStatus
    {
        protected ResponseStatus(string status, string message)
        {
            StatusName = status;
            Message = message;
        }

        public string Message { get; protected set; }
        public string StatusName { get; protected set; }

        public static readonly ResponseStatus Ok = new ResponseStatus("Ok", "Good response");
        public static readonly ResponseStatus Bad = new ResponseStatus("Bad", "Bad response");
        public static readonly ResponseStatus UnhandleException = new ResponseStatus("UnhandleException", "UnhandleException on server");
    }
}
