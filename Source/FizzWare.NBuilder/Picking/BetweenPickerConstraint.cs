namespace FizzWare.NBuilder.Picking
{
    public class BetweenPickerConstraint : PickerConstraint
    {
        private int lower;
        private int upper;

        private int start;

        public BetweenPickerConstraint(int lower)
        {
            this.lower = lower;
        }

        public BetweenPickerConstraint(int lower, int upper)
        {
            this.lower = lower;
            this.upper = upper;
        }

        public override int GetStart(int max)
        {
            start = random.Next(lower, upper);
            return start;
        }

        public override int GetEnd(int max)
        {
            return random.Next(start, upper);
        }

        public BetweenPickerConstraint And(int end)
        {
            this.upper = end;
            return this;
        }
    }
}