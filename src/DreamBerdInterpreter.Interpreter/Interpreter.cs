using System.Text.RegularExpressions;
using DreamBerdInterpreter.Infrastructure.Console;
using DreamBerdInterpreter.Interpreter.Extensions;
using DreamBerdInterpreter.Interpreter.Types;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;
    private IDictionary<string, VarConst> _variables = new Dictionary<string, VarConst>();
    private IDictionary<string, string> _functions = new Dictionary<string, string>();

    private Regex expressionSplitRegex = new(@"([!?]+)(?![^{]*})|}", RegexOptions.Compiled);
    private Regex onlyHasExclamationPointsAndQuestionMarksRegex = new("^[!?]+$", RegexOptions.Compiled);
    private Regex varConstRegex = new(@"(var|const)\s*(var|const)\s*([^\s]*)\s*=*\s*([^!?]*)", RegexOptions.Compiled);

    public string Interpret(string expression, int visibilityLevel = 0)
    {
        var subExpressions = GetSubExpressions(expression);

        foreach (var subExpression in subExpressions)
            if (subExpression.Matches(varConstRegex))
            {
                var variable = GetVarConstFromSubExpression(visibilityLevel, subExpression);
                _variables.Add(variable.Name, variable);
            }

        return default;
    }

    private VarConst GetVarConstFromSubExpression(int visibilityLevel, string subExpression)
    {
        var match = varConstRegex.Match(subExpression);
        var reassignable = match.Groups[1].ToString() == "var";
        var editable = match.Groups[2].ToString() == "var";
        var name = match.Groups[3].ToString();
        var value = match.Groups[4].ToString();
        var variable = new VarConst(value, name, editable, reassignable, visibilityLevel);
        return variable;
    }

    private IEnumerable<string> GetSubExpressions(string expression)
    {
        var splittedExpressions = expression.SplitAccordingTo(expressionSplitRegex);
        return splittedExpressions.Where(subExpression =>
            !string.IsNullOrWhiteSpace(subExpression) &&
            !subExpression.Matches(onlyHasExclamationPointsAndQuestionMarksRegex)
        );
    }
}