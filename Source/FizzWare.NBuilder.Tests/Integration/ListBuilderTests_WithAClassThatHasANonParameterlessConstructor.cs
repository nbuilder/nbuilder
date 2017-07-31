using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class ListBuilderTests_WithAClassThatHasANonParameterlessConstructor
    {
        private const string theString = "TheString";
        private const decimal theDecimal = 10m;

        #pragma warning disable 0618 // (prevent warning for using obsolete method)
        [Fact]
        public void should_be_able_to_create_a_list_using_legacy_syntax()
        {
            var builderSetup = new BuilderSettings();
            var list =
               new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                        .WithConstructor(() => new MyClassWithConstructor(theString, theDecimal))
                    .Build();

            list.Count.ShouldBe(10);
        }
        #pragma warning restore 0618

        [Fact]
        public void should_be_able_to_use_WithConstructor()
        {
            var builderSetup = new BuilderSettings();
            var list =
               new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                        .WithConstructor(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            list.Count.ShouldBe(10);
        }

        [Fact]
        public void should_be_able_to_use_IsConstructedUsing()
        {
            var builderSetup = new BuilderSettings();
            var list =
               new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(1)
                    .TheFirst(1)
                        .WithConstructor(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            list.Count.ShouldBe(1);
        }

        #pragma warning disable 0618 // (prevent warning for using obsolete method)
        [Fact]
        public void should_set_properties_through_constructor_args_using_legacy_syntax()
        {
            var builderSetup = new BuilderSettings();
            var list =
                new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                        .WithConstructor(() => new MyClassWithConstructor(theString, theDecimal))
                    .Build();

            list[0].String.ShouldBe(theString);
            list[0].Decimal.ShouldBe(theDecimal);
        }

        [Fact]
        public void should_be_able_to_use_legacy_singular_syntax()
        {
            var builderSetup = new BuilderSettings();
            var list =
               new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(1)
                    .TheFirst(1)
                        .WithConstructor(() => new MyClassWithConstructor(theString, theDecimal))
                    .Build();

            list[0].String.ShouldBe(theString);
            list[0].Decimal.ShouldBe(theDecimal);
        }
        #pragma warning restore 0618
    }
}