namespace AdventOfCode.Exercises
{
    public class Ex4_2 : Exercise
    {
        public Ex4_2() : base("4") { }

        public override int Run()
        {
            int count = 0;

            while (!inputReader.EndOfStream)
            {
                string line = inputReader.ReadLine();
                string[] pair = line.Split(",");

                int[] zones1 = pair[0].Split("-").Select(item => int.Parse(item)).ToArray();
                int[] zones2 = pair[1].Split("-").Select(item => int.Parse(item)).ToArray();

                if (zones1[0] <= zones2[1] && zones2[0] <= zones1[1])
                {
                    count++;
                }
            }

            return count;
        }
    }
}
