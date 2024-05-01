using Cocona;
using DreamBerdInterpreter.ConsoleApplication.Extensions;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.ConsoleApplication;

internal class Program
{
    public static void Main()
    {
        CoconaApp.Run(
            ([Argument] string path) =>
            {
                var console = new ConsoleWrapper();

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

                var interpreter = new Interpreter.Interpreter(console);
                fileContent = fileContent.GetSanitizedExpression();
                interpreter.Interpret(fileContent);
            }
        );
    }
}