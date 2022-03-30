using Exchange.System.Entities;
using Exchange.System.Enums;
using Newtonsoft.Json;

namespace Exchange.System.Packages.Primitives
{
    public class Package : IPackage
    {
        public Package() { }

        [JsonConstructor]
        public Package(ControllerType requestType, IRequestObject attachObject, string userToken)
        {
            RequestObject = attachObject;
            RequestType = requestType;
            UserToken = userToken;
        }

        public Package(IRequestObject requestObject) =>
            RequestObject = requestObject;

        [JsonProperty]
        public ControllerType RequestType { get; protected set; }
        [JsonProperty]
        public IRequestObject RequestObject { get; protected set; }
        [JsonProperty]
        public string UserToken { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
