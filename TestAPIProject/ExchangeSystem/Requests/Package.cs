using ExchangeSystem.Requests;
using ExchangeSystem.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeSystem.Requests
{
    public abstract class Package : IPackage
    {
        public Package(int requestType, IRequestObject attachObject)
        {
            RequestObject = attachObject;
            RequestType = requestType;
        }
        public Package(IRequestObject requestObject)
        {
            RequestObject = requestObject;
        }
        [JsonProperty]
        public int RequestType { get; protected set; }
        [JsonProperty]
        public IRequestObject RequestObject { get; protected set; }
        [JsonProperty]
        public SecurityData Security { get; } 
        public string UserToken { get; }
        public bool IsSecure { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
