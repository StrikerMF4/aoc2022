using AdventOfCode.Exercises.Days.Domain13;
using System.Xml.Linq;

namespace AdventOfCode.Exercises
{
    public class Ex13_2 : Exercise
    {
        public Ex13_2() : base("13") { }

        public override int Run()
        {
            int sum = 0;
            string line = "";

            Node divider1 = ParseNode("[[2]]");
            Node divider2 = ParseNode("[[6]]");
            List<Node> nodes = new List<Node>() { divider1, divider2 };

            for (int index = 1; !inputReader.EndOfStream; index++)
            {
                line = inputReader.ReadLine();
                if(line != "")
                {
                    nodes.Add(ParseNode(line));
                }
            }

            nodes.Sort((a, b) => a.Compare(b));

            foreach(var node in nodes)
                Console.WriteLine(NodeStringify(node));

            return 
                (nodes.FindIndex(item => item.Equals(divider1)) + 1) * 
                (nodes.FindIndex(item => item.Equals(divider2)) + 1);
        }

        
        private Node ParseNode(string input)
        {
            IEnumerator<char> enumerator = input.GetEnumerator();

            Fork first = new Fork(), actual = first;
            Leaf buffer = null;

            enumerator.MoveNext();
            while (enumerator.MoveNext())
            {
                char ch = enumerator.Current;

                if (ch == '[')
                {
                    actual = new Fork(actual);
                }
                else if (ch == ']')
                {
                    actual = actual.parent;
                }
                else if (ch == ',')
                {
                    buffer = null;
                }
                else if (buffer == null)
                {
                    buffer = new Leaf(actual, int.Parse(ch.ToString()));
                }
                else
                {
                    buffer.value = buffer.value * 10 + int.Parse(ch.ToString());
                }
            }
            return first;
        }

        private string NodeStringify(Node node)
        {
            string result = "";
            if(node is Fork)
            {
                List<string> res = new List<string>();
                foreach (var child in (node as Fork).nodes)
                {
                    res.Add(NodeStringify(child));
                }
                return "[" + string.Join(',', res) + "]";
            }
            else
            {
                return (node as Leaf).value.ToString();
            }
        }
    }
}
