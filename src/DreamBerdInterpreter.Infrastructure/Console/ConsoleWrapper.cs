namespace DreamBerdInterpreter.Infrastructure.Console;

public class ConsoleWrapper : IConsole
{
    public void Write(string textToWrite)
    {
        System.Console.Write(textToWrite);
    }

    public void WriteLine(string textToWrite)
    {
        System.Console.WriteLine(textToWrite);
    }
}