using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode.Exercises.Days.Domain15;
using Range = AdventOfCode.Exercises.Days.Domain15.Range;

namespace AdventOfCode.Exercises
{
    public class Ex15_2 : Exercise
    {
        readonly Regex regex = new Regex("-{0,1}\\d+", RegexOptions.Compiled);

        const int MAX_COORD = 4000000;
        //const int MAX_COORD = 20;

        public Ex15_2() : base("15") { }

        public override int Run()
        {
            string line = inputReader.ReadLine();

            List<Range>[] occ = new List<Range>[MAX_COORD + 1];
            for (int i = 0; i <= MAX_COORD; i++)
            {
                occ[i] = new List<Range>();
            }

            while (line != null)
            {
                var matches = regex.Matches(line).Select(item => int.Parse(item.Value)).ToArray();

                Point sensor = new Point(matches[0], matches[1]);
                Point beacon = new Point(matches[2], matches[3]);

                Console.WriteLine("Procesando sensor x=" + sensor.x + " y=" + sensor.y);

                int distance = sensor.DistanceTo(beacon);

                int min = sensor.y - distance;
                int max = sensor.y + distance;
                for (int seek_height = Math.Max(min, 0); seek_height <= Math.Min(max, MAX_COORD); seek_height++)
                {
                    int span = Math.Min(max - seek_height, seek_height - min);

                    Range range = new Range(Math.Max(sensor.x - span, 0), Math.Min(sensor.x + span, MAX_COORD));

                    Add(occ[seek_height], range);
                }

                line = inputReader.ReadLine();
            }

            for (int i = 0; i <= MAX_COORD; i++)
            {
                if (occ[i].Count > 1 || occ[i][0].from != 0 || occ[i][0].to != MAX_COORD)
                {
                    int covered = MAX_COORD + 1;

                    //The function Add produces a List without redundant or overlapping ranges
                    //So I can add the ranges lenght and know if there is some uncovered block
                    foreach (Range range in occ[i])
                    {
                        covered -= range.to - range.from;
                    }

                    if (covered > 0)
                    {
                        Range some = Invert(occ[i])[0];

                        Console.WriteLine("ENCONTRADO x=" + some.from + " y=" + i);

                        return 1;
                    }
                }
            }

            return -1;
        }

        private int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private void Add(List<Range> list, Range range)
        {
            //Si está contenido en otro
            if (list.Any(item => item.from <= range.from && range.to <= item.to))
                return;

            //Si se puede juntar con otro (yo a la derecha)
            var item = list.FindAll(item => item.from <= range.from && item.to >= range.from - 1 && range.to >= item.to).OrderBy(item => item.from).FirstOrDefault();
            if (item != null)
            {
                range = new Range(item.from, range.to);
            }

            //Si se puede juntar con otro (yo a la izquierda)
            item = list.FindAll(item => range.from <= item.from && range.to >= item.from - 1 && item.to >= range.to).OrderBy(item => item.from).FirstOrDefault();
            if (item != null)
            {
                range = new Range(range.from, item.to);
            }

            list.RemoveAll(item => range.from <= item.from && item.to <= range.to);

            list.Add(range);
        }

        //Precondition: All ranges start is greater or equal than 0, and all ends are less or equal than MAX_COORDS
        private List<Range> Invert(List<Range> list)
        {
            List<Range> nonOverlapping = new List<Range>();
            foreach(Range range in list)
            {
                Add(nonOverlapping, range);
            }

            nonOverlapping.OrderBy(item => item.from);

            List<Range> result = new List<Range>();

            if (nonOverlapping[0].from > 0)
                result.Add(new Range(0, nonOverlapping[0].from - 1));

            for (int i = 0; i < nonOverlapping.Count - 1; i++)
            {
                if(nonOverlapping[i].to < nonOverlapping[i + 1].from - 1)
                    result.Add(new Range(nonOverlapping[i].to + 1, nonOverlapping[i + 1].from - 1));
            }

            if(nonOverlapping[nonOverlapping.Count - 1].to < MAX_COORD)
                result.Add(new Range(nonOverlapping[nonOverlapping.Count - 1].to + 1, MAX_COORD));

            return result;
        }
    }
}
