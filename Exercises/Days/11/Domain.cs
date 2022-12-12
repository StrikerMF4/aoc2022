using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Exercises.Days.Domain11
{
    public class Monkey
    {
        public int Id;
        public Queue<long> Items;
        readonly Func<long, long> OperationFunc;
        readonly Func<long, bool> TestFunc;
        readonly int MonkeyFalse;
        readonly int MonkeyTrue;

        public int InspectionsCount = 0;

        public Monkey(int id, long[] items, Func<long, long> operation, Func<long, bool> test, int monkeyTrue, int monkeyFalse)
        {
            Id = id;
            Items = new Queue<long>(items);
            OperationFunc = operation;
            TestFunc = test;
            MonkeyTrue = monkeyTrue;
            MonkeyFalse = monkeyFalse;
        }

        public long Inspect(long worryLevel)
        {
            InspectionsCount++;
            return OperationFunc(worryLevel);
        }

        public int Test(long worryLevel)
        {
            return TestFunc(worryLevel) ? MonkeyTrue : MonkeyFalse;
        }
    }
}
