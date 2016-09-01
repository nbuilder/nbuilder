using System;

namespace NBuilderCore.Dates
{
    public static class DateTimeExtensions
    {
        public static DateTime At(this DateTime date, int hour, int minute)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        public static DateTime At(this DateTime date, int hour, int minute, int second)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
        }
    }
}