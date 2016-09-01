using NBuilderCore.Generators;

namespace NBuilderCore.Picking
{
    public class With
    {
        public static UpToConstraint UpTo(int count)
        {
            return new UpToConstraint(new RandomGenerator(), count);
        }

        public static BetweenConstraint Between(int lower)
        {
            return new BetweenConstraint(new RandomGenerator(), lower);
        }

        public static BetweenConstraint Between(int lower, int upper)
        {
            return new BetweenConstraint(new RandomGenerator(), lower, upper);
        }

        public static ExactlyConstraint Exactly(int count)
        {
            return new ExactlyConstraint(count);
        }
    }
}