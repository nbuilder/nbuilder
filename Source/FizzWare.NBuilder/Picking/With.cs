using System;

namespace FizzWare.NBuilder
{
    public class With
    {
        public static UpToConstraint UpTo(int count)
        {
            return new UpToConstraint(new RandomGenerator<int>(), count);
        }

        public static BetweenConstraint Between(int lower)
        {
            return new BetweenConstraint(new RandomGenerator<int>(), lower);
        }

        public static BetweenConstraint Between(int lower, int upper)
        {
            return new BetweenConstraint(new RandomGenerator<int>(), lower, upper);
        }

        public static ExactlyConstraint Exactly(int count)
        {
            return new ExactlyConstraint(count);
        }
    }
}