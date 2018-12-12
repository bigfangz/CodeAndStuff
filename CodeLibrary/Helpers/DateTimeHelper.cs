

namespace ZacksSampleCode.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class DateTimeHelper
    {
        /// <summary>
        /// Create a datetime that is formated so that is it friendly for filenames ( no / )
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FileFriendlyDateTime(DateTime dateTime)
        {
            return string.Format("{0}-{1}-{2}_{3}-{4}", dateTime.Month, dateTime.Day, dateTime.Year, dateTime.Hour, dateTime.Minute);
        }
        public static string FileFriendlyDate(DateTime dateTime)
        {
            return string.Format("{0}-{1}-{2}", dateTime.Month, dateTime.Day, dateTime.Year);
        }
        public static string UIFriendlyDateTime(long ticks)
        {
            return UIFriendlyDateTime(new DateTime(ticks));
        }
        public static string UIFriendlyDateTime(DateTime dateTime)
        {
            return string.Format("{0}/{1}/{2} {3}:{4}", dateTime.Month, dateTime.Day, dateTime.Year, dateTime.Hour, dateTime.Minute);
        }
 
    }

}
