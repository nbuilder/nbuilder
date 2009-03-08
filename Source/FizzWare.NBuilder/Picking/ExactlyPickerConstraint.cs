namespace FizzWare.NBuilder
{
    public class ExactlyPickerConstraint : PickerConstraint
    {
        private readonly int count;

        public ExactlyPickerConstraint(int count)
        {
            this.count = count;
        }

        public override int GetStart(int max)
        {
            return 0;
        }

        public override int GetEnd(int max)
        {
            return count;
        }
    }
}