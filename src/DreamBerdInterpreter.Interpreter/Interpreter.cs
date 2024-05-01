using System.Text.RegularExpressions;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;
    private IDictionary<string, string> _variables = new Dictionary<string, string>();
    private IDictionary<string, string> _functions = new Dictionary<string, string>();

    private Regex expressionSplitRegex = new(@"([!?]+)(?![^{]*})|}", RegexOptions.Compiled);
    private Regex onlyHasExclamationPointsAndQuestionMarksRegex = new("^[!?]+$");
    private Regex varConstRegex = new(@"(var|const)\s*(var|const)\s*([^\s]*)", RegexOptions.Compiled);

    public void Interpret(string expression)
    {
        var subExpressions = GetSubExpressions(expression);
    }

    private IEnumerable<string> GetSubExpressions(string expression)
    {
        var splittedExpressions = expressionSplitRegex.Split(expression);
        return splittedExpressions.Where(subExpression =>
            !string.IsNullOrWhiteSpace(subExpression) &&
            onlyHasExclamationPointsAndQuestionMarksRegex.IsMatch(subExpression)
        );
    }
}