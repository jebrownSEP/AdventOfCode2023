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