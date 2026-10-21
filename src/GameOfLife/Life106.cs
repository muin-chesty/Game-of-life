using System.Globalization;

public static class Life106
{
    public const string HEADER = "#Life 1.06";
    public const char ByteOrderMark = '\uFEFF';

    public static HashSet<(long X, long Y)> Parse(TextReader reader)
    {
        HashSet<(long X, long Y)> cells = new();
        string? line;
        int lineNumber = 0;

        while((line = reader.ReadLine()) != null)
        {
            lineNumber++;
            // Notepad and Windows powershell pipes can add a byte order mark at the start
            string trimmed = line.Trim().TrimStart(ByteOrderMark);
            if(trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            string[] parts = trimmed.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);

            if(parts.Length != 2)
            {
                throw new FormatException($"Line {lineNumber}: expected two integers 'x y' but got \"{line}\"");
            }

            cells.Add((ParseCoordinate(parts[0], lineNumber),
                ParseCoordinate(parts[1], lineNumber))
            );
        }

        return cells;
    }

    private static long ParseCoordinate(string text, int lineNumber)
    {
        if(long.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long value))
        {
            return value;
        }
        // Can't parse the given text. Investigate why?!

        bool isInteger = System.Numerics.BigInteger.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out _);
        string reason = isInteger ? "Number is outside of the range of signed 64-bit integer" : "is not an integer";
        throw new FormatException($"Line {lineNumber}: \"{text}\" {reason}");
    }

    // C# tuple's comparator would first compare by first value then second.
    // So this would write output first ordered by x followed by y
    public static void Write(TextWriter writer, HashSet<(long X, long Y)> cells)
    {
        writer.WriteLine(HEADER);
        foreach(var cell in cells.Order())
        {
            writer.Write(cell.X.ToString(CultureInfo.InvariantCulture));
            writer.Write(' ');
            writer.WriteLine(cell.Y.ToString(CultureInfo.InvariantCulture));
        }
    }
}