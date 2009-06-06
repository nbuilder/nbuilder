namespace FizzWare.NBuilder.Dates
{
    public class Months
    {
        private readonly int year;

        public Months(int year)
        {
            this.year = year;
        }

        public Month January
        {
            get { return new Month(1, year); }
        }

        public Month February
        {
            get { return new Month(2, year); }
        }

        public Month March
        {
            get { return new Month(3, year); }
        }

        public Month April
        {
            get { return new Month(4, year); }
        }

        public Month May
        {
            get { return new Month(5, year); }
        }

        public Month June
        {
            get { return new Month(6, year); }
        }

        public Month July
        {
            get { return new Month(7, year); }
        }

        public Month August
        {
            get { return new Month(8, year); }
        }

        public Month September
        {
            get { return new Month(9, year); }
        }

        public Month October
        {
            get { return new Month(10, year); }
        }

        public Month November
        {
            get { return new Month(11, year); }
        }

        public Month December
        {
            get { return new Month(12, year); }
        }
    }
}