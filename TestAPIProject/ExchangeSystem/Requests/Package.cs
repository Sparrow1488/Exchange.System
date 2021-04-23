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
            AttachObject = attachObject;
            RequestType = requestType;
        }
        public Package(IRequestObject requestObject)
        {
            AttachObject = requestObject;
        }
        [JsonProperty]
        public int RequestType { get; protected set; }
        [JsonProperty]
        public IRequestObject AttachObject { get; protected set; }
        public PackageSecurity SecurityInfo { get; } 
        public string UserToken { get; }
        public bool IsSecure { get; }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
