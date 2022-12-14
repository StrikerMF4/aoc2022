using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Exercises.Days.Domain14
{
    public class Point
    {
        public char value;
        
        public int x;
        public int y;

        public Point(int x, int y, char value = '.')
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public Point Copy()
        {
            return new Point(x, y, value);
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
}
