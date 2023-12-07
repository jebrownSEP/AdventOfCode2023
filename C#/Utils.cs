namespace Utils;

public class Utils
{
    // https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}