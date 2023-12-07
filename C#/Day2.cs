namespace Days;


public class Day2
{
    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day2.txt");
        // Part1(input);
        Part2(input);
    }

    public static void Part1(string input)
    {
        var games = input.Split("\n").Select((line, index) => new Game(line, index + 1)).ToArray();
        var total = games.Where(game => game.isValid).Select(game => game.id).Sum();
        Console.WriteLine(total);
    }

    public static void Part2(string input)
    {
        var games = input.Split("\n").Select((line, index) => new Game(line, index + 1)).ToArray();
        var totalPower = games.Select(game => game.power).Sum();
        Console.WriteLine(totalPower);
    }
}

public class Game
{
    private readonly Draw[] draws;
    public readonly int id;
    public readonly bool isValid;

    public readonly int maxRed = 0;
    public readonly int maxGreen = 0;
    public readonly int maxBlue = 0;
    public readonly int power = 0;


    public Game(string line, int id)
    {
        this.id = id;
        var gameValueString = line.Split(": ")[1];
        this.draws = gameValueString.Split("; ").Select(drawString => new Draw(drawString)).ToArray();
        this.isValid = this.draws.All(draw => draw.isValid);
        this.maxRed = this.draws.Select(draw => draw.red).Max();
        this.maxBlue = this.draws.Select(draw => draw.blue).Max();
        this.maxGreen = this.draws.Select(draw => draw.green).Max();
        this.power = this.maxBlue * this.maxRed * this.maxGreen;
    }
}

public class Draw
{

    readonly int MAX_RED = 12;
    readonly int MAX_GREEN = 13;
    readonly int MAX_BLUE = 14;
    public readonly int red = 0;
    public readonly int green = 0;
    public readonly int blue = 0;

    public readonly bool isValid = true;

    public Draw(string drawString)
    {
        var colorStrings = drawString.Split(", ");
        foreach (string colorString in colorStrings)
        {
            var splitColor = colorString.Split(" ");
            var isNumeric = int.TryParse(splitColor[0].ToString(), out int number);
            if (!isNumeric)
            {
                throw new Exception("Fail parse number");
            }

            var color = splitColor[1];
            if (String.Equals(color, "red"))
            {
                this.red = number;
                if (this.red > MAX_RED)
                {
                    this.isValid = false;
                }
            }
            if (String.Equals(color, "green"))
            {
                this.green = number;
                if (this.green > MAX_GREEN)
                {
                    this.isValid = false;
                }
            }
            if (String.Equals(color, "blue"))
            {
                this.blue = number;
                if (this.blue > MAX_BLUE)
                {
                    this.isValid = false;
                }
            }
        }
    }
}