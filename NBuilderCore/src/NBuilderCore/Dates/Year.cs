namespace NBuilderCore.Dates
{
    public class Year
    {
        private readonly int year;

        public Year(int year)
        {
            this.year = year;
        }

        public Months On
        {
            get
            {
                return new Months(year);
            }
        }
    }
}