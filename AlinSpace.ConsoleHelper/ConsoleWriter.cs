namespace AlinSpace.ConsoleHelper
{
    public static class ConsoleWriter
    {
        public static void WriteLineWithPrefix(string text, string prefix = null, ConsoleColor? prefixForegroundColor = null, ConsoleColor? prefixBackgroundColor = null)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                var currentForegroundColor = Console.ForegroundColor;
                var currentBackgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = prefixForegroundColor ?? currentForegroundColor;
                Console.BackgroundColor = prefixBackgroundColor ?? currentBackgroundColor;

                Console.Write($"{prefix} ");

                Console.ForegroundColor = currentForegroundColor;
                Console.BackgroundColor = currentBackgroundColor;
            }

            Console.WriteLine(text);
        }

        public static void WriteWithPrefix(string text, string prefix = null, ConsoleColor? prefixForegroundColor = null, ConsoleColor? prefixBackgroundColor = null)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                var currentForegroundColor = Console.ForegroundColor;
                var currentBackgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = prefixForegroundColor ?? currentForegroundColor;
                Console.BackgroundColor = prefixBackgroundColor ?? currentBackgroundColor;

                Console.Write(prefix);

                Console.ForegroundColor = currentForegroundColor;
                Console.BackgroundColor = currentBackgroundColor;
            }

            Console.Write(text);
        }
    }
}
