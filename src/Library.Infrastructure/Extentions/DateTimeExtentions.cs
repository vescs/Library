using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Extentions
{
    public static class DateTimeExtentions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = dateTime.Subtract(new TimeSpan(epoch.Ticks));
            return time.Ticks / 10000;
        }
    }
}
