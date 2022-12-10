namespace AdventOfCode.Exercises
{
    public class Ex10_2 : Exercise
    {
        public Ex10_2() : base("10") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();

            int cycle = 1;
            int needed_cycles;
            int buffer;
            int X = 1;

            while (line != null)
            {
                string[] instruction = line.Split(" ");

                if (instruction[0] == "addx")
                {
                    buffer = int.Parse(instruction[1]);
                    needed_cycles = 2;
                }
                else
                {
                    needed_cycles = 1;
                    buffer = 0;
                }

                for (int i = 0; i < needed_cycles; i++)
                {
                    //Drawing
                    Console.Write((Math.Abs(X - ((cycle - 1) % 40)) <= 1) ? "#" : ".");
                    if ((cycle % 40) == 0)
                        Console.Write("\n");

                    cycle++;
                }

                X += buffer;

                line = inputReader.ReadLine();
            }

            return 1;
        }
    }
}
