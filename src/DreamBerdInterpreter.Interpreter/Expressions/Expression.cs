namespace DreamBerdInterpreter.Interpreter.Expressions;

public class Expression
{
    public string Type { get; init; }
    public string Content { get; set; }
    public int Priority { get; init; }
}