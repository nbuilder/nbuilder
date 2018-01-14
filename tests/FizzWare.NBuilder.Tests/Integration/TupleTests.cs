using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    public class TupleTests
    {
        [Fact]
        public void _Build_Tuple_DoesNotReturnNullValues()
        {

            var z = (Class1: new MyClass(), Class2: new MyClass());
            var y = z.GetType();

            var target = new Builder().CreateNew<(MyClass Class1, MyClass Class2)>()
                .With(x=>
                {
                    var argItem1 = new MyClass();
                    x.Class1 = argItem1;
                    return x.Class1;
                })
                .With(x => x.Class2 = new MyClass());
            var actual = target.Build();
            actual.Class1.ShouldNotBeNull();
            actual.Class2.ShouldNotBeNull();
        }
    }
}