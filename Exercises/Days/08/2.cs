namespace AdventOfCode.Exercises
{
    public class Ex8_2 : Exercise
    {
        public Ex8_2() : base("8") { }

        public override int Run()
        {
            string[] text = inputReader.ReadToEnd().Split("\n");

            int[,] forest = new int[text[0].Length, text.Length];
            int[,] value = new int[text[0].Length, text.Length];

            //Load forest
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < text[i].Length; j++)
                {
                    forest[i, j] = Convert.ToInt32(text[i][j]);
                }
            }

            int max = 0;
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < text[i].Length; j++)
                {
                    int top = 0;
                    int down = 0;
                    int left = 0;
                    int right = 0;

                    for (int m = 0; m < i; m++)
                    {
                        if (forest[m, j] >= forest[i, j])
                            top = 1;
                        else
                            top += 1;
                    }

                    for (int m = text.Length - 1; m > i; m--)
                    {
                        if (forest[m, j] >= forest[i, j])
                            down = 1;
                        else
                            down += 1;
                    }

                    for (int m = 0; m < j; m++)
                    {
                        if (forest[i, m] >= forest[i, j])
                            left = 1;
                        else
                            left += 1;
                    }

                    for (int m = text.Length - 1; m > j; m--)
                    {
                        if (forest[i, m] >= forest[i, j])
                            right = 1;
                        else
                            right += 1;
                    }

                    if (top * down * left * right > max)
                        max = top * down * left * right;
                }
            }

            return max;
        }
    }
}
