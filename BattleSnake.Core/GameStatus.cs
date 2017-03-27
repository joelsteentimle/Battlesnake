using Newtonsoft.Json;

namespace BattleSnake.Core
{
    public class GameStatus
    {
        private Board board;

        public Board Board
        {
            get { return board = board ?? new Board(You, Width, Height, Food, DeadSnakes, Snakes); }
        }

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
        public Coordinate[] Food { get; set; }
    }
}
