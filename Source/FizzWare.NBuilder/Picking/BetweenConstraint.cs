using System;

namespace FizzWare.NBuilder
{
    public class BetweenConstraint : Constraint
    {
        private readonly IRandomGenerator<int> uniqueRandomGenerator;
        private readonly int lower;
        private int upper;

        public BetweenConstraint(IRandomGenerator<int> uniqueRandomGenerator, int lower)
        {
            this.uniqueRandomGenerator = uniqueRandomGenerator;
            this.lower = lower;
        }

        public BetweenConstraint(IRandomGenerator<int> uniqueRandomGenerator, int lower, int upper)
            : this(uniqueRandomGenerator, lower)
        {
            this.upper = upper;
        }

        public override int GetEnd()
        {
            return uniqueRandomGenerator.Generate(lower, upper);
        }

        public BetweenConstraint And(int end)
        {
            this.upper = end;
            return this;
        }
    }
}