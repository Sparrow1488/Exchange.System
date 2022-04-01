using ExchangeSystem.Packages;
using Newtonsoft.Json;

namespace Exchange.System.Packages
{
    public class Response<T> : Response
    {
        public Response(ResponseReport report) : base(report) { }

        [JsonConstructor]
        public Response(ResponseReport report, T content) : base(report) =>
            Content = content;

        [JsonProperty] public T Content { get; private set; }
    }

    public class Response
    {
        [JsonConstructor]
        public Response(ResponseReport report) =>
            Report = report;

        public ResponseReport Report { get; private set; }
    }
}
