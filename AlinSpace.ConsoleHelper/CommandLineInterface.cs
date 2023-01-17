using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AlinSpace.ConsoleHelper
{
    public static class CommandLineInterface
    {
        public static async Task<string> ExecuteAsync(string command, CancellationToken cancellationToken = default)
        {
            ProcessStartInfo processStartInfo;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                processStartInfo = new ProcessStartInfo("/bin/sh", $"-c \"{command}\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                };
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                processStartInfo = new ProcessStartInfo("CMD.exe", $"/C \"{command}\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = false
                };
            }
            else
            {
                throw new PlatformNotSupportedException();
            }

            var process = Process.Start(processStartInfo);

            if (process == null)
            {
                throw new Exception("Unable to start process.");
            }

            while (!process.HasExited)
            {
                await process.WaitForExitAsync(cancellationToken);
            }

            if (process.ExitCode != 0)
            {
                throw new ExitCodeException(process.ExitCode);
            }

            try
            {
                var output = await process.StandardOutput.ReadToStringAsync();
                return output;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
