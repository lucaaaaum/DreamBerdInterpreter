using Cocona;
using DreamBerdInterpreter.Infrastructure.Console;

namespace DreamBerdInterpreter.ConsoleApplication;

internal class Program
{
    public static void Main() =>
        CoconaApp.Run(([Argument] string path) =>
        {
            if (!File.Exists(path))
                Console.WriteLine("Bro I can't run this, the path is invalid!");
            var fileContent = string.Empty;
            try
            {
                fileContent = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("sumthing happend idunno");
                Console.WriteLine("here's the log if you know dotnet:");
                Console.WriteLine(e);
            }
            var interpreter = new Interpreter.Interpreter(new ConsoleWrapper());
            interpreter.Interpret(fileContent);
        });
}