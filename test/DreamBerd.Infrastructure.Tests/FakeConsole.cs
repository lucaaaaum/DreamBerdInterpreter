using System.Text;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerd.Infrastructure.Tests;

public class FakeConsole : IConsole
{
    private StringBuilder _stringBuilder = new();
    
    public void Write(string textToWrite) => _stringBuilder.Append(textToWrite);

    public void WriteLine(string textToWrite) => _stringBuilder.AppendLine(textToWrite);
}