namespace DreamBerdInterpreter.Infrastructure.Console;

public interface IConsole
{
    public void Write(string textToWrite);
    public void WriteLine(string textToWrite);
    public void WriteErrorMessage(string messageToWrite);
    public void WriteDebugMessage(string messageToWrite);
    public char Read();
    public string ReadLine();
}