using local9_2;

namespace AdventOfCode.Exercises
{
    public class Ex9_2 : Exercise
    {
        public Ex9_2() : base("9") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();

            Knot[] Rope = new Knot[10];
            //Rope.Initialize not working ¯\_(ツ)_/¯
            for (int i = 0; i < 10; i++)
                Rope[i] = new Knot();

            Position position = new Position(0, 0);
            HashSet<Position> positions = new HashSet<Position>();

            while (line != null)
            {
                string[] action = line.Split(' ');

                for (int i = 0; i < int.Parse(action[1]); i++)
                {
                    Rope[0].MoveTowards(action[0]);

                    Knot last = Rope[0];
                    foreach(Knot knot in Rope[1..])
                    {
                        position = knot.MoveTowards(last);
                        last = knot;
                    }

                    if (!positions.Contains(position))
                        positions.Add(position);
                }

                line = inputReader.ReadLine();
            }

            return positions.Count;
        }
    }
}

namespace local9_2 {
    internal class Knot
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

    internal class Position
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
            if(obj is not null and Position)
            {
                Position position = (Position)obj; 
                return x == position.x && y == position.y;
            }
            return false;
        }
    }
}
