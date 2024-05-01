using System.Text;
using System.Text.RegularExpressions;

namespace DreamBerdInterpreter.Interpreter.Extensions;

public static class StringExtensions
{
    public static bool Matches(this string text, Regex regex) => regex.IsMatch(text);

    public static IEnumerable<string> SplitAccordingTo(this string text, Regex regex) => regex.Split(text);

    public static string Times(this string text, int amount)
    {
        var result = string.Empty;
        for (var count = 0; count < amount; count++)
            result += text;
        return result;
    }
}