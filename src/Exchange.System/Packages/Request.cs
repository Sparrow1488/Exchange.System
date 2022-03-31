using Exchange.System.Enums;

namespace Exchange.System.Packages
{
    public class Request<T> : Request
    {
        public Request(string query) : base(query) { }
        public Request(string query, ProtectionType protection) : base(query, protection) { }

        public RequestBody<T> Body { get; set; }
    }

    public class Request
    {
        public Request(string query) =>
            Query = query;

        public Request(string query, ProtectionType protection) : this(query) =>
            Protection = protection;

        public string Query { get; set; }
        public ProtectionType Protection { get; set; }
    }
}
