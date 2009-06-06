using System;

namespace FizzWare.NBuilder.Dates
{
    public class Today
    {
        public static DateTime At(int hours, int minutes)
        {
            var time = DateTime.Today.AddHours(hours).AddMinutes(minutes);
            return time;
        }

        public static DateTime At(int hours, int minutes, int seconds)
        {
            var time = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
            return time;
        }
    }
}