using Newtonsoft.Json;

namespace BattleSnake.Core
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
