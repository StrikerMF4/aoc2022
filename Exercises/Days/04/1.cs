namespace AdventOfCode.Exercises
{
    public class Ex4_1 : Exercise
    {
        public Ex4_1() : base("4") { }

        public override int Run()
        {
            string line = "";
            int count = 0;

            while (line != null)
            {
                line = inputReader.ReadLine();
                string[] pair = line.Split(",");

                int[] zones1 = pair[0].Split("-").Select(item => int.Parse(item)).ToArray();
                int[] zones2 = pair[1].Split("-").Select(item => int.Parse(item)).ToArray();

                bool ainb = zones1[0] >= zones2[0] && zones1[1] <= zones2[1];
                bool bina = zones2[0] >= zones1[0] && zones2[1] <= zones1[1];

                if (bina || ainb)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
