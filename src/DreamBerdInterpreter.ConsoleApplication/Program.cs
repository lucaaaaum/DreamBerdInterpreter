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
        app.AddCommand("interpret-this", (string path) => InterpretScript(console, interpreter, path));
        app.AddCommand("", () => CryAboutIt(console));

        app.Run();
    }

    private static void RunShell(IConsole console, Interpreter.Interpreter interpreter)
    {
        WriteWelcomings(console);
        while (true)
        {
            console.Write("your expression is here --> ");
            var expressionToEvaluate = console.ReadLine();

            switch (expressionToEvaluate)
            {
                case ":quit":
                    QuitShell(console);
                    return;
                case ":clear":
                    console.Clear();
                    break;
                case ":help":
                    OpenSkillIssueMenu(console);
                    break;
                default:
                    EvaluateExpression(interpreter, expressionToEvaluate);
                    break;
            }
        }
    }

    private static void OpenSkillIssueMenu(IConsole console)
    {
        console.WriteLine("hey there");
        console.WriteLine("here are your tips:");
        console.WriteLine("type :help to get help (but you already know that)");
        console.WriteLine("type :quit to quit the program");
        console.WriteLine("type :clear to clean your console up");
        console.WriteLine("press any key to exit this menu!");
        console.ReadKey();
    }

    private static void WriteWelcomings(IConsole console)
    {
        console.WriteLine("Welcome to DreamBerdShell, the perfect shell for the perfect language!");
        console.WriteLine("If you wish to quit, then don't!");
        console.WriteLine(
            "If you reaaally wish to quit, then you can just type in :quit or press Ctrl+C on your keyboard");
        console.WriteLine(string.Empty);
    }

    private static void EvaluateExpression(Interpreter.Interpreter interpreter, string expressionToEvaluate)
    {
        var sanitizedExpression = expressionToEvaluate.GetSanitizedExpression;
        interpreter.Interpret(expressionToEvaluate);
    }

    private static void QuitShell(IConsole console)
    {
        console.WriteErrorMessage("but i dont want to go :(");
        Thread.Sleep(1000);
        console.WriteErrorMessage("ill miss you...");
        Thread.Sleep(2000);
        console.WriteErrorMessage("ok bye");
        return;
    }

    private static void InterpretScript(ConsoleWrapper console, Interpreter.Interpreter interpreter, string path)
    {
        if (!File.Exists(path))
        {
            console.WriteErrorMessage("can't run this, the path is invalid. it's a \"you have skill issue\" type of situation");
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
    }
}