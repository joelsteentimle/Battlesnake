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

        private static Dictionary<string, int> lastDirection = new Dictionary<string, int>();

        public Move Update(GameStatus state)
        {
            var directions = new[] { Move.Up, Move.Right, Move.Down, Move.Left };

            var oldDir = lastDirection.ContainsKey(Id) ? lastDirection[Id] : 0;

            lastDirection[Id] = ++oldDir % 4;

            return directions[lastDirection[Id]];
        }

        public static SnakeSettings Create(GameSettings game)
        {
            return new SnakeSettings(game);
        }
    }
}