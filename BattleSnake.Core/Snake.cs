using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BattleSnake.Core
{
    public class Snake
    {
        private GameStatus gameState;

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
            gameState = state;

            return Directions.OrderBy(MoveScore).First();            
        }
        
        private int MoveScore(Move move)
        {
            if (IsOk(move))
            {
                var movePosition = Head.MoveInDirection(move);
                return gameState.Food.Select(movePosition.Distance).Min();
            }

            return int.MaxValue;
        }

        private bool IsOk(Move direction)
        {
            var peekType = gameState.Board.Peek(Head, direction);

            return peekType == BoardCellType.Empty || peekType == BoardCellType.Food || peekType == BoardCellType.Dead;
        }

        public static SnakeSettings Create(GameSettings game)
        {
            return new SnakeSettings(game);
        }

        private static IEnumerable<Move> Directions => new[] {Move.Up, Move.Right, Move.Down, Move.Left,};
    }
}