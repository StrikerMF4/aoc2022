namespace AdventOfCode.Exercises
{
    public class Ex6_1 : Exercise
    {
        public Ex6_1() : base("6") { }

        public override int Run()
        {
            char[] buffer = new char[16];

            int index = 1;

            while (!inputReader.EndOfStream)
            {
                buffer[(index - 1) % 16] = (char)inputReader.Read();

                if (buffer.Length == 16 && !buffer.Contains('\0') && buffer.Distinct().Count() == 16)
                    return index;

                index++;
            }

            return -1;
        }
    }
}
