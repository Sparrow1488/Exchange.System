using Newtonsoft.Json;
using System.Collections.Generic;

namespace Exchange.System.Entities
{
    public class Profile
    {
        [JsonConstructor]
        public Profile(string openLogin, string description, IEnumerable<string> tags = null)
        {
            OpenLogin = openLogin;
            Description = description;
            Tags = tags;
        }

        [JsonProperty] public string OpenLogin { get; private set; }
        [JsonProperty] public string Description { get; private set; }
        [JsonProperty] public IEnumerable<string> Tags { get; private set; }
    }
}
