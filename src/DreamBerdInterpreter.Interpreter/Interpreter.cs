using System.Text;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;

    public void Interpret(string fileContent)
    {
        fileContent = GetFileContentSanitized(fileContent);
        
        var expressions = GetExpressions(fileContent);
        
        foreach (var expression in expressions)
        {
            if (expression.StartsWith("print"))
            {
                if (expression.EndsWith('?'))
                    _console.WriteLine($"Trying to evaluate the expression {expression}");
                var segments = expression.Split('"');
                _console.WriteLine(segments[1]);
            }
        }
    }

    private static IEnumerable<string> GetExpressions(string fileContent)
    {
        var expressions = new List<string>();
        var currentExpression = new List<char>();
        var previousChar = char.MinValue;
        var charCounter = 0;
        foreach (var currentChar in fileContent)
        {
            if (AnExpressionHasEndedInThePreviousChar(previousChar, currentChar))
            {
                expressions.Add(GetStringFromCharEnumerable(currentExpression));
                currentExpression.Clear();
            }

            if (ItsTheLastChar(fileContent, charCounter))
            {
                currentExpression.Add(currentChar);
                expressions.Add(GetStringFromCharEnumerable(currentExpression));
                break;
            }
            
            currentExpression.Add(currentChar);
            previousChar = currentChar;
            charCounter++;
        }

        return expressions;
    }

    private static bool AnExpressionHasEndedInThePreviousChar(char previousChar, char currentChar) => 
        (previousChar == '!' && currentChar != '!');

    private static bool ItsTheLastChar(string fileContent, int charCounter) => charCounter == fileContent.Length - 1;

    private string GetFileContentSanitized(string fileContent)
    {
        var lines = fileContent.Split(Environment.NewLine);
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

    private static string GetStringFromCharEnumerable(IEnumerable<char> charEnumerable)
    {
        var stringBuilder = new StringBuilder();
        foreach (var c in charEnumerable)
            stringBuilder.Append(c);
        return stringBuilder.ToString();
    }
}