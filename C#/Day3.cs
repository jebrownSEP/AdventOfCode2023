using System.Collections;
using Shared;

namespace Days;


public class Day3
{
    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day3.txt");
        Part1(input);
        // Part2(input);
    }

    public static void Part1(string input)
    {

        var grid = new Grid(input);

        var numbersAdjacentToSymbol = grid.GetNumbersAdjacentToSymbol();
        Console.WriteLine(numbersAdjacentToSymbol.Sum());
        // foreach (int num in numbersAdjacentToSymbol)
        // {
        //     Console.WriteLine(num);
        // }
    }

    public class Grid
    {
        public readonly CoordinateWithValue[][] coordinatesGrid;

        public Grid(string input)
        {
            // Remember these coordinates are actually arranged as y, x since each row (y) holds the x individual character columns
            this.coordinatesGrid = input.Split("\n").Select((row, y) => row.ToCharArray().Select((ch, x) => new CoordinateWithValue(x, y, ch.ToString())).ToArray()).ToArray();
        }

        public int[] GetNumbersAdjacentToSymbol()
        {
            var symbolCoordinates = this.GetSymbolCoordinates();
            // For each symbol, map out all the coordinates in a SET that have numbers adjacent
            var uniqueCoordinatesAdjacentToSymbol = new HashSet<CoordinateWithValue>();
            foreach (CoordinateWithValue symbCoord in symbolCoordinates)
            {
                var adjacentCoords = Utils.GetAdjacentCoordinates(symbCoord.coordinate, this.coordinatesGrid[0].Length - 1, this.coordinatesGrid.Length - 1);
                var adjNumberCoords = adjacentCoords.Select(adjCoord => this.coordinatesGrid[adjCoord.y][adjCoord.x]).Where(coord => coord.isNumber).ToArray();
                foreach (CoordinateWithValue adjNumberCoord in adjNumberCoords)
                {
                    // Make sure the set is unique for coordinates...
                    uniqueCoordinatesAdjacentToSymbol.Add(adjNumberCoord);
                }
            }

            return this.ParseNumbersFromCoordinates(uniqueCoordinatesAdjacentToSymbol.ToArray());
        }

        private int[] ParseNumbersFromCoordinates(CoordinateWithValue[] numberCoordinates)
        {
            // Then for each value in the set, make them numbers
            var visitedNumberCoordinates = new Dictionary<string, bool>();
            var parsedNumbers = new List<int>();

            foreach (CoordinateWithValue numCoord in numberCoordinates)
            {
                if (!visitedNumberCoordinates.ContainsKey(numCoord.ToString()))
                {
                    var leftCoordinates = this.GetCoordinatesLeftOf(numCoord.coordinate);
                    var rightCoordinates = this.GetCoordinatesRightOf(numCoord.coordinate);
                    // Mark them visited. 
                    // TODO: JEB may only need to add right coordinates
                    foreach (CoordinateWithValue lCoord in leftCoordinates)
                    {
                        if (!visitedNumberCoordinates.ContainsKey(lCoord.ToString()))
                        {
                            visitedNumberCoordinates.Add(lCoord.ToString(), true);
                        }
                        else
                        {
                            Console.WriteLine("Duplicate lCoord " + lCoord.ToString() + " " + lCoord.value);
                        }
                    }
                    foreach (CoordinateWithValue rCoord in rightCoordinates)
                    {
                        if (!visitedNumberCoordinates.ContainsKey(rCoord.ToString()))
                        {
                            visitedNumberCoordinates.Add(rCoord.ToString(), true);
                        }
                        else
                        {
                            Console.WriteLine("Duplicate rCoord " + rCoord.ToString() + " " + rCoord.value);
                        }
                    }
                    visitedNumberCoordinates.Add(numCoord.ToString(), true);
                    // Parse the number and add to parsedNumbers
                    var parsedNumber = Utils.ParseNumber(String.Join("", leftCoordinates.Select((coord) => coord.value)) + numCoord.value + String.Join("", rightCoordinates.Select((coord) => coord.value)));
                    parsedNumbers.Add(parsedNumber);
                }
            }
            return parsedNumbers.ToArray();
        }

        private CoordinateWithValue[] GetCoordinatesLeftOf(Coordinate coord)
        {
            var leftCoords = new List<CoordinateWithValue>();
            var currentX = coord.x - 1;
            while (currentX >= 0 && this.coordinatesGrid[coord.y][currentX].isNumber)
            {
                // Need to parse these so that farthest left is 1st
                leftCoords.Insert(0, this.coordinatesGrid[coord.y][currentX]);
                currentX--;
            }
            return leftCoords.ToArray();
        }
        private CoordinateWithValue[] GetCoordinatesRightOf(Coordinate coord)
        {
            var rightCoords = new List<CoordinateWithValue>();
            var currentX = coord.x + 1;
            while (currentX <= this.coordinatesGrid[0].Length - 1 && this.coordinatesGrid[coord.y][currentX].isNumber)
            {
                rightCoords.Add(this.coordinatesGrid[coord.y][currentX]);
                currentX++;
            }
            return rightCoords.ToArray();
        }

        private CoordinateWithValue[] GetSymbolCoordinates()
        {
            // Find all the symbols that are not numbers or .
            var symbolCoordinates = new List<List<CoordinateWithValue>>();
            foreach (CoordinateWithValue[] coordRow in coordinatesGrid)
            {
                symbolCoordinates.Add(new List<CoordinateWithValue>(coordRow.Where(coord => coord.isSymbol).ToArray()));
            }
            return symbolCoordinates.SelectMany(coord => coord).ToArray();
        }
    }

    public class CoordinateWithValue
    {
        public readonly Coordinate coordinate;
        public readonly bool isSymbol = false;
        public readonly bool isNumber = false;

        public readonly string value;
        public CoordinateWithValue(int x, int y, string value)
        {
            this.value = value;
            this.coordinate = new Coordinate(x, y);
            var isNumeric = int.TryParse(value, out int n);
            if (isNumeric)
            {
                this.isNumber = true;
            }
            else if (!String.Equals(value, "."))
            {
                this.isSymbol = true;
            }
        }

        public override string ToString()
        {
            return this.coordinate.ToString();
        }
    }


    // public static void Part2(string input)
    // {
    //     var games = input.Split("\n").Select((line, index) => new Game(line, index + 1)).ToArray();
    //     var totalPower = games.Select(game => game.power).Sum();
    //     Console.WriteLine(totalPower);
    // }
}