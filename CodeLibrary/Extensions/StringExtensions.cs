

namespace ZacksSampleCode.Extensions
{
    using System;
    using System.IO;
    using System.Linq;
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool Contains(this string str, params string[] strings)
        {
            return strings.Any(s => str.Contains(s));
        }

        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence where found returns true;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="found"></param>
        /// <returns></returns>
        public static int IndexOf(this string str, Func<char, int, bool> found)
        {
            int i = 0;
            while (i < str.Length && !found(str[i], i))
            {
                i++;
            }
            if (i == str.Length)
                return -1;
            else
                return i;
        }

        public static int? ToIntOrNull(this String str)
        {
            int result = 0;
            if (Int32.TryParse(str, out result))
                return result;
            else
                return null;
        }
        public static int ToIntOrDefault(this String str)
        {
            int result = 0;
            Int32.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// Converts this string to an integer defaulting to Default if cannot convert
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static int ToIntOrDefault(this String str, int Default)
        {
            int result = Default;
            //can this be done?
            if (str == null)
                return result;
            Int32.TryParse(str, out result);
            return result;
        }

        /// <summary>
        /// parses a string to datetime or a default value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeOrDefault(this String str, DateTime Default)
        {
            DateTime.TryParse(str, out Default);
            return Default;
        }
        /// <summary>
        /// parses a string to datetime. return min date time if parse fails
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this String str)
        {
            DateTime Default;
            DateTime.TryParse(str, out Default);
            return Default;
        }


        private static int toI(string str)
        {
            if (string.IsNullOrEmpty(str)) return -1;
            int index = 0;
            int pos = 0;
            char ch = str[0];
            while (ch >= '0' && ch <= '9' && pos < str.Length && index < 1000000)
            {
                index = index * 10 + ch - '0';
                ch = str[pos];
                pos++;
            }
            return index;
        }

    }
}
