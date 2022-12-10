using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Exercises
{
    public abstract class Exercise
    {
        public StreamReader inputReader;

        public Exercise(string name)
        {
            inputReader = new StreamReader("../../../Inputs/input" + name + ".txt");
        }

        public abstract int Run();
    }
}
