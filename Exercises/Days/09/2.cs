using AdventOfCode.Exercises.Days.Domain09;

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