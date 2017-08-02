using System;

namespace FizzWare.NBuilder.Dates
{
    public static class At
    {
        public static TimeSpan Time(int hour, int minute)
        {
            return new TimeSpan(hour, minute, 0);
        }

        public static TimeSpan Time(int hour, int minute, int seconds)
        {
            return new TimeSpan(hour, minute, seconds);
        }
    }
}