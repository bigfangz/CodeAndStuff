
namespace ZacksSampleCode.Extensions
{
    using System.IO;
    using System.Text;
    public static class StreamExtensions
    {
        public static string ReadToString(this Stream stream)
        {
            return ReadToString(stream, 0);
        }

        public static string ReadToString(this Stream stream, long startPosition)
        {
            StringBuilder readString = new StringBuilder();
            stream.Position = startPosition;
            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    readString.AppendLine(sr.ReadLine());
                }
            }
            return readString.ToString();
        }
    }
}
