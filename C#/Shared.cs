namespace Shared;

public class Utils
{

    public static int ParseNumber(string s)
    {
        var isNumeric = int.TryParse(s, out int n);
        if (isNumeric)
        {
            return n;
        }
        throw new Exception("Not able to parse number " + s);
    }
    // https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }



    // Adjacent including diagonals
    public static Coordinate[] GetAdjacentCoordinates(Coordinate coord, int maxX, int maxY)
    {
        var adjacentCoords = new List<Coordinate>();
        if (coord.y > 0)
        {
            // top left
            if (coord.x > 0)
            {
                adjacentCoords.Add(new Coordinate(coord.x - 1, coord.y - 1));
            }
            // top
            adjacentCoords.Add(new Coordinate(coord.x, coord.y - 1));

            // top right
            if (coord.x < maxX)
            {
                adjacentCoords.Add(new Coordinate(coord.x + 1, coord.y - 1));
            }
        }

        // left
        if (coord.x > 0)
        {
            adjacentCoords.Add(new Coordinate(coord.x - 1, coord.y));
        }
        // right
        if (coord.x < maxX)
        {
            adjacentCoords.Add(new Coordinate(coord.x + 1, coord.y));
        }

        if (coord.y < maxY)
        {
            // bottom left
            if (coord.x > 0)
            {
                adjacentCoords.Add(new Coordinate(coord.x - 1, coord.y + 1));
            }

            // bottom
            adjacentCoords.Add(new Coordinate(coord.x, coord.y + 1));

            // bottom right
            if (coord.x < maxX)
            {
                adjacentCoords.Add(new Coordinate(coord.x + 1, coord.y + 1));
            }
        }
        return adjacentCoords.ToArray();
    }
}

public class Coordinate
{
    public readonly int x;
    public readonly int y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return this.x + "," + this.y;
    }
}

public class CoordinateWithValue
{
    public readonly Coordinate coordinate;

    public readonly string value;
    public CoordinateWithValue(int x, int y, string value)
    {
        this.value = value;
        this.coordinate = new Coordinate(x, y);

    }

    public override string ToString()
    {
        return this.coordinate.ToString();
    }
}


public enum Direction
{
    North,
    East,
    South,
    West
}



public class Grid
{
    public readonly CoordinateWithValue[][] coordinatesGrid;

    public Grid(string input)
    {
        // Remember these coordinates are actually arranged as y, x since each row (y) holds the x individual character columns
        this.coordinatesGrid = input.Split("\n").Select((row, y) => row.ToCharArray().Select((ch, x) => new CoordinateWithValue(x, y, ch.ToString())).ToArray()).ToArray();
    }

    // y,x
    public CoordinateWithValue? GetCoordinateInDirection(CoordinateWithValue coord, Direction direction)
    {
        if (direction == Direction.North && coord.coordinate.y > 0)
        {
            return this.coordinatesGrid[coord.coordinate.y - 1][coord.coordinate.x];
        }
        else if (direction == Direction.East && coord.coordinate.x < coordinatesGrid[0].Length - 1)
        {
            return this.coordinatesGrid[coord.coordinate.y][coord.coordinate.x + 1];
        }
        else if (direction == Direction.South && coord.coordinate.y < coordinatesGrid.Length - 1)
        {
            return this.coordinatesGrid[coord.coordinate.y + 1][coord.coordinate.x];
        }
        else if (direction == Direction.West && coord.coordinate.x > 0)
        {
            return this.coordinatesGrid[coord.coordinate.y][coord.coordinate.x - 1];
        }

        return null;
    }

}