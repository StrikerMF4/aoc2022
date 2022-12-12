using AdventOfCode.Exercises.Days.Domain12;

namespace AdventOfCode.Exercises
{
    public class Ex12_2 : Exercise
    {
        char[,] map;
        int[,] distance;

        public Ex12_2() : base("12") { }

        public override int Run()
        {
            string[] lines = inputReader.ReadToEnd().Split('\n');

            map = new char[lines.Length, lines[0].Length];
            distance = new int[lines.Length, lines[0].Length];

            Position goal = new Position();
            Queue<Position> starting_points = new Queue<Position>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    map[i, j] = lines[i][j];
                    distance[i, j] = int.MaxValue;

                    if (map[i, j] == 'S')
                    {
                        map[i, j] = 'a';
                    }

                    if (map[i, j] == 'E')
                    {
                        goal = new Position(i, j);
                        map[i, j] = 'z';
                    }

                    if (map[i, j] == 'a')
                    {
                        starting_points.Enqueue(new Position(i, j));
                    }
                }
            }


            int min = int.MaxValue;
            while(starting_points.Count > 0)
            {
                Position actual = starting_points.Dequeue();

                int distance = Distance(actual, goal);
                if(distance < min)
                {
                    min = distance;
                }
            }

            return min;
        }

        private int Distance(Position from, Position to)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    distance[i, j] = int.MaxValue;
                }
            }

            Queue<Position> queue = new Queue<Position>();
            queue.Enqueue(from);
            distance[from.x, from.y] = 0;

            Position actual;
            while (queue.Count > 0)
            {
                actual = queue.Dequeue();

                foreach (Position position in GetNeighbours(map, actual))
                {
                    if (distance[position.x, position.y] == int.MaxValue)
                    {
                        if (position.Equals(to))
                            return distance[actual.x, actual.y] + 1;

                        distance[position.x, position.y] = distance[actual.x, actual.y] + 1;

                        queue.Enqueue(position);
                    }
                }
            }

            return distance[to.x, to.y];
        }

        private List<Position> GetNeighbours(char[,] map, Position position)
        {
            List<Position> neighbours = new List<Position>();

            if (IsValidMovement(position, position.Left(), map))
                neighbours.Add(position.Left());

            if (IsValidMovement(position, position.Right(), map))
                neighbours.Add(position.Right());

            if (IsValidMovement(position, position.Up(), map))
                neighbours.Add(position.Up());

            if (IsValidMovement(position, position.Down(), map))
                neighbours.Add(position.Down());

            return neighbours;
        }

        private bool IsValidMovement(Position from, Position to, char[,] map)
        {
            if (to.x < 0 || to.x >= map.GetLength(0))
                return false;

            if (to.y < 0 || to.y >= map.GetLength(1))
                return false;

            if (map[from.x, from.y] < map[to.x, to.y] - 1)
                return false;

            return true;
        }
    }
}
