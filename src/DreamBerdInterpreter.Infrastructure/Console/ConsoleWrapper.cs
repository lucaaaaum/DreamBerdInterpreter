namespace DreamBerdInterpreter.Infrastructure.Console;

public class ConsoleWrapper : IConsole
{
    public void Write(string textToWrite) => System.Console.Write(textToWrite);

    public void WriteLine(string textToWrite) => System.Console.WriteLine(textToWrite);

    public void WriteDebugMessage(string messageToWrite)
    {
        var prefix = "\ue490 Debug Info: ";
        System.Console.BackgroundColor = ConsoleColor.Yellow;
        System.Console.ForegroundColor = ConsoleColor.Gray;
        Write(prefix);
        System.Console.BackgroundColor = default;
        System.Console.ForegroundColor = default;
        WriteLine(messageToWrite);
    }

    public void WriteErrorMessage(string messageToWrite)
    {
        var prefix = "\uf1e2 Error Info: ";
        System.Console.BackgroundColor = ConsoleColor.Red;
        System.Console.ForegroundColor = ConsoleColor.Gray;
        Write(prefix);
        System.Console.BackgroundColor = default;
        System.Console.ForegroundColor = default;
        WriteLine(messageToWrite);
    }
}