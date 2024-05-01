using System.Text.RegularExpressions;
using DreamBerdInterpreter.Infrastructure.Console;
using DreamBerdInterpreter.Interpreter.Expressions;
using DreamBerdInterpreter.Interpreter.Extensions;
using DreamBerdInterpreter.Interpreter.Types;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;
    private IDictionary<string, VarConst> _variables = new Dictionary<string, VarConst>();
    private IDictionary<string, Function> _functions = new Dictionary<string, Function>();

    private readonly Regex _subExpressionRegex = new(
        """([^?!\n]*".*"|[^?!\n]*".*"[^?!\n]*|[^?!\n]*)([?!]+)""",
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
            {
                var variable = GetVarConstFromSubExpression(visibilityLevel, subExpression);
            if (subExpression.Content.Matches(_varConstRegex))
            {
                var variable = GetVarConstFromSubExpression(visibilityLevel, subExpression.Content);
                _variables.Add(variable.Name, variable);
            }
            else if (subExpression.Content.Matches(_printRegex))
            {
                var match = _printRegex.Match(subExpression.Content);
                var thingToPrint = match.Groups[1].ToString();
                if (thingToPrint.First() == '"' && thingToPrint.Last() == '"')
                {
                    thingToPrint = thingToPrint.Trim('"');
                    _console.WriteLine(thingToPrint);
                    continue;
                }

                VarConst variable;
                try
                {
                    variable = _variables[thingToPrint];
                }
                catch (Exception e)
                {
                    _console.WriteErrorMessage($"bro there is no variable called *{thingToPrint}* you dummy");
                    _console.WriteErrorMessage("anyway heres the full error stack");
                    throw;
                }

                _console.WriteLine(variable.Value);
            }
            else if (subExpression.Content.Matches(_functionCallRegex))
            {
                var match = _functionCallRegex.Match(subExpression.Content);
                var function = _functions[match.Groups[1].ToString()];
                Interpret(function.Expression, visibilityLevel + 1);
            }
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
        if (value.First() == '"' && value.Last() == '"')
            value = value.Trim('"');
        var variable = new VarConst(value, name, editable, reassignable, visibilityLevel);
        return variable;
    }

    private List<Expression> GetSubExpressions(string expression)
    {
        var matches = _subExpressionRegex.Matches(expression);
        var subExpressions = new List<Expression>();
        foreach (Match match in matches)
        {
            var expressionContent = match.Groups[1].ToString();
            var expressionEnd = match.Groups[2].ToString();
            var subExpression = new Expression(
                expressionContent,
                expressionEnd.Contains('?'),
                expressionEnd.Count(x => x == '!')
            );
            subExpressions.Add(subExpression);
        }

        return subExpressions;
    }
}