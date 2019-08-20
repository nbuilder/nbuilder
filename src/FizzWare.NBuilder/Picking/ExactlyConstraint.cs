namespace FizzWare.NBuilder
{
    public class ExactlyConstraint : Constraint
    {
        private readonly int count;

        public ExactlyConstraint(int count)
        {
            this.count = count;
        }

        public override int GetEnd()
        {
            return count;
        }
    }
}