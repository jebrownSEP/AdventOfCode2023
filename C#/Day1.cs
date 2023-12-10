namespace Days;

public class Day1
{
    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day1.txt");
        // Part1(input);
        Part2(input);
    }

    public static void Part1(string input)
    {
        var calibrationLines = input.Split("\n").Select(line => new CalibrationLine(line)).ToArray();
        var total = calibrationLines.Select(line => line.calibrationValue).ToArray().Sum();
        Console.WriteLine(total);
    }

    public static void Part2(string input)
    {
        var newInput = input
        .Replace("one", "one1one")
        .Replace("two", "two2two")
        .Replace("three", "three3three")
        .Replace("four", "four4four")
        .Replace("five", "five5five")
        .Replace("six", "six6six")
        .Replace("seven", "seven7seven")
        .Replace("eight", "eight8eight")
        .Replace("nine", "nine9nine");
        Part1(newInput);
    }
}

public class CalibrationLine
{


    private readonly int start;
    private readonly int end;
    public readonly int calibrationValue;


    public CalibrationLine(string line)
    {
        this.start = this.GetFirstNumber(line);
        this.end = this.GetLastNumber(line);
        this.calibrationValue = CombineNumbers(this.start, this.end);
    }

    private int GetFirstNumber(string line)
    {

        foreach (char c in line)
        {
            var isNumeric = int.TryParse(c.ToString(), out int n);
            if (isNumeric)
            {
                return n;
            }
        }
        return 0;
    }

    private int GetLastNumber(string line)
    {
        return this.GetFirstNumber(Shared.Utils.Reverse(line));
    }

    private static int CombineNumbers(int start, int end)
    {
        var isNumeric = int.TryParse(start.ToString() + end.ToString(), out int n);
        if (isNumeric)
        {
            return n;
        }
        return 0;
    }



}