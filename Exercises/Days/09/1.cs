using AdventOfCode.Exercises.Days.Domain09;

namespace AdventOfCode.Exercises
{
    public class Ex9_1 : Exercise
    {
        public Ex9_1() : base("9") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();

            Knot Head = new Knot(0, 0);
            Knot Tail = new Knot(0, 0);

            Position position;
            HashSet<Position> positions = new HashSet<Position>();

            while (line != null)
            {
                string[] action = line.Split(' ');

                for (int i = 0; i < int.Parse(action[1]); i++)
                {
                    Head.MoveTowards(action[0]);
                    position = Tail.MoveTowards(Head);

                    if (!positions.Contains(position))
                        positions.Add(position);
                }

                line = inputReader.ReadLine();
            }

            return positions.Count;
        }
    }
}
