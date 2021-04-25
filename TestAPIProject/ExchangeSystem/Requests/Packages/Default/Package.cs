using ExchangeSystem.Requests.Objects;
using Newtonsoft.Json;

namespace ExchangeSystem.Requests.Packages.Default
{
    public abstract class Package : IPackage
    {
        public Package(RequestTypes requestType, IRequestObject attachObject, string userToken)
        {
            RequestObject = attachObject;
            RequestType = requestType;
            UserToken = userToken;
        }
        public Package(IRequestObject requestObject)
        {
            RequestObject = requestObject;
        }
        [JsonProperty]
        public RequestTypes RequestType { get; protected set; }
        [JsonProperty]
        public IRequestObject RequestObject { get; protected set; }
        [JsonProperty]
        public string UserToken { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
