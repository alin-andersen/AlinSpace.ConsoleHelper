namespace AlinSpace.ConsoleHelper.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t = CommandLineInterface.ExecuteAsync("echo Test").Result;
        }
    }
}