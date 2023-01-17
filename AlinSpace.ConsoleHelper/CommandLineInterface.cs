using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AlinSpace.ConsoleHelper
{
    public static class CommandLineInterface
    {
        public static async Task<string> ExecuteAsync(string command, bool throwException = false, CancellationToken cancellationToken = default)
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
                    RedirectStandardOutput = true
                };
            }
            else
            {
                if (throwException)
                {
                    throw new PlatformNotSupportedException();
                }

                return null;
            }

            var process = Process.Start(processStartInfo);

            if (process == null)
            {
                if (throwException)
                {
                    throw new Exception("Unable to start process.");
                }

                return null;
            }

            while (!process.HasExited)
            {
                try
                {
                    await process.WaitForExitAsync(cancellationToken);
                }
                catch(Exception)
                {
                    if (throwException)
                    {
                        throw;
                    }

                    return null;
                }
            }

            if (process.ExitCode != 0)
            {
                if (throwException)
                {
                    throw new ExitCodeException(process.ExitCode);
                }

                return null;
            }

            try
            {
                var output = await process.StandardOutput.ReadToStringAsync();
                return output;
            }
            catch (Exception)
            {
                if (throwException)
                {
                    throw;
                }

                return null;
            }
        }
    }
}
