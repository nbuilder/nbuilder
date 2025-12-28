using System;

namespace FizzWare.NBuilder
{
    public class With
    {
        public static UpToConstraint UpTo(int count)
        {
            return new UpToConstraint(new UniqueRandomGenerator(), count);
        }

        public static BetweenConstraint Between(int lower)
        {
            return new BetweenConstraint(new UniqueRandomGenerator(), lower);
        }

        public static BetweenConstraint Between(int lower, int upper)
        {
            return new BetweenConstraint(new UniqueRandomGenerator(), lower, upper);
        }

        public static ExactlyConstraint Exactly(int count)
        {
            return new ExactlyConstraint(count);
        }
    }
}