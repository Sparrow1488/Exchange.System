using Exchange.System.Abstractions;
using Exchange.System.Enums;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class Request<T> : Request
    {
        public Request(string query) : base(query) { }
        [JsonConstructor]
        public Request(string query, ProtectionType protection) : base(query, protection) { }

        [JsonProperty] public Body<T> Body { get; set; }

        public override object GetBodyContent() => Body.Content;
    }

    public class Request
    {
        public Request(string query) =>
            Query = query;

        [JsonConstructor]
        public Request(string query, ProtectionType protection) : this(query) =>
            Protection = protection;

        [JsonProperty] public string Query { get; set; }
        [JsonProperty] public ProtectionType Protection { get; set; }

        public virtual object GetBodyContent() => null;
    }
}
