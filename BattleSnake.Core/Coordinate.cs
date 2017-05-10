using System;
using System.Collections.Generic;

namespace BattleSnake.Core
{
    public class Coordinate : List<int>
    {
        public static Coordinate Create(int x, int y)
        {
            return new Coordinate {x, y};
        }

        public int X => this[0];
        public int Y => this[1];

        public int Distance(Coordinate otherCoordinate)
        {
            var xDistance = Math.Abs(X - otherCoordinate.X);
            var yDistance = Math.Abs(Y - otherCoordinate.Y);

            return xDistance + yDistance;
        }

        public Coordinate MoveInDirection(Move direction)
        {
            var peekX = X + Board.directionOffset[direction].X;
            var peekY = Y + Board.directionOffset[direction].Y;

            return Create(peekX, peekY);
        }
    }
}