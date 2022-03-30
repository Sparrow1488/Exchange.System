using Newtonsoft.Json;

namespace Exchange.System.Enums
{
    public class ResponseStatus
    {
        [JsonConstructor]
        protected ResponseStatus(string status, string message)
        {
            StatusName = status;
            Message = message;
        }

        [JsonProperty]
        public string Message { get; protected set; }
        [JsonProperty]
        public string StatusName { get; protected set; }

        public static readonly ResponseStatus Ok = new ResponseStatus("Ok", "Good response");
        public static readonly ResponseStatus Bad = new ResponseStatus("Bad", "Bad response");
        public static readonly ResponseStatus UnhandleException = new ResponseStatus("UnhandleException", "UnhandleException on server");
    }
}
