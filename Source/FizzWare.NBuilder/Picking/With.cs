using FizzWare.NBuilder.Picking;

namespace FizzWare.NBuilder
{
    public class With
    {
        public static UpToPickerConstraint UpTo(int count)
        {
            return new UpToPickerConstraint(count);
        }

        public static BetweenPickerConstraint Between(int lower)
        {
            return new BetweenPickerConstraint(lower);
        }

        public static ExactlyPickerConstraint Exactly(int count)
        {
            return new ExactlyPickerConstraint(count);
        }

        public static AtLeastPickerConstraint AtLeast(int atLeast)
        {
            return new AtLeastPickerConstraint(atLeast);
        }
    }
}