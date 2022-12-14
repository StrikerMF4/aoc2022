using AdventOfCode.Exercises.Days.Domain14;

namespace AdventOfCode.Exercises
{
    public class Ex14_2 : Exercise
    {
        public Ex14_2() : base("14") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();
            HashSet<Point> set = new HashSet<Point>();

            int maxy = 0;

            while (line != null)
            {
                string[] points = line.Split(" -> ");

                for (int i = 0; i < points.Length - 1; i++)
                {
                    Point point1 = ParsePoint(points[i], '#');
                    Point point2 = ParsePoint(points[i + 1], '#');

                    set.Add(point1);
                    foreach (Point point in Range(point1, point2))
                    {
                        set.Add(point);
                    }

                    maxy = (point1.y > maxy) ? point1.y : maxy;
                }
                Point last = ParsePoint(points[points.Length - 1], '#');
                set.Add(last);
                maxy = (last.y > maxy) ? last.y : maxy;

                line = inputReader.ReadLine();
            }

            int floor = 2 + maxy;

            bool end = false;
            for (int count = 1; true; count++)
            {
                Point sand = new Point(500, 0, 'o');

                while (sand.y < floor)
                {
                    Point check = new Point(sand.x, sand.y + 1);

                    if (!Full(check, set, floor))
                    {
                        sand.x = check.x;
                        sand.y = check.y;
                    }
                    else
                    {
                        check.x = sand.x - 1;
                        if (!Full(check, set, floor))
                        {
                            sand.x = check.x;
                            sand.y = check.y;
                        }
                        else
                        {
                            check.x = sand.x + 1;
                            if (!Full(check, set, floor))
                            {
                                sand.x = check.x;
                                sand.y = check.y;
                            }
                            else
                            {
                                set.Add(sand);
                                if (sand.y == 0)
                                    return count;
                                break;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        private Point[] Range(Point from, Point to)
        {
            if (from.x != to.x)
            {
                int min = Math.Min(from.x, to.x);
                int max = Math.Max(from.x, to.x);

                Point[] res = new Point[Math.Abs(from.x - to.x)];

                for (int i = min + 1; i < max; i++)
                {
                    res[i - min] = new Point(i, from.y, from.value);
                }

                return res;
            }
            else
            {
                int min = Math.Min(from.y, to.y);
                int max = Math.Max(from.y, to.y);

                Point[] res = new Point[Math.Abs(from.y - to.y)];

                for (int i = min + 1; i < max; i++)
                {
                    res[i - min] = new Point(from.x, i, from.value);
                }

                return res;
            }
        }

        private bool Full(Point point, HashSet<Point> set, int floor)
        {
            return set.Contains(point) || point.y == floor;
        }

        private Point ParsePoint(string input, char value)
        {
            string[] coords = input.Split(',');
            return new Point(int.Parse(coords[0]), int.Parse(coords[1]), value);
        }
    }
}
