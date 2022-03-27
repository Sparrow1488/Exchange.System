using Exchange.System.Requests.Objects;
using Newtonsoft.Json;

namespace Exchange.System.Requests.Packages.Default
{
    public abstract class Package : IPackage
    {
        public Package(RequestType requestType, IRequestObject attachObject, string userToken)
        {
            RequestObject = attachObject;
            RequestType = requestType;
            UserToken = userToken;
        }
        public Package(IRequestObject requestObject)
        {
            RequestObject = requestObject;
        }
        public Package() { }
        [JsonProperty]
        public RequestType RequestType { get; protected set; }
        [JsonProperty]
        public IRequestObject RequestObject { get; protected set; }
        [JsonProperty]
        public string UserToken { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
