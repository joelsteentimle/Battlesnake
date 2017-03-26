using Newtonsoft.Json;

namespace Battlesnake
{
    public class GameSettings
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("game_id")]
        public string GameId { get; set; }
    }
}
