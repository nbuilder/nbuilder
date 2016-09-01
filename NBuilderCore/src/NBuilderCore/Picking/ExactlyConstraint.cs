namespace NBuilderCore.Picking
{
    public class ExactlyConstraint : Constraint
    {
        private readonly int count;

        public ExactlyConstraint(int count)
            : base()
        {
            this.count = count;
        }

        public override int GetEnd()
        {
            return count;
        }
    }
}