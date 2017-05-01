using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BattleSnake.Core
{
    public class Coordinate : List<int>
    {
        public int X => this[0];
        public int Y => this[1];
    }

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
        public Coordinate[] Coordinates { get; set; }

        public Coordinate Head => Coordinates[0];

        public Move Update(GameStatus state)
        {
            return Move.Down;                                            
        }        

        public static SnakeSettings Create(GameSettings game)
        {
            return new SnakeSettings(game);
        }
    }
}