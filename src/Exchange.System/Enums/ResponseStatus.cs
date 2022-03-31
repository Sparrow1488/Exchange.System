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
        public static readonly ResponseStatus Invalid = new ResponseStatus("Invalid", "Invalid request data");
        public static readonly ResponseStatus UnhandleException = new ResponseStatus("UnhandleException", "Unhandled Exception on server");

        public override string ToString() => StatusName ?? string.Empty;
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj)
        {
            bool isEquals = false;
            if (obj is ResponseStatus compareType)
            {
                if(compareType.StatusName == StatusName && compareType.Message == Message)
                    isEquals = true;
            }
            return isEquals;
        }
    }
}
