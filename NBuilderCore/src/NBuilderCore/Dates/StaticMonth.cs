using System;

namespace NBuilderCore.Dates
{
    public class January : StaticMonth<January> { }
    public class February : StaticMonth<February> { }
    public class March : StaticMonth<March> { }
    public class April : StaticMonth<April> { }
    public class May : StaticMonth<May> { }
    public class June : StaticMonth<June> { }
    public class July : StaticMonth<July> { }
    public class August : StaticMonth<August> { }
    public class September : StaticMonth<September> { }
    public class October : StaticMonth<October> { }
    public class November : StaticMonth<November> { }
    public class December : StaticMonth<December> { }
    
    public abstract class StaticMonth<TMonthNum> where TMonthNum : StaticMonth<TMonthNum>
    {
        private static int MonthNum
        {
            get
            {
                if (typeof(TMonthNum) == typeof(January)) return 1;
                if (typeof(TMonthNum) == typeof(February)) return 2;
                if (typeof(TMonthNum) == typeof(March)) return 3;
                if (typeof(TMonthNum) == typeof(April)) return 4;
                if (typeof(TMonthNum) == typeof(May)) return 5;
                if (typeof(TMonthNum) == typeof(June)) return 6;
                if (typeof(TMonthNum) == typeof(July)) return 7;
                if (typeof(TMonthNum) == typeof(August)) return 8;
                if (typeof(TMonthNum) == typeof(September)) return 9;
                if (typeof(TMonthNum) == typeof(October)) return 10;
                if (typeof(TMonthNum) == typeof(November)) return 11;
                
                return 12;
            }
        }

        public static DateTime The(int day)
        {
            return new DateTime(DateTime.Now.Year, MonthNum, day);
        }

        public static DateTime The1st
        {
            get { return The(1); }
        }

        public static DateTime The2nd
        {
            get { return The(2); }
        }

        public static DateTime The3rd
        {
            get { return The(3); }
        }

        public static DateTime The4th
        {
            get { return The(4); }
        }

        public static DateTime The5th
        {
            get { return The(5); }
        }

        public static DateTime The6th
        {
            get { return The(6); }
        }

        public static DateTime The7th
        {
            get { return The(7); }
        }

        public static DateTime The8th
        {
            get { return The(8); }
        }

        public static DateTime The9th
        {
            get { return The(9); }
        }

        public static DateTime The10th
        {
            get { return The(10); }
        }

        public static DateTime The11th
        {
            get { return The(11); }
        }

        public static DateTime The12th
        {
            get { return The(12); }
        }

        public static DateTime The13th
        {
            get { return The(13); }
        }

        public static DateTime The14th
        {
            get { return The(14); }
        }

        public static DateTime The15th
        {
            get { return The(15); }
        }

        public static DateTime The16th
        {
            get { return The(16); }
        }

        public static DateTime The17th
        {
            get { return The(17); }
        }

        public static DateTime The18th
        {
            get { return The(18); }
        }

        public static DateTime The19th
        {
            get { return The(19); }
        }

        public static DateTime The20th
        {
            get { return The(20); }
        }

        public static DateTime The21st
        {
            get { return The(21); }
        }

        public static DateTime The22nd
        {
            get { return The(22); }
        }

        public static DateTime The23rd
        {
            get { return The(23); }
        }

        public static DateTime The24th
        {
            get { return The(24); }
        }

        public static DateTime The25th
        {
            get { return The(25); }
        }

        public static DateTime The26th
        {
            get { return The(26); }
        }

        public static DateTime The27th
        {
            get { return The(27); }
        }

        public static DateTime The28th
        {
            get { return The(28); }
        }

        public static DateTime The29th
        {
            get { return The(29); }
        }

        public static DateTime The30th
        {
            get { return The(30); }
        }

        public static DateTime The31st
        {
            get { return The(31); }
        }
    }
}