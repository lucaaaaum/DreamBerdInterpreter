using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.Interpreter;

public class Interpreter(IConsole console)
{
    private readonly IConsole _console = console;

    public void Interpret(string fileContent)
    {
        
    }
}