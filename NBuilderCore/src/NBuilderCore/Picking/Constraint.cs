namespace NBuilderCore.Picking
{
    public abstract class Constraint : IConstraint
    {
        protected Constraint()
        {
        }

        // This allows the code to read better
        public Constraint Elements
        {
            get
            {
                return this;
            }
        }

        public abstract int GetEnd();
    }
}