using AdventOfCode.Exercises.Days.Domain12;
using System.Text;

namespace AdventOfCode.Exercises
{
    public class Ex12_1 : Exercise
    {
        public Ex12_1() : base("12") { }

        public override int Run()
        {
            string[] lines = inputReader.ReadToEnd().Split('\n');
            char[,] map = new char[lines.Length, lines[0].Length];
            int[,] distance = new int[lines.Length, lines[0].Length];

            Position actual = new Position();
            Position destination = new Position();

            for(int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    map[i, j] = lines[i][j];
                    distance[i, j] = int.MaxValue;

                    if (map[i, j] == 'S') {
                        actual = new Position(i, j);
                        map[i, j] = 'a';
                    }

                    if (map[i, j] == 'E')
                    {
                        destination = new Position(i, j);
                        map[i, j] = 'z';
                    }
                }
            }


            Queue<Position> queue = new Queue<Position>();
            queue.Enqueue(actual);
            distance[actual.x, actual.y] = 0;

            while (queue.Count > 0)
            {
                actual = queue.Dequeue();

                foreach(Position position in GetNeighbours(map, actual))
                {
                    if (distance[position.x, position.y] == int.MaxValue)
                    {
                        if (position.Equals(destination))
                            return distance[actual.x, actual.y] + 1;

                        distance[position.x, position.y] = distance[actual.x, actual.y] + 1;

                        queue.Enqueue(position);
                    }
                }
            }

            return distance[destination.x, destination.y];
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
