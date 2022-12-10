using System.Text.RegularExpressions;

namespace AdventOfCode.Exercises
{
    public class Ex7_1 : Exercise
    {
        public Ex7_1() : base("7") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();
            int sum = 0;
            Stack<string> path = new Stack<string>();

            Dictionary<string, int> pathSize = new Dictionary<string, int>();

            while (line != null)
            {
                if (line.StartsWith("$ cd"))
                {
                    string dir = line[5..];

                    if (dir == "..")
                        path.Pop();
                    else
                    {
                        if (dir == "/")
                            path.Clear();
                        path.Push(dir);
                    }
                    line = inputReader.ReadLine();
                }
                else if (line.StartsWith("$ ls"))
                { //Leo los resultados y voy introduciendo los pesos intermedios en el diccionario
                    line = inputReader.ReadLine();
                    while (line != null && !line.StartsWith("$"))
                    {
                        string[] entry = line.Split(" ");

                        if (entry[0] != "dir")
                        {
                            if (pathSize.ContainsKey(Join(path)))
                                pathSize[Join(path)] += int.Parse(entry[0]);
                            else
                                pathSize[Join(path)] = int.Parse(entry[0]);
                        }
                        line = inputReader.ReadLine();
                    }
                }
                else
                {
                    line = inputReader.ReadLine();
                }
            }

            //Biggest Lenght, highest priority
            PriorityQueue<string, int> LengthQueue = new(Comparer<int>.Create((a, b) => -a.CompareTo(b)));
            LengthQueue.EnqueueRange(pathSize.Keys.Select(key => (key, key.Length)));

            while (LengthQueue.Count > 0)
            {
                var key = LengthQueue.Dequeue();

                if (key != "/")
                {
                    if (pathSize.ContainsKey(ParentPath(key)))
                        pathSize[ParentPath(key)] += pathSize[key];
                    else
                    {
                        pathSize[ParentPath(key)] = pathSize[key];
                        LengthQueue.Enqueue(ParentPath(key), ParentPath(key).Length);
                    }
                }

                if (pathSize[key] <= 100000)
                    sum += pathSize[key];
            }

            return sum;
        }

        private static string Join(Stack<string> stack)
        {
            var path = stack.ToArray().Reverse();
            if (stack.Count == 1)
                return string.Join("/", path)[0..];
            return string.Join("/", path)[1..];
        }

        private static string ParentPath(string path)
        {
            Regex regex = new Regex("/\\w+$");
            string res = regex.Replace(path, "");
            return res == "" ? "/" : res;
        }
    }
}
