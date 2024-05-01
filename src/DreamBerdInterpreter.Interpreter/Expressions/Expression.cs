namespace DreamBerdInterpreter.Interpreter.Expressions;

public class Expression(string content, bool dumbProgrammerWantsToDebug, int priority)
{
    public string Content { get; init; } = content;
    public bool DumbProgrammerWantsToDebug { get; init; } = dumbProgrammerWantsToDebug;
    public int Priority { get; init; } = priority;
}