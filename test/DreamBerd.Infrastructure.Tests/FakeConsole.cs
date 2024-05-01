using System.Text;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerd.Infrastructure.Tests;

public class FakeConsole : IConsole
{
    private StringBuilder _stringBuilder = new();
    public string Output => _stringBuilder.ToString();
    
    public void Write(string textToWrite) => _stringBuilder.Append(textToWrite);

    public void WriteLine(string textToWrite) => _stringBuilder.AppendLine(textToWrite);

    public void WriteDebugMessage(string messageToWrite)
    {
        var prefix = "\ue490 Debug Info: ";
        WriteLine(prefix + messageToWrite);
    }

    public void WriteErrorMessage(string messageToWrite)
    {
        var prefix = "\uf1e2 Error Info: ";
        WriteLine(prefix + messageToWrite);
    }
    
    public char Read()
    {
        throw new NotImplementedException();
    }

    public string ReadLine()
    {
        throw new NotImplementedException();
    }
}