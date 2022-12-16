using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Exercises.Days.Domain15
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int DistanceTo(Point to)
        {
            return Math.Abs(x - to.x) + Math.Abs(y - to.y);
        }

        public override int GetHashCode()
        {
            return (x.ToString() + ":" + y.ToString()).GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null and Point)
            {
                Point position = (Point)obj;
                return x == position.x && y == position.y;
            }
            return false;
        }
    }

    public class Range
    {
        public int from, to;

        public Range(int from, int to)
        {
            this.from = from;
            this.to = to;
        }
    }
}
