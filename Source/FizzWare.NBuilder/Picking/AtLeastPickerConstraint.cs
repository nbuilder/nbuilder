namespace FizzWare.NBuilder.Picking
{
    public  class AtLeastPickerConstraint : PickerConstraint
    {
        private readonly int atLeast;

        public AtLeastPickerConstraint(int atLeast)
        {
            this.atLeast = atLeast;
        }

        public override int GetStart(int max)
        {
            //return random.Next(atLeast, max);

            return 0;
        }

        public override int GetEnd(int max)
        {
            return random.Next(atLeast, max);
        }
    }
}