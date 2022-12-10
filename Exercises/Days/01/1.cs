namespace AdventOfCode.Exercises
{
    public class Ex1_1 : Exercise
    {
        public Ex1_1() : base("1") { }

        public override int Run()
        {
            string line;
            int aux = 0;
            int max = 0;

            while (!inputReader.EndOfStream)
            {
                line = inputReader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    aux += int.Parse(line);
                }
                else
                {
                    if (aux > max)
                        max = aux;
                    aux = 0;
                }
            }

            return max;
        }
    }
}
