using Newtonsoft.Json;

namespace Exchange.System.Entities
{
    public class TestEntity
    {
        [JsonConstructor]
        public TestEntity(string message) =>
            Message = message;

        [JsonProperty] public string Message { get; }
    }
}
