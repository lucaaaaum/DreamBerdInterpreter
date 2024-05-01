using System.Text.RegularExpressions;

namespace DreamBerdInterpreter.Interpreter.Extensions;

public static class StringExtensions
{
    public static bool Matches(this string text, Regex regex) => regex.IsMatch(text);

    public static IEnumerable<string> SplitAccordingTo(this string text, Regex regex) => regex.Split(text);
}