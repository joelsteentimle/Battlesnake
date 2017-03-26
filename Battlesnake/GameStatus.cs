using Newtonsoft.Json;

namespace Battlesnake
{
    public class GameStatus
    {
        [JsonProperty("you")]
        public string You { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("turn")]
        public int Move { get; set; }
        [JsonProperty("snakes")]
        public Snake[] Snakes { get; set; }
        [JsonProperty("dead_snakes")]
        public Snake[] DeadSnakes { get; set; }
        [JsonProperty("food")]
        public int[][] Food { get; set; }
    }
}
