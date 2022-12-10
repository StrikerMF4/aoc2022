namespace AdventOfCode.Exercises
{
    public class Ex8_1 : Exercise
    {
        public Ex8_1() : base("8") { }

        /*Each tree is represented as a single digit whose value is its 
         * height, where 0 is the shortest and 9 is the tallest.

        A tree is visible if all of the other trees between it and an edge of the grid are shorter than it. 
        Only consider trees in the same row or column; that is, only look up, down, left, or right from any given tree.*/

        public override int Run()
        {
            string[] text = inputReader.ReadToEnd().Split("\n");

            int[,] forest = new int[text[0].Length, text.Length];

            int sum = 0;

            //Load forest
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < text[i].Length; j++)
                {
                    forest[i, j] = Convert.ToInt32(text[i][j]);
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < text[i].Length; j++)
                {
                    bool left = true;
                    bool top = true;
                    bool down = true;
                    bool right = true;

                    for (int m = 0; m < text.Length; m++)
                    {
                        if (m < i && forest[m, j] >= forest[i, j])
                        {
                            top = false;
                        }

                        if (m > i && forest[m, j] >= forest[i, j])
                        {
                            down = false;
                        }

                        if (m < j && forest[i, m] >= forest[i, j])
                        {
                            left = false;
                        }

                        if (m > j && forest[i, m] >= forest[i, j])
                        {
                            right = false;
                        }
                    }

                    sum += (left || right || top || down) ? 1 : 0;
                }
            }

            return sum;
        }

        private int[,] SuperRange(int[,] array, Range rangex, Range rangey)
        {
            int[,] result = new int[rangex.End.Value - rangex.Start.Value, rangey.End.Value - rangey.Start.Value];
            for (int i = rangex.Start.Value; i < rangex.End.Value; i++)
            {
                for (int j = rangey.Start.Value; j < rangey.End.Value; j++)
                {
                    result[i, j] = array[i, j];
                }
            }
            return result;
        }

        private bool Any(int[,] array, Func <int, bool> predicate)
        {
            foreach(var e in array)
            {
                if (predicate(e))
                    return true;
            }
            return false;
        }
    }
}
