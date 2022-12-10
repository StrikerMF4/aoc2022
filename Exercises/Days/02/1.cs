namespace AdventOfCode.Exercises
{
    public class Ex2_1 : Exercise
    {
        public Ex2_1() : base("2") { }

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
                if (map[line[1]] == map[line[0]])
                { //Draw
                    score += 3 + map[line[1]];
                }
                else if (Wins(map[line[1]], map[line[0]]))
                { //Win
                    score += 6 + map[line[1]];
                }
                else
                { //Lose
                    score += map[line[1]];
                }
            }

            return score;
        }

        //Returns true if player1 beats player2
        private bool Wins(int player1, int player2)
        {
            return (player1 == 1 && player2 == 3) || (player1 == 2 && player2 == 1) || (player1 == 3 && player2 == 2);
        }
    }
}
