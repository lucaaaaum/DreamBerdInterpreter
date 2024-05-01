using System.Text;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;

    public void Interpret(string fileContent)
    {
        var lines = fileContent.Split(Environment.NewLine);
        var linesNotCommentedOut = lines.Where(line => !line.StartsWith("//"));
        fileContent = BuildStringFromLines(lines);
    }

    public static string BuildStringFromLines(IEnumerable<string> lines)
    {
        var stringBuilder = new StringBuilder();
        foreach (var line in lines)
            stringBuilder.AppendLine(line);
        return stringBuilder.ToString();
    }
}