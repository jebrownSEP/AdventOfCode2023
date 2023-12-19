using Shared;

namespace Days;


public class Day3
{
    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day3.txt");
        // Part1(input);
        Part2(input);
    }

    public static void Part1(string input)
    {
        var grid = new Day3Grid(input);

        var numbersAdjacentToSymbol = grid.GetNumbersAdjacentToSymbol();
        Console.WriteLine(numbersAdjacentToSymbol.Sum());
    }

    public static void Part2(string input)
    {
        var grid = new Day3Grid(input);
        var gridGearRatios = grid.GetGearRatios();
        // foreach (int ratio in gridGearRatios)
        // {
        //     Console.WriteLine(ratio);
        // }
        Console.WriteLine(gridGearRatios.Sum());
    }

    public class Day3CoordinateWithValue
    {
        public readonly Coordinate coordinate;
        public readonly bool isSymbol = false;
        public readonly bool isNumber = false;

        public readonly bool isStar = false;

        public readonly string value;
        public Day3CoordinateWithValue(int x, int y, string value)
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
                if (String.Equals(value, "*"))
                {
                    this.isStar = true;
                }
            }
        }

        public override string ToString()
        {
            return this.coordinate.ToString();
        }
    }


    public class Day3Grid
    {
        public readonly Day3CoordinateWithValue[][] coordinatesGrid;

        public Day3Grid(string input)
        {
            // Remember these coordinates are actually arranged as y, x since each row (y) holds the x individual character columns
            this.coordinatesGrid = input.Split("\n").Select((row, y) => row.ToCharArray().Select((ch, x) => new Day3CoordinateWithValue(x, y, ch.ToString())).ToArray()).ToArray();
        }

        public int[] GetGearRatios()
        {
            var gearRatios = new List<int>();
            var starCoordinates = this.GetStarCoordinates();
            // For each symbol, map out all the coordinates in a SET that have 2 numbers adjacent
            var uniqueCoordinatesAdjacentToSymbol = new HashSet<Day3CoordinateWithValue>();
            foreach (Day3CoordinateWithValue symbCoord in starCoordinates)
            {
                // var adjacentCoords = Utils.GetAdjacentCoordinates(symbCoord.coordinate, this.coordinatesGrid[0].Length - 1, this.coordinatesGrid.Length - 1);
                // var adjNumberCoords = adjacentCoords.Select(adjCoord => this.coordinatesGrid[adjCoord.y][adjCoord.x]).Where(coord => coord.isNumber).ToArray();
                // if (adjNumberCoords.Length > 1)
                // {
                var connectedNumberCoordsList = new List<Day3CoordinateWithValue>();
                var numberConnected = 0;
                // top 3
                Day3CoordinateWithValue? topLeft = this.GetTopLeft(symbCoord.coordinate);
                Day3CoordinateWithValue? top = this.GetTop(symbCoord.coordinate);
                Day3CoordinateWithValue? topRight = this.GetTopRight(symbCoord.coordinate);
                Day3CoordinateWithValue? left = this.GetLeft(symbCoord.coordinate);
                Day3CoordinateWithValue? right = this.GetRight(symbCoord.coordinate);
                Day3CoordinateWithValue? bottomLeft = this.GetBottomLeft(symbCoord.coordinate);
                Day3CoordinateWithValue? bottom = this.GetBottom(symbCoord.coordinate);
                Day3CoordinateWithValue? bottomRight = this.GetBottomRight(symbCoord.coordinate);

                if (topLeft?.isNumber ?? false)
                {
                    connectedNumberCoordsList.Add(topLeft);
                    if (top?.isNumber ?? false)
                    {
                        connectedNumberCoordsList.Add(top);
                        if (topRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(topRight);
                            numberConnected++;
                        }
                        else
                        {
                            numberConnected++;
                        }
                    }
                    else
                    {
                        if (topRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(topRight);
                            numberConnected += 2;
                        }
                        else
                        {
                            numberConnected++;
                        }
                    }
                }
                else
                {
                    if (top?.isNumber ?? false)
                    {
                        connectedNumberCoordsList.Add(top);
                        if (topRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(topRight);
                            numberConnected++;
                        }
                        else
                        {
                            numberConnected++;
                        }
                    }
                    else
                    {
                        if (topRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(topRight);
                            numberConnected++;
                        }
                        // else => +=0
                    }
                }

                // left
                if (left?.isNumber ?? false)
                {
                    connectedNumberCoordsList.Add(left);
                    numberConnected++;
                }

                // right
                if (right?.isNumber ?? false)
                {
                    connectedNumberCoordsList.Add(right);
                    numberConnected++;
                }

                // bottom 3
                if (bottomLeft?.isNumber ?? false)
                {
                    connectedNumberCoordsList.Add(bottomLeft);
                    if (bottom?.isNumber ?? false)
                    {
                        connectedNumberCoordsList.Add(bottom);
                        if (bottomRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(bottomRight);
                            numberConnected++;
                        }
                        else
                        {
                            numberConnected++;
                        }
                    }
                    else
                    {
                        if (bottomRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(bottomRight);
                            numberConnected += 2;
                        }
                        else
                        {
                            numberConnected++;
                        }
                    }
                }
                else
                {
                    if (bottom?.isNumber ?? false)
                    {
                        connectedNumberCoordsList.Add(bottom);
                        if (bottomRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(bottomRight);
                            numberConnected++;
                        }
                        else
                        {
                            numberConnected++;
                        }
                    }
                    else
                    {
                        if (bottomRight?.isNumber ?? false)
                        {
                            connectedNumberCoordsList.Add(bottomRight);
                            numberConnected++;
                        }
                        // else => +=0
                    }
                }

                if (numberConnected == 2)
                {
                    foreach (Day3CoordinateWithValue connCoord in connectedNumberCoordsList)
                    {
                        uniqueCoordinatesAdjacentToSymbol.Add(connCoord);
                    }
                    var gearNumbers = this.ParseNumbersFromCoordinates(connectedNumberCoordsList.ToArray());
                    // There should be exactly 2
                    if (gearNumbers.Length != 2)
                    {
                        throw new Exception("Somehow got !2 gears... " + gearNumbers.Length);
                    }
                    gearRatios.Add(gearNumbers[0] * gearNumbers[1]);
                }
            }
            return gearRatios.ToArray();
        }

        private Day3CoordinateWithValue? GetTopLeft(Coordinate coord)
        {
            if (coord.x > 0 && coord.y > 0)
            {
                return this.coordinatesGrid[coord.y - 1][coord.x - 1];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetTop(Coordinate coord)
        {
            if (coord.y > 0)
            {
                return this.coordinatesGrid[coord.y - 1][coord.x];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetTopRight(Coordinate coord)
        {
            if (coord.x < this.coordinatesGrid[0].Length - 1)
            {
                return this.coordinatesGrid[coord.y - 1][coord.x + 1];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetBottomLeft(Coordinate coord)
        {
            if (coord.x > 0 && coord.y < this.coordinatesGrid.Length - 1)
            {
                return this.coordinatesGrid[coord.y + 1][coord.x - 1];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetBottom(Coordinate coord)
        {
            if (coord.y < this.coordinatesGrid.Length - 1)
            {
                return this.coordinatesGrid[coord.y + 1][coord.x];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetBottomRight(Coordinate coord)
        {
            if (coord.y < this.coordinatesGrid.Length - 1 && coord.x < this.coordinatesGrid.Length - 1)
            {
                return this.coordinatesGrid[coord.y + 1][coord.x + 1];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetLeft(Coordinate coord)
        {
            if (coord.x > 0)
            {
                return this.coordinatesGrid[coord.y][coord.x - 1];
            }
            return null;
        }

        private Day3CoordinateWithValue? GetRight(Coordinate coord)
        {
            if (coord.x < this.coordinatesGrid[0].Length - 1)
            {
                return this.coordinatesGrid[coord.y][coord.x + 1];
            }
            return null;
        }

        public int[] GetNumbersAdjacentToSymbol()
        {
            var symbolCoordinates = this.GetSymbolCoordinates();
            // For each symbol, map out all the coordinates in a SET that have numbers adjacent
            var uniqueCoordinatesAdjacentToSymbol = new HashSet<Day3CoordinateWithValue>();
            foreach (Day3CoordinateWithValue symbCoord in symbolCoordinates)
            {
                var adjacentCoords = Utils.GetAdjacentCoordinates(symbCoord.coordinate, this.coordinatesGrid[0].Length - 1, this.coordinatesGrid.Length - 1);
                var adjNumberCoords = adjacentCoords.Select(adjCoord => this.coordinatesGrid[adjCoord.y][adjCoord.x]).Where(coord => coord.isNumber).ToArray();
                foreach (Day3CoordinateWithValue adjNumberCoord in adjNumberCoords)
                {
                    // Make sure the set is unique for coordinates...
                    uniqueCoordinatesAdjacentToSymbol.Add(adjNumberCoord);
                }
            }

            return this.ParseNumbersFromCoordinates(uniqueCoordinatesAdjacentToSymbol.ToArray());
        }

        private int[] ParseNumbersFromCoordinates(Day3CoordinateWithValue[] numberCoordinates)
        {
            // Then for each value in the set, make them numbers
            var visitedNumberCoordinates = new Dictionary<string, bool>();
            var parsedNumbers = new List<int>();

            foreach (Day3CoordinateWithValue numCoord in numberCoordinates)
            {
                if (!visitedNumberCoordinates.ContainsKey(numCoord.ToString()))
                {
                    var leftCoordinates = this.GetCoordinatesLeftOf(numCoord.coordinate);
                    var rightCoordinates = this.GetCoordinatesRightOf(numCoord.coordinate);
                    // Mark them visited. 
                    foreach (Day3CoordinateWithValue lCoord in leftCoordinates)
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
                    foreach (Day3CoordinateWithValue rCoord in rightCoordinates)
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

        private Day3CoordinateWithValue[] GetCoordinatesLeftOf(Coordinate coord)
        {
            var leftCoords = new List<Day3CoordinateWithValue>();
            var currentX = coord.x - 1;
            while (currentX >= 0 && this.coordinatesGrid[coord.y][currentX].isNumber)
            {
                // Need to parse these so that farthest left is 1st
                leftCoords.Insert(0, this.coordinatesGrid[coord.y][currentX]);
                currentX--;
            }
            return leftCoords.ToArray();
        }
        private Day3CoordinateWithValue[] GetCoordinatesRightOf(Coordinate coord)
        {
            var rightCoords = new List<Day3CoordinateWithValue>();
            var currentX = coord.x + 1;
            while (currentX <= this.coordinatesGrid[0].Length - 1 && this.coordinatesGrid[coord.y][currentX].isNumber)
            {
                rightCoords.Add(this.coordinatesGrid[coord.y][currentX]);
                currentX++;
            }
            return rightCoords.ToArray();
        }

        private Day3CoordinateWithValue[] GetSymbolCoordinates()
        {
            // Find all the symbols that are not numbers or .
            var symbolCoordinates = new List<List<Day3CoordinateWithValue>>();
            foreach (Day3CoordinateWithValue[] coordRow in coordinatesGrid)
            {
                symbolCoordinates.Add(new List<Day3CoordinateWithValue>(coordRow.Where(coord => coord.isSymbol).ToArray()));
            }
            return symbolCoordinates.SelectMany(coord => coord).ToArray();
        }

        private Day3CoordinateWithValue[] GetStarCoordinates()
        {
            // Find all the symbols that are not numbers or .
            var symbolCoordinates = new List<List<Day3CoordinateWithValue>>();
            foreach (Day3CoordinateWithValue[] coordRow in coordinatesGrid)
            {
                symbolCoordinates.Add(new List<Day3CoordinateWithValue>(coordRow.Where(coord => coord.isStar).ToArray()));
            }
            return symbolCoordinates.SelectMany(coord => coord).ToArray();
        }
    }
}


