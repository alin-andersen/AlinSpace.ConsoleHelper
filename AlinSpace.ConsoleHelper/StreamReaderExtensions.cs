using System.Text;

namespace AlinSpace.ConsoleHelper
{
    public static class StreamReaderExtensions
    {
        public static async Task<string> ReadToStringAsync(this StreamReader streamReader, Encoding encoding = null)
        {
            using var memoryStream = new MemoryStream();

            await streamReader.BaseStream.CopyToAsync(memoryStream);

            var bytes = memoryStream.ToArray();

            return (encoding ?? Encoding.UTF8).GetString(bytes);
        }
    }
}
