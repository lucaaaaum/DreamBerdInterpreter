namespace DreamBerdInterpreter.Infrastructure.Console;

public class ConsoleWrapper : IConsole
{
    public void Write(string textToWrite) => System.Console.Write(textToWrite);

    public void WriteLine(string textToWrite) => System.Console.WriteLine(textToWrite);

    public void WriteDebugMessage(string messageToWrite)
    {
        var prefix = "\ue490  Debug Info:";
        var defaultBackgroundColor = System.Console.BackgroundColor;
        var defaultForeGroundColor = System.Console.ForegroundColor;
        System.Console.BackgroundColor = ConsoleColor.Yellow;
        System.Console.ForegroundColor = ConsoleColor.Black;
        Write(prefix);
        System.Console.BackgroundColor = defaultBackgroundColor;
        System.Console.ForegroundColor = defaultForeGroundColor;
        WriteLine(" " + messageToWrite);
    }

    public void WriteErrorMessage(string messageToWrite)
    {
        var prefix = "\uf1e2  Error Info: ";
        var defaultBackgroundColor = System.Console.BackgroundColor;
        var defaultForeGroundColor = System.Console.ForegroundColor;
        System.Console.BackgroundColor = ConsoleColor.Red;
        System.Console.ForegroundColor = ConsoleColor.Black;
        Write(prefix);
        System.Console.BackgroundColor = defaultBackgroundColor;
        System.Console.ForegroundColor = defaultForeGroundColor;
        WriteLine(" " + messageToWrite);
    }

    public char Read()
    {
        return (char)System.Console.Read();
    }

    public char ReadKey() => System.Console.ReadKey(true).KeyChar;

    public string ReadLine() => System.Console.ReadLine() ?? string.Empty;

    public void Clear() => System.Console.Clear();
}