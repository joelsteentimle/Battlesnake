using Newtonsoft.Json;
using System.Linq;

namespace Battlesnake
{
    public class Snake
    {
        [JsonProperty("taunt")]
        public string Taunt { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("health_points")]
        public int HealthPoints { get; set; }
        [JsonProperty("coords")]
        public int[][] Coordinates { get; set; }

        public Move Update(GameStatus state)
        {
            if (state.Food.Any())
            {
                var food = state.Food[0];
                if (Coordinates[0][0] < food[0])
                    return Move.Right;
                if (Coordinates[0][0] > food[0])
                    return Move.Left;
                if (Coordinates[0][1] < food[1])
                    return Move.Down;
                if (Coordinates[0][1] > food[1])
                    return Move.Up;
            }

            return Move.Down;
        }

        public static SnakeSettings Create(GameSettings game)
        {
            return new SnakeSettings();
        }
    }
}