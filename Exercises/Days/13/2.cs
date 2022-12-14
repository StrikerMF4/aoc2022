using AdventOfCode.Exercises.Days.Domain13;

namespace AdventOfCode.Exercises
{
    public class Ex13_2 : Exercise
    {
        public Ex13_2() : base("13") { }

        public override int Run()
        {
            int sum = 0;

            for (int index = 1; !inputReader.EndOfStream; index++)
            {
                Node node = ParseNode(inputReader.ReadLine());

                List<Node> nodes = new List<Node>();

                inputReader.ReadLine();
            }

            return sum;
        }

        private Status InOrder(Node node1, Node node2)
        {
            if (node1 is Leaf && node2 is Leaf)
            {
                if ((node1 as Leaf).value > (node2 as Leaf).value)
                {
                    return Status.NotInOrder;
                }
                else if ((node1 as Leaf).value < (node2 as Leaf).value)
                {
                    return Status.InOrder;
                }
                return Status.ContinueSearching;
            }
            else if (node1 is Leaf && node2 is not Leaf)
            {
                Fork cover = new Fork();
                cover.nodes.Add(node1);

                return InOrder(cover, node2);
            }
            else if (node1 is not Leaf && node2 is Leaf)
            {
                Fork cover = new Fork();
                cover.nodes.Add(node2);

                return InOrder(node1, cover);
            }
            else
            {
                return InOrder((node1 as Fork).nodes.GetEnumerator(), (node2 as Fork).nodes.GetEnumerator());
            }
        }

        private Status InOrder(IEnumerator<Node> enum1, IEnumerator<Node> enum2)
        {
            enum1.MoveNext();
            enum2.MoveNext();

            while (enum1.Current != null && enum2.Current != null)
            {
                var status = InOrder(enum1.Current, enum2.Current);

                if (status != Status.ContinueSearching)
                {
                    return status;
                }

                enum1.MoveNext();
                enum2.MoveNext();
            }

            if (enum1.Current == null)
            {
                if (enum2.Current != null)
                {
                    return Status.InOrder;
                }
                else
                {
                    return Status.ContinueSearching;
                }
            }

            return Status.NotInOrder;
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
    }
}
