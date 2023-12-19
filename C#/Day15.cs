using System.Text;

namespace Days;

public class Day15
{
    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day15.txt");
        Part1(input);
        // Part2(input);
    }

    public static void Part1(string input)
    {
        var total = 0;
        var steps = input.Split(",");
        foreach (string step in steps)
        {
            var currentValue = 0;
            foreach (char c in step)
            {
                var intValue = (int)c;
                currentValue += intValue;
                currentValue *= 17;
                currentValue %= 256;
            }
            total += currentValue;
        }
        Console.WriteLine(total);
    }

    public static void Part2(string input)
    {
        // Split input into lines
        var calibrationLines = input.Split("\n");
        // Do some stuff
        var sum = 34;
        // Write out the final answer
        Console.WriteLine(sum);
    }
}