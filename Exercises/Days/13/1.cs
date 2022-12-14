using AdventOfCode.Exercises.Days.Domain13;
using System.Collections.Generic;

namespace AdventOfCode.Exercises
{
    public class Ex13_1 : Exercise
    {
        public Ex13_1() : base("13") { }

        public override int Run()
        {
            int sum = 0;

            for (int index = 1; !inputReader.EndOfStream; index++)
            {
                Node first = ParseNode(inputReader.ReadLine());
                Node second = ParseNode(inputReader.ReadLine());

                sum += first.Compare(second) < 0 ? index : 0;

                inputReader.ReadLine();
            }

            return sum;
        }

        private Node ParseNode(string input)
        {
            IEnumerator<char> enumerator = input.GetEnumerator();

            Node first = new Fork(), actual = first;
            Node buffer = null;


            enumerator.MoveNext();
            while (enumerator.MoveNext())
            {
                char ch = enumerator.Current;

                if (ch == '[')
                {
                    actual = new Fork(actual as Fork);
                }
                else if (ch == ']')
                {
                    if (actual is Fork)
                    {
                        actual = (actual as Fork).parent;
                    }
                }
                else if (ch == ',')
                {
                    buffer = null;
                }
                else if (buffer == null)
                {
                    buffer = new Leaf(actual as Fork, int.Parse(ch.ToString()));
                }
                else
                {
                    (buffer as Leaf).value *= 10;
                    (buffer as Leaf).value += int.Parse(ch.ToString());
                }
            }
            return first;
        }
    }
}
