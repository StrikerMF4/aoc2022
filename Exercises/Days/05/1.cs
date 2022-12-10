using System.Text.RegularExpressions;

namespace AdventOfCode.Exercises
{
    public class Ex5_2 : Exercise
    {
        public Ex5_2() : base("5") { }

        public override int Run()
        {
            Regex reg = new Regex("...(\\s){0,1}");
            List<MatchCollection> lines = new List<MatchCollection>();

            string line1 = inputReader.ReadLine();
            while (line1 != "")
            {
                lines.Add(reg.Matches(line1));
                line1 = inputReader.ReadLine();
            }
            lines.Reverse();
            lines.RemoveAt(0);

            var stacks = new Stack<char>[lines[0].Count];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<char>();
            }


            foreach (var matches in lines)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    var item = matches[i].ToString().Trim().Replace("[", "").Replace("]", "");
                    if (!string.IsNullOrEmpty(item))
                    {
                        stacks[i].Push(item[0]);
                    }
                }
            }

            while (!inputReader.EndOfStream)
            {
                string line = inputReader.ReadLine();
                int[] action = line.Split(new String[] { "move", "from", "to" }, (StringSplitOptions)3)
                    .Select(item => int.Parse(item)).ToArray();

                for (int i = 1; i <= action[0]; i++)
                {
                    if (stacks[action[1] - 1].Count > 0)
                    {
                        stacks[action[2] - 1].Push(stacks[action[1] - 1].Pop());
                    }
                }
            }

            Console.WriteLine(stacks.Select(item => item.Pop()).ToArray());
            return 0;
        }
    }
}
