using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class SingleObjectBuilderTests_WithAClassThatHasANonParameterlessConstructor
    {
        private const string theString = "string";
        private const decimal theDecimal = 10m;
        private const int theInt = 5;
        private const float theFloat = 15f;

        #pragma warning disable 0618 // (prevent warning for using obsolete method)
        [Fact]
        public void ShouldBeAbleToCreateAnObject()
        {
            var obj = Builder<MyClassWithConstructor>
                .CreateNew()
                    .WithConstructor(() => new MyClassWithConstructor(theString, theDecimal))
                .Build()
                ;

            obj.String.ShouldBe(theString);
            obj.Decimal.ShouldBe(theDecimal);
        }

        [Fact]
        public void ShouldChooseCorrectConstructor()
        {
            var builderSetup = new BuilderSettings();
            var obj = Builder<MyClassWithConstructor>.CreateNew()
                    .WithConstructor(() => new MyClassWithConstructor(theInt, theFloat))
                .Build();

            obj.Int.ShouldBe(theInt);
            obj.Float.ShouldBe(theFloat);
        }
        #pragma warning restore 0618
    }
}