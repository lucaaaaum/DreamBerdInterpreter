using System.Text;

namespace DreamBerdInterpreter.ConsoleApplication.Extensions;

public static class StringExtensions
{
    public static string GetSanitizedExpression(this string expression)
    {
        var lines = expression.Split(Environment.NewLine);
        var linesSanitized = GetSanitizedLines(lines);
        return BuildStringFromLines(linesSanitized);
    }

    private static IEnumerable<string> GetSanitizedLines(string[] lines) =>
        lines.Where(line => NotCommentedOut(line) && NotEmpty(line)).Select(LineTrimmed);

    private static bool NotCommentedOut(string line) => !line.StartsWith("//");
    
    private static bool NotEmpty(string line) => !string.IsNullOrWhiteSpace(line);
    
    private static string LineTrimmed(string line) => line.Trim();

    private static string BuildStringFromLines(IEnumerable<string> lines)
    {
        var stringBuilder = new StringBuilder();
        foreach (var line in lines)
            stringBuilder.Append(line);
        return stringBuilder.ToString();
    }
}