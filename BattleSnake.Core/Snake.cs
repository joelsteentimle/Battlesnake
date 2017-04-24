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
            if (state.Food.Any())
            {
                var food = state.Food.OrderBy(Distance).First();                

                if (Head.X < food.X && IsOk(state.Board, Move.Right))
                    return Move.Right;

                if (Head.X > food.X && IsOk(state.Board, Move.Left))
                    return Move.Left;

                if (Head.Y < food.Y && IsOk(state.Board, Move.Down))
                    return Move.Down;

                if (Head.Y > food.Y && IsOk(state.Board, Move.Up))
                    return Move.Up;
            }

            if (IsOk(state.Board, Move.Right))
                return Move.Right;

            if (IsOk(state.Board, Move.Left))
                return Move.Left;

            if (IsOk(state.Board, Move.Down))
                return Move.Down;

            return Move.Up;
        }

        private int Distance(Coordinate coordinate)
        {
            var xDistance = Math.Abs(Head.X - coordinate.X);
            var yDistance = Math.Abs(Head.Y - coordinate.Y);

            return xDistance + yDistance;
        }

        private bool IsOk(Board board, Move direction)
        {
            var peekType = board.Peek(Head, direction);

            return peekType == BoardCellType.Empty || peekType == BoardCellType.Food || peekType == BoardCellType.Dead;
        }

        public static SnakeSettings Create(GameSettings game)
        {
            return new SnakeSettings(game);
        }
    }
}