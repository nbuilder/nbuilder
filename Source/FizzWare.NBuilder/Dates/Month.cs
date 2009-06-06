using System;

namespace FizzWare.NBuilder.Dates
{
    public class Month
    {
        private readonly int monthNumber;
        private readonly int year;
        
        public Month(int monthNumber)
            : this (monthNumber, DateTime.Now.Year)
        {
        }

        public Month(int monthNumber, int year)
        {
            this.monthNumber = monthNumber;
            this.year = year;
        }

        public DateTime The(int dayOfMonth)
        {
            return new DateTime(year, monthNumber, dayOfMonth);
        }

        public DateTime The1st
        {
            get { return The(1); }
        }

        public DateTime The2nd
        {
            get { return The(2); }
        }

        public DateTime The3rd
        {
            get { return The(3); }
        }

        public DateTime The4th
        {
            get { return The(4); }
        }

        public DateTime The5th
        {
            get { return The(5); }
        }

        public DateTime The6th
        {
            get { return The(6); }
        }

        public DateTime The7th
        {
            get { return The(7); }
        }

        public DateTime The8th
        {
            get { return The(8); }
        }

        public DateTime The9th
        {
            get { return The(9); }
        }

        public DateTime The10th
        {
            get { return The(10); }
        }

        public DateTime The11th
        {
            get { return The(11); }
        }

        public DateTime The12th
        {
            get { return The(12); }
        }

        public DateTime The13th
        {
            get { return The(13); }
        }

        public DateTime The14th
        {
            get { return The(14); }
        }

        public DateTime The15th
        {
            get { return The(15); }
        }

        public DateTime The16th
        {
            get { return The(16); }
        }

        public DateTime The17th
        {
            get { return The(17); }
        }

        public DateTime The18th
        {
            get { return The(18); }
        }

        public DateTime The19th
        {
            get { return The(19); }
        }

        public DateTime The20th
        {
            get { return The(20); }
        }

        public DateTime The21st
        {
            get { return The(21); }
        }

        public DateTime The22nd
        {
            get { return The(22); }
        }

        public DateTime The23rd
        {
            get { return The(23); }
        }

        public DateTime The24th
        {
            get { return The(24); }
        }

        public DateTime The25th
        {
            get { return The(25); }
        }

        public DateTime The26th
        {
            get { return The(26); }
        }

        public DateTime The27th
        {
            get { return The(27); }
        }

        public DateTime The28th
        {
            get { return The(28); }
        }

        public DateTime The29th
        {
            get { return The(29); }
        }

        public DateTime The30th
        {
            get { return The(30); }
        }

        public DateTime The31st
        {
            get { return The(31); }
        }
    }
}