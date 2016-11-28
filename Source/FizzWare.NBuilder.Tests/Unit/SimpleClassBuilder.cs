using FizzWare.NBuilder.Generators;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class SimpleClassBuilder
    {
        public static int String1Length = 9;
        public static int String2Length = 4;

        public static ISingleObjectBuilder<SimpleClass> New
        {
            get
            {
                return Builder<SimpleClass>.CreateNew()
                    .With(x => x.String1 = GetRandom.String(String1Length))
                    .With(x => x.String2 = GetRandom.String(String2Length));
            }
        }
    }
}