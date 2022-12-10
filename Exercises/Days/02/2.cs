namespace AdventOfCode.Exercises
{
    public class Ex2_2 : Exercise
    {
        public Ex2_2() : base("2") { }

        public override int Run()
        {
            Dictionary<string, int> map = new Dictionary<string, int>()
            { //1 for Rock, 2 for Paper, and 3 for Scissors
                { "A", 1 }, { "X", 1 },
                { "B", 2 }, { "Y", 2 },
                { "C", 3 }, { "Z", 3 },
            };

            int score = 0;

            while (!inputReader.EndOfStream)
            {
                string[] line = inputReader.ReadLine().Split(" ");

                //0 if you lost, 3 if the round was a draw, and 6 if you won
                if (map[line[1]] == 1) //You need to lose
                {
                    score += 0 + map[Beats(line[0])];
                }
                else if (map[line[1]] == 2) //You need to draw
                {
                    score += 3 + map[line[0]];
                }
                else //You need to win
                {
                    score += 6 + map[BeatedBy(line[0])];
                }
            }

            return score;
        }

        private string BeatedBy(string move)
        {
            switch (move)
            {
                case "A": //Rock
                    return "B";
                case "B": //Paper
                    return "C";
                case "C": //Scissor
                    return "A";
            }
            return "";
        }

        private string Beats(string move)
        {
            switch (move)
            {
                case "A": //Rock
                    return "C";
                case "B": //Paper
                    return "A";
                case "C": //Scissor
                    return "B";
            }
            return "";
        }
    }
}
