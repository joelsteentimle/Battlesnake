using System.Collections.Generic;

namespace BattleSnake.Core
{
    /// <summary>
    ///     Represent the board with x/y coords instead, by creating a grid from the battlesnake arrays
    ///     (for convenience, we allow -1 for x and y so the coords offered from battlesnake can be used directly)
    /// </summary>
    public class Board
    {
        private readonly BoardCellType[,] grid;

        public static Dictionary<Move, Coordinate> directionOffset = new Dictionary<Move, Coordinate>
        {
            {Move.Up, new Coordinate {0, -1}},
            {Move.Right, new Coordinate {1, 0}},
            {Move.Down, new Coordinate {0, 1}},
            {Move.Left, new Coordinate {-1, 0}}
        };

        private readonly int height;
        private readonly int width;

        public Board(string myId, int width, int height, Coordinate[] food, Snake[] deadSnakes, Snake[] snakes)
        {
            this.width = width;
            this.height = height;

            grid = new BoardCellType[this.width, this.height];

            SetPointArray(food, BoardCellType.Food);

            if (deadSnakes != null)
                foreach (var deadSnake in deadSnakes)
                    SetPointArray(deadSnake.Coordinates, BoardCellType.Dead);

            foreach (var snake in snakes)
            {
                var coords = snake.Coordinates;

                var boardCellType = snake.Id == myId ? BoardCellType.Me : BoardCellType.Enemy;

                SetPointArray(coords, boardCellType);
            }
        }

        public BoardCellType Peek(Coordinate peekCoordinate, Move direction)
        {
            var cor = peekCoordinate.MoveInDirection(direction);
            return GetBoardType(cor);
        }

        private BoardCellType GetBoardType(Coordinate cor)
        {
            int x = cor.X;
            int y = cor.Y;
            if (x < 0 || x >= width || y < 0 || y >= height)
                return BoardCellType.Wall;

            return grid[x, y];
        }

        private void SetPointArray(Coordinate[] coordinates, BoardCellType boardCellType)
        {
            if (coordinates == null)
                return;

            foreach (var point in coordinates)
                SetBoardPositionType(point, boardCellType);
        }

        private void SetBoardPositionType(Coordinate coordinate, BoardCellType cellType)
        {
            if (GetBoardType(coordinate) != BoardCellType.Wall)
            {
                grid[coordinate.X, coordinate.Y] = cellType;
            }
        }
    }
}