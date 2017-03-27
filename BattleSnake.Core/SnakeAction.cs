using Newtonsoft.Json;

namespace BattleSnake.Core
{
    public class SnakeAction
    {
        [JsonProperty("move")]
        public Move Move { get; set; }

        [JsonProperty("taunt", NullValueHandling = NullValueHandling.Ignore)]
        public string Taunt { get; set; }
    }
}
