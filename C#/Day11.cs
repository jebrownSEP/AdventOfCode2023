namespace Days;

public class Day11
{
    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day11Test.txt");
        Part1(input);
        // Part2(input);
    }

    public static void Part1(string input)
    {
        var grid = new Shared.Grid(input);
        // Do some stuff
        var sum = 34;
        // Write out the final answer
        Console.WriteLine(sum);
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