using Newtonsoft.Json;
using System;

namespace ExchangeSystem.Requests.Packages.Default
{
    public class ResponsePackage : IPackage
    {
        public ResponsePackage(object response)
        {
            ResponseData = response;
        }
        /// <summary>
        /// ErrorMessage не может быть null!!!
        /// </summary>
        /// <param name="response"></param>
        /// <param name="errorMessage"></param>
        /// <exception cref="ArgumentException">Если errorMessage == null</exception>
        public ResponsePackage(object response, string errorMessage)
        {
            ResponseData = response;
            ErrorMessage = errorMessage;
        }
        [JsonProperty]
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

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
