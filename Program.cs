using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode.Exercises;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static int Main(string[] args)
    {
        var excercise = new Ex12_2();

        return excercise.Run();
    }
}