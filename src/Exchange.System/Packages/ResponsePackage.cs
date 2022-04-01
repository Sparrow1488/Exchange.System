using Exchange.System.Enums;
using Newtonsoft.Json;
using System;

namespace Exchange.System.Packages
{
    public class ResponsePackage
    {
        public ResponsePackage(object response, ResponseStatus status)
        {
            ResponseData = response;
            Status = status;
        }

        [JsonConstructor]
        public ResponsePackage(object response, ResponseStatus status, string errorMessage)
        {
            ResponseData = response;
            ErrorMessage = errorMessage;
            Status = status;
        }

        [JsonProperty("errorMessage")]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (value != null)
                    _errorMessage = value;
                else
                    throw new ArgumentException("Вы не можете использовать null в качестве значения для присвоения");
            }
        }
        private string _errorMessage = string.Empty;
        [JsonProperty("response")]
        public object ResponseData { get; }
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
