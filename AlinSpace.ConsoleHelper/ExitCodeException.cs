namespace AlinSpace.ConsoleHelper
{
    public class ExitCodeException : Exception
    {
        public int ExitCode { get; }

        public ExitCodeException(int exitCode, string message = null) : base(message ?? "Exit code does not indicate success.")
        {
            ExitCode = exitCode;
        }
    }
}
