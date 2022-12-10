namespace AdventOfCode.Exercises
{
    public class Ex10_1 : Exercise
    {
        public Ex10_1() : base("10") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();

            int cycle = 0;
            int idle_steps = 0;
            int buffer = 0;
            int X = 1;

            int sum = 0;
            int[] wanted = new int[6] { 20, 60, 100, 140, 180, 220 };

            while(line != null)
            {
                cycle++;

                if(--idle_steps <= 0)
                {
                    X += buffer;

                    string[] instruction = line.Split(" ");

                    if (instruction[0] == "addx")
                    {
                        idle_steps = 2;
                        buffer = int.Parse(instruction[1]);
                    }
                    if (instruction[0] == "noop")
                    {
                        idle_steps = 1;
                        buffer = 0;
                    }

                    line = inputReader.ReadLine();
                }

                if(wanted.Contains(cycle))
                    sum += X * cycle;
            }

            return sum;
        }
    }
}
