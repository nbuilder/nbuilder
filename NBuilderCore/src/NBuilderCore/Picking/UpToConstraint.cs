using NBuilderCore.Generators;

namespace NBuilderCore.Picking
{
    public class UpToConstraint : Constraint
    {
        private readonly IRandomGenerator randomGenerator;
        private readonly int upper;

        public UpToConstraint(IRandomGenerator uniqueRandomGenerator, int upper)
        {
            this.randomGenerator = uniqueRandomGenerator;
            this.upper = upper;
        }

        public override int GetEnd()
        {
            return randomGenerator.Next(0, upper);
        }
    }
}