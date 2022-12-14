using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Exercises.Days.Domain12
{
    public class Position
    {
        public int x;
        public int y;

        public Position(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public Position Up()
        {
            return new Position(x, y + 1);
        }

        public Position Down()
        {
            return new Position(x, y - 1);
        }

        public Position Left()
        {
            return new Position(x - 1, y);
        }

        public Position Right()
        {
            return new Position(x + 1, y);
        }

        public override bool Equals(object? obj)
        {
            if(obj is not Position)
            {
                return false;
            }

            Position position = obj as Position;
            return position.x == x && position.y == y;
        }
    }
}
