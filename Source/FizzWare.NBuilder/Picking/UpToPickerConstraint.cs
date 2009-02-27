namespace FizzWare.NBuilder
{
    public class UpToPickerConstraint : PickerConstraint
    {
        private readonly int count;

        public UpToPickerConstraint(int count)
        {
            this.count = count;
        }

        public override int GetStart(int max)
        {
            return 0;
        }

        public override int GetEnd(int max)
        {
            return random.Next(0, count);
        }
    }
}