using Cocona;
using DreamBerdInterpreter.ConsoleApplication.Extensions;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.ConsoleApplication;

internal class Program
{
    public static void Main()
    {
        var builder = CoconaApp.CreateBuilder();
        var app = builder.Build();
        var console = new ConsoleWrapper();
        var interpreter = new Interpreter.Interpreter(console);

        app.AddCommand("shell", () => RunShell(console, interpreter));
        app.AddCommand("interpret-this", (string path) => EvaluateExpression(console, interpreter, path));
        app.AddCommand("", () => CryAboutIt(console));
        
        app.Run();
    }

    private static void RunShell(IConsole console, Interpreter.Interpreter interpreter)
    {
        console.WriteLine("Welcome to DreamBerdShell, the perfect shell for the perfect language!");
        console.WriteLine("If you wish to quit, then don't!");
        console.WriteLine(
            "If you reaaally wish to quit, then you can just type in :quit or press Ctrl+C on your keyboard");
        console.WriteLine("");
        console.Write("your expression is here --> ");
        while (true)
        {
            var expressionToEvaluate = console.ReadLine();

            if (expressionToEvaluate == ":quit")
            {
                console.WriteErrorMessage("but i dont want to go :(");
                Thread.Sleep(1000);
                console.WriteErrorMessage("ill miss you...");
                Thread.Sleep(2000);
                console.WriteErrorMessage("ok bye");
                return;
            }

            var sanitizedExpression = expressionToEvaluate.GetSanitizedExpression;
            interpreter.Interpret(expressionToEvaluate);
        }
    }

    private static void EvaluateExpression(ConsoleWrapper console, Interpreter.Interpreter interpreter, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
        }

        if (!File.Exists(path))
        {
            console.WriteErrorMessage("Bro I can't run this, the path is invalid!");
            return;
        }

        var fileContent = string.Empty;
        try
        {
            fileContent = File.ReadAllText(path);
        }
        catch (Exception e)
        {
            console.WriteErrorMessage("sumthing happend idunno");
            console.WriteErrorMessage("here's the log if you know dotnet:");
            console.WriteErrorMessage(e.ToString());
        }

        fileContent = fileContent.GetSanitizedExpression();
        interpreter.Interpret(fileContent);
    }

    private static void CryAboutIt(ConsoleWrapper console)
    {
        console.WriteErrorMessage(
            "what am I supposed to do? you haven't given me either a path nor asked me to open the shell");
        console.WriteErrorMessage("guess i'll just, i dunno... run *sudo rm -rf /*. just for fun");
        console.WriteErrorMessage("1...");
        Thread.Sleep(1000);
        console.WriteErrorMessage("2...");
        Thread.Sleep(1000);
        console.WriteErrorMessage("3...");
        Thread.Sleep(1000);
        console.WriteErrorMessage("just kidding. ill die now. bye bye!!");
        return;
    }
}