using System.Text.RegularExpressions;

namespace AdventOfCode.Exercises
{
    public class Ex15_1 : Exercise
    {
        public Ex15_1() : base("15") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();
            Regex regex = new Regex("-{0,1}\\d+", RegexOptions.Compiled);

            HashSet<int> locked = new HashSet<int>();
            HashSet<int> beacons = new HashSet<int>();
            int seek_height = 2000000;

            while(line != null)
            {
                var matches = regex.Matches(line).Select(item => int.Parse(item.Value)).ToArray();

                int distance = Distance(matches[0], matches[1], matches[2], matches[3]);

                int min = matches[1] - distance;
                int max = matches[1] + distance;
                if (max >= seek_height && seek_height >= min)
                {
                    int span = Math.Min(max - seek_height, seek_height - min);

                    for(int i = matches[0] - span; i <= matches[0] + span; i++)
                    {
                        if (!locked.Contains(i))
                        {
                            locked.Add(i);
                        }
                    }
                }

                if (matches[3] == seek_height)
                    beacons.Add(matches[2]);

                line = inputReader.ReadLine();
            }

            int sum = 0;
            foreach(var position in locked)
            {
                if (!beacons.Contains(position))
                {
                    sum++;
                }
            }

            return sum;
        }

        private int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}
