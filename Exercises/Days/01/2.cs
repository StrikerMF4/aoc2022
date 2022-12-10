namespace AdventOfCode.Exercises
{
    public class Ex1_2 : Exercise
    {
        public Ex1_2() : base("1") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();
            int aux = 0;

            List<int> calories = new List<int>();

            while (line != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    aux += int.Parse(line);
                }
                else
                {
                    calories.Add(aux);
                    aux = 0;
                }

                line = inputReader.ReadLine();
            }

            calories.Sort();
            var top = calories.Take(3);

            return top.Sum();
        }
    }
}
