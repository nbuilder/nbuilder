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
            var target = new Builder().CreateNew<(MyClass Class1, MyClass Class2)>();
            var actual = target.Build();
            actual.Class1.ShouldNotBeNull();
            actual.Class2.ShouldNotBeNull();
        }
    }
}