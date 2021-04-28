using Newtonsoft.Json;

namespace ExchangeSystem.Requests.Objects
{
    public class Message : IRequestObject
    {
        public Message(string description)
        {
            Description = description;
        }
        [JsonProperty]
        public string Description { get; } = string.Empty;
        public int SenderId { get; } = -1;
    }
}
