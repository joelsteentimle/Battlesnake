using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Battlesnake
{
    public class SnakeAction
    {
        [JsonProperty("move")]
        public Move Move { get; set; }
        [JsonProperty("taunt", NullValueHandling = NullValueHandling.Ignore)]
        public string Taunt { get; set; }
    }
}
