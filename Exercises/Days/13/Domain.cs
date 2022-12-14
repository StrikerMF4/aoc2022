using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode.Exercises.Days.Domain13
{
    public abstract class Node
    {
        public Fork parent = null;

        public Node() { }

        public Node(Fork parent)
        {
            this.parent = parent;

            if (parent != null)
                parent.nodes.Add(this);
        }

        public abstract int Compare(Node other);
    }

    public class Fork : Node
    {
        public List<Node> nodes;

        public Fork()
        {
            nodes = new List<Node>();
        }

        public Fork(Fork parent) : base(parent)
        {
            nodes = new List<Node>();
        }

        public override int Compare(Node other)
        {
            if(other is Leaf)
            {
                return -other.Compare(this);
            }

            IEnumerator<Node> left = nodes.GetEnumerator();
            IEnumerator<Node> right = (other as Fork).nodes.GetEnumerator();

            left.MoveNext();
            right.MoveNext();

            while (left.Current != null && right.Current != null)
            {
                int value = left.Current.Compare(right.Current);

                if (value != 0)
                {
                    return value;
                }

                left.MoveNext();
                right.MoveNext();
            }

            if (left.Current == null)
            {
                if (right.Current != null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            return 1;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Fork)
                return false;

            Fork fork = obj as Fork;

            if (nodes.Count != fork.nodes.Count)
                return false;

            return nodes
                .Zip(fork.nodes)
                .All(item => item.First.Equals(item.Second));
        }
    }

    public class Leaf : Node
    {
        public int value;

        public Leaf(int value)
        {
            this.value = value;
        }

        public Leaf(Fork parent, int value): base(parent)
        {
            this.value = value;
        }

        //Devuelve:
        //     A signed number indicating the relative values of this instance and value.
        //     Return Value – Description
        //     Less than zero – This instance is less than value.
        //     Zero – This instance is equal to value.
        //     Greater than zero – This instance is greater than value.
        public override int Compare(Node other)
        {
            if (other is Fork)
            {
                var fork = new Fork();
                fork.nodes.Add(this);

                return fork.Compare(other);
            }

            return this.value - (other as Leaf).value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Leaf)
                return false;

            Leaf fork = obj as Leaf;

            return value.Equals(fork.value);
        }
    }

    public enum Status
    {
        NotInOrder = 0,
        InOrder = 1,
        ContinueSearching = 2
    }
}
