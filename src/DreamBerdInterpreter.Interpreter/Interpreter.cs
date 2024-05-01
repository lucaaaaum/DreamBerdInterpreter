using System.Text.RegularExpressions;
using DreamBerdInterpreter.Infrastructure.Console;
using DreamBerdInterpreter.Interpreter.Extensions;
using DreamBerdInterpreter.Interpreter.Types;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;
    private IDictionary<string, VarConst> _variables = new Dictionary<string, VarConst>();
    private IDictionary<string, Function> _functions = new Dictionary<string, Function>();

    private readonly Regex _expressionSplitRegex = new(
        @"([!?]+)(?![^{]*})|}",
        RegexOptions.Compiled
    );

    private readonly Regex _onlyHasExclamationPointsAndQuestionMarksRegex = new(
        "^[!?]+$",
        RegexOptions.Compiled
    );

    private readonly Regex _varConstRegex = new(
        @"(var|const)\s*(var|const)\s*([^\s]*)\s*=*\s*([^!?]*)",
        RegexOptions.Compiled
    );

    private readonly Regex _printRegex = new(
        @"print\((.*)\)",
        RegexOptions.Compiled
    );

    private Regex _functionCallRegex = new(
        @"(.*)\((.*)\)",
        RegexOptions.Compiled
    );

    public string Interpret(string expression, int visibilityLevel = 0)
    {
        var subExpressions = GetSubExpressions(expression);

        foreach (var subExpression in subExpressions)
            if (subExpression.Matches(_varConstRegex))
            {
                var variable = GetVarConstFromSubExpression(visibilityLevel, subExpression);
                _variables.Add(variable.Name, variable);
            }
            else if (subExpression.Matches(_printRegex))
            {
                var match = _printRegex.Match(subExpression);
                var textToPrint = match.Groups[1].ToString();
                if (textToPrint.First() == '"' && textToPrint.Last() == '"')
                    textToPrint = textToPrint.Trim('"');
                _console.WriteLine(textToPrint);
            }
            else if (subExpression.Matches(_functionCallRegex))
            {
                var match = _functionCallRegex.Match(subExpression);
                var function = _functions[match.Groups[1].ToString()];
                Interpret(function.Expression, visibilityLevel + 1);
            }

        return default;
    }

    private VarConst GetVarConstFromSubExpression(int visibilityLevel, string subExpression)
    {
        var match = _varConstRegex.Match(subExpression);
        var reassignable = match.Groups[1].ToString() == "var";
        var editable = match.Groups[2].ToString() == "var";
        var name = match.Groups[3].ToString();
        var value = match.Groups[4].ToString();
        var variable = new VarConst(value, name, editable, reassignable, visibilityLevel);
        return variable;
    }

    private IEnumerable<string> GetSubExpressions(string expression)
    {
        var splittedExpressions = expression.SplitAccordingTo(_expressionSplitRegex);
        return splittedExpressions.Where(subExpression =>
            !string.IsNullOrWhiteSpace(subExpression) &&
            !subExpression.Matches(_onlyHasExclamationPointsAndQuestionMarksRegex)
        );
    }
}