using Newtonsoft.Json;
using System;

namespace Exchange.System.Requests.Packages.Default
{
    public class ResponsePackage : IPackage
    {
        public ResponsePackage(object response, ResponseStatus status)
        {
            ResponseData = response;
            Status = status;
        }
        /// <summary>
        /// ErrorMessage не может быть null!!!
        /// </summary>
        /// <param name="response"></param>
        /// <param name="errorMessage"></param>
        /// <exception cref="ArgumentException">Если errorMessage == null</exception>
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
