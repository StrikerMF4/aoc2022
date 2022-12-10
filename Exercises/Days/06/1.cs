namespace AdventOfCode.Exercises
{
    public class Ex6_2 : Exercise
    {
        public Ex6_2() : base("6") { }

        public override int Run()
        {
            char[] buffer = new char[4];

            int index = 1;

            while (!inputReader.EndOfStream)
            {
                buffer[(index - 1) % 4] = (char)inputReader.Read();

                if (buffer.Length == 4 && !buffer.Contains('\0') && buffer.Distinct().Count() == 4)
                    return index;

                index++;
            }

            return -1;
        }
    }
}
