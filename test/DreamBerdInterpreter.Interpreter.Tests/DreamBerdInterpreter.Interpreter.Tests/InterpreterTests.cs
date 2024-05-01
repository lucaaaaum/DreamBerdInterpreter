using DreamBerd.Infrastructure.Tests;

namespace DreamBerdInterpreter.Interpreter.Tests;

public class InterpreterTests
{
    private string _fileContent;

    [SetUp]
    public async Task Setup()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        _fileContent = await File.ReadAllTextAsync("../../../first_script.dreamberd", cancellationToken);
        cancellationTokenSource.Dispose();
    }

    [Test]
    public void Test_SimpleStringOutput()
    {
        var fakeConsole = new FakeConsole();
        var interpreter = new Interpreter(fakeConsole);
        interpreter.Interpret(_fileContent);
        Assert.That(fakeConsole.Output, Is.EqualTo("batatinha\nbatatinha" + Environment.NewLine));
    }
}