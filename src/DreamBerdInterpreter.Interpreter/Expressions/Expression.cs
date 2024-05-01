namespace DreamBerdInterpreter.Interpreter.Expressions;

public class Expression
{
    public string Content { get; init; }
    public bool DumbProgrammerWantsToDebug { get; init; }
    public int Priority { get; init; }
}