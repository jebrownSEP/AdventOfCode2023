using Shared;

namespace Days;

public class Day16
{

    public static Dictionary<string, List<Direction>> energizedCoordinates = new();

    public static void Run()
    {
        var input = File.ReadAllText("../inputs/day16.txt");
        Part1(input);
        // Part2(input);
    }

    public static void Part1(string input)
    {
        var grid = new Grid(input);
        energizedCoordinates = new();
        PopulateEnergizedMap(grid);
        // y,x
        Console.WriteLine(energizedCoordinates.Keys.ToArray().Length);
    }

    public static void Part2(string input)
    {

    }

    public static void PopulateEnergizedMap(Grid grid)
    {
        // Start in top left going East (right) and mark all the spots as energized
        var currentCoordinate = grid.coordinatesGrid[0][0];
        var currentDirection = Direction.East;
        TrackPath(grid, currentCoordinate, currentDirection);
    }

    public static void TrackPath(Grid grid, CoordinateWithValue? currentCoordinate, Direction currentDirection)
    {
        if (currentCoordinate is null)
        {
            return;
        }
        if (energizedCoordinates.ContainsKey(currentCoordinate.ToString()))
        {
            if (energizedCoordinates[currentCoordinate.ToString()].Contains(currentDirection))
            {
                return;
            }
            var currentList = energizedCoordinates[currentCoordinate.ToString()];
            currentList.Add(currentDirection);
            energizedCoordinates[currentCoordinate.ToString()] = currentList;
        }
        else
        {
            var newList = new List<Direction>{
                currentDirection
            };
            energizedCoordinates.Add(currentCoordinate.ToString(), newList);
        }
        var newDirections = GetNewDirections(currentCoordinate.value, currentDirection);

        foreach (Direction dir in newDirections)
        {
            var nextCoordinate = grid.GetCoordinateInDirection(currentCoordinate, dir);
            TrackPath(grid, nextCoordinate, dir);
        }
        return;
    }

    private static List<Direction> GetNewDirections(string value, Direction currentDirection)
    {
        var newDirections = new List<Direction>();
        switch (value)
        {
            case ".":
                newDirections.Add(currentDirection);
                break;
            case "|":
                if (currentDirection == Direction.North || currentDirection == Direction.South)
                {
                    newDirections.Add(currentDirection);
                }
                else
                {
                    newDirections.Add(Direction.North);
                    newDirections.Add(Direction.South);
                }
                break;
            case "-":
                if (currentDirection == Direction.East || currentDirection == Direction.West)
                {
                    newDirections.Add(currentDirection);
                }
                else
                {
                    newDirections.Add(Direction.East);
                    newDirections.Add(Direction.West);
                }
                break;
            case "\\":
                switch (currentDirection)
                {
                    case Direction.North:
                        newDirections.Add(Direction.West);
                        break;
                    case Direction.East:
                        newDirections.Add(Direction.South);
                        break;
                    case Direction.South:
                        newDirections.Add(Direction.East);
                        break;
                    case Direction.West:
                        newDirections.Add(Direction.North);
                        break;
                    default:
                        break;
                }
                break;
            case "/":
                switch (currentDirection)
                {
                    case Direction.North:
                        newDirections.Add(Direction.East);
                        break;
                    case Direction.East:
                        newDirections.Add(Direction.North);
                        break;
                    case Direction.South:
                        newDirections.Add(Direction.West);
                        break;
                    case Direction.West:
                        newDirections.Add(Direction.South);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return newDirections;
    }



}