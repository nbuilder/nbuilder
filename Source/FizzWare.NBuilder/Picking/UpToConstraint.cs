using System;

namespace FizzWare.NBuilder
{
    public class UpToConstraint : Constraint
    {
        private readonly IRandomGenerator<int> uniqueRandomGenerator;
        private readonly int upper;

        public UpToConstraint(IRandomGenerator<int> uniqueRandomGenerator, int upper)
        {
            this.uniqueRandomGenerator = uniqueRandomGenerator;
            this.upper = upper;
        }

        public override int GetEnd()
        {
            return uniqueRandomGenerator.Generate(0, upper);
        }
    }
}