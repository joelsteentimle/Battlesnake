using System;
using Newtonsoft.Json;

namespace BattleSnake.Core
{
    public class SnakeSettings
    {
        public SnakeSettings(GameSettings game)
        {
            var random = Guid.NewGuid().ToString().Substring(0, 6);

            Color = "#" + random;
            Name = "c#-snake-" + random;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("secondary_color")]
        public string SecondaryColor { get; set; }

        [JsonProperty("head_url", NullValueHandling = NullValueHandling.Ignore)]
        public string HeadUrl { get; set; }

        [JsonProperty("taunt", NullValueHandling = NullValueHandling.Ignore)]
        public string Taunt { get; set; }

        [JsonProperty("head_type", NullValueHandling = NullValueHandling.Ignore)]
        public DisplayType? HeadType { get; set; }

        [JsonProperty("tail_type", NullValueHandling = NullValueHandling.Ignore)]
        public DisplayType? TailType { get; set; }
    }
}
