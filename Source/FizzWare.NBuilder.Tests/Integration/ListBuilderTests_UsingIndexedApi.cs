using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class ListBuilderTests_UsingIndexedApi
    {
        private const string theString = "TheString";
        private const decimal theDecimal = 10m;

        [Test]
        public void Do_WithIndex()
        {
            var builderSetup = new BuilderSettings();
            var list =
             new Builder (builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                    .Do((row, index) => row.Int = index*2)
                    .WithConstructor(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            for (int i = 0; i < 10; i++)
            {
                var row = list[i];
                Assert.That(row.Int, Is.EqualTo(i*2));
            }
        }

        [Test]
        public void And_WithIndex()
        {
            var builderSetup = new BuilderSettings();
            var list =
               new Builder(builderSetup)
                    .CreateListOfSize< MyClassWithConstructor>(10)
                    .All()
                    .Do((row, index) => row.Int = index * 2)
                    .And((row, index) => row.Int = index * 3)
                    .WithConstructor(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            for (int i = 0; i < 10; i++)
            {
                var row = list[i];
                Assert.That(row.Int, Is.EqualTo(i * 3));
            }
        }

        [Test]
        public void WithConstructor_WithIndex()
        {
            var builderSetup = new BuilderSettings();
            var list =
          new Builder(builderSetup)
                .CreateListOfSize< MyClassWithConstructor>(10)
                .All()
                .Do((row, index) => row.Int = index * 2)
                .WithConstructor(index => new MyClassWithConstructor(index, 2f))
                .Build();

            for (int i = 0; i < 10; i++)
            {
                var row = list[i];
                Assert.That(row.Int, Is.EqualTo(i * 2));
            }        
        }

    }
}