using System;

namespace NBuilderCore.Dates
{
    public static class The
    {
        public static Year Year(int year)
        {
            return new Year(year);
        }

        public static TimeSpan Time(int hours, int minutes)
        {
            return new TimeSpan(0, hours, minutes, 0);
        }

        public static TimeSpan Time(int hours, int minutes, int seconds)
        {
            return new TimeSpan(0, hours, minutes, seconds);
        }
    }
}