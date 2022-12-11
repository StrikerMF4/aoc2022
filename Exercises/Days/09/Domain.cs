using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Exercises.Days.Domain09
{
    public class Knot
    {
        Position position;

        public Knot(int x = 0, int y = 0)
        {
            position = new Position(x, y);
        }

        public Position MoveTowards(string direction)
        {
            if (direction == "U")
                position.y++;
            if (direction == "D")
                position.y--;
            if (direction == "L")
                position.x--;
            if (direction == "R")
                position.x++;
            return position.Copy();
        }

        public Position MoveTowards(Knot objective)
        {
            if (!Touching(objective))
            {
                if (Math.Abs(position.x - objective.position.x) >= 1)
                {
                    position.x += Math.Sign(objective.position.x - position.x);
                }
                if (Math.Abs(position.y - objective.position.y) >= 1)
                {
                    position.y += Math.Sign(objective.position.y - position.y);
                }
            }
            return position.Copy();
        }

        private bool Touching(Knot objective)
        {
            if (Math.Abs(position.x - objective.position.x) > 1)
            {
                return false;
            }
            if (Math.Abs(position.y - objective.position.y) > 1)
            {
                return false;
            }
            return true;
        }
    }

    public class Position
    {
        public int x;
        public int y;

        public Position(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public Position Copy()
        {
            return new Position(x, y);
        }

        public override int GetHashCode()
        {
            return (x.ToString() + ":" + y.ToString()).GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null and Position)
            {
                Position position = (Position)obj;
                return x == position.x && y == position.y;
            }
            return false;
        }
    }
}
