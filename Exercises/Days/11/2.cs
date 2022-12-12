using AdventOfCode.Exercises.Days.Domain11;

namespace AdventOfCode.Exercises
{
    public class Ex11_2 : Exercise
    {
        public Ex11_2() : base("11") { }

        public override int Run()
        {
            string line = "";

            int mcm = 1;
            List<Monkey> monkeys = new();

            while (line != null)
            {
                int id = int.Parse(inputReader.ReadLine().Substring(7, 1));
                string[] items = inputReader.ReadLine()[18..].Split(", ");
                string operation = inputReader.ReadLine()[19..];
                int divisor = int.Parse(inputReader.ReadLine()[21..]);
                int monkeyTrue = int.Parse(inputReader.ReadLine()[29..]);
                int monkeyFalse = int.Parse(inputReader.ReadLine()[30..]);

                mcm *= divisor;

                monkeys.Add(new Monkey(
                    id: id,
                    items: items.Select(item => long.Parse(item)).ToArray(),
                    operation: GetOperation(operation),
                    test: GetTest(divisor),
                    monkeyTrue,
                    monkeyFalse
                ));

                line = inputReader.ReadLine();
            }

            long worryLevel;
            int itemsCount;
            int rounds = 10000;
            for (int round = 0; round < rounds; round++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    itemsCount = monkey.Items.Count;
                    for (int i = 0; i < itemsCount; i++)
                    {
                        worryLevel = monkey.Items.Dequeue();

                        worryLevel = monkey.Inspect(worryLevel);

                        int toMonkey = monkey.Test(worryLevel);

                        monkeys[toMonkey].Items.Enqueue(worryLevel % mcm);
                    }
                }
            }

            foreach (Monkey monkey in monkeys)
            {
                Console.WriteLine("Monkey " + monkey.Id + " inspected items " + monkey.InspectionsCount + " times.");
            }

            var topMonkeys = monkeys
                .Select(item => item.InspectionsCount)
                .OrderByDescending(item => item)
                .Take(2)
                .ToArray();

            Console.WriteLine(topMonkeys[0]);
            Console.WriteLine(topMonkeys[1]);

            return 0;
        }

        public Func<long, long> GetOperation(string command)
        {
            Func<long, long, long> oper;
            if (command.Contains(" * "))
                oper = (a, b) => a * b;
            else
                oper = (a, b) => a + b;

            string[] variables = command.Split(new char[] { '+', '*' }, StringSplitOptions.TrimEntries);
            if (variables[1] != "old")
                return a => oper(a, long.Parse(variables[1]));
            return a => oper(a, a);
        }

        public Func<long, bool> GetTest(int divisor)
        {
            return a => a % divisor == 0;
        }
    }
}
