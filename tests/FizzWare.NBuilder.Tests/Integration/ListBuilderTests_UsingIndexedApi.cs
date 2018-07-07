using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class ListBuilderTests_UsingIndexedApi
    {
        private const string theString = "TheString";
        private const decimal theDecimal = 10m;

        [Fact]
        public void Do_WithIndex()
        {
            var builderSettings = new BuilderSettings();
            var list = new Builder (builderSettings)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                    .Do((row, index) => row.Int = index*2)
                    .WithFactory(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            for (int i = 0; i < 10; i++)
            {
                var row = list[i];
                row.Int.ShouldBe(i*2);
            }
        }

        [Fact]
        public void And_WithIndex()
        {
            var builderSetup = new BuilderSettings();
            var list =
               new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                    .Do((row, index) => row.Int = index * 2)
                    .And((row, index) => row.Int = index * 3)
                    .WithFactory(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            for (int i = 0; i < 10; i++)
            {
                var row = list[i];
                row.Int.ShouldBe(i * 3);
            }
        }

        [Fact]
        public void WithConstructor_WithIndex()
        {
            var builderSetup = new BuilderSettings();
            var list =
          new Builder(builderSetup)
                .CreateListOfSize< MyClassWithConstructor>(10)
                .All()
                .Do((row, index) => row.Int = index * 2)
                .WithFactory(index => new MyClassWithConstructor(index, 2f))
                .Build();

            for (int i = 0; i < 10; i++)
            {
                var row = list[i];
                row.Int.ShouldBe(i * 2);
            }        
        }

    }
}