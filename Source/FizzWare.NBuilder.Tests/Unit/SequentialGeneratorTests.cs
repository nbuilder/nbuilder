using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class SequentialGeneratorTests
    {
        private SequentialGenerator<int> generator;

        [SetUp]
        public void SetUp()
        {
            generator = new SequentialGenerator<int>();
        }

        [Test]
        public void ShouldBeAbleToGenerate()
        {
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));
            Assert.That(generator.Generate(), Is.EqualTo(2));
        }

        [Test]
        public void ShouldBeAbleToSetIncrement()
        {
            generator.Increment = 2;
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(2));
            Assert.That(generator.Generate(), Is.EqualTo(4));
            Assert.That(generator.Generate(), Is.EqualTo(6));
        }

        [Test]
        public void ShouldBeAbleToGenerateInReverse()
        {
            generator.Direction = GeneratorDirection.Descending;
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(-1));
            Assert.That(generator.Generate(), Is.EqualTo(-2));
            Assert.That(generator.Generate(), Is.EqualTo(-3));
        }

        [DebuggerDisplay("Property: {Property}")]
        public class TestClass
        {
            public string Property { get; set; }
        }

        [Test]
        public void RangeDeclarationsShouldExecuteInOrderOfStartingPosition()
        {
            var generator = new SequentialGenerator<int>();
            var build = new Builder<TestClass>(new BuilderSetup())
                .CreateListOfSize(10)
                .All()
                    .Do(x => x.Property = "item")
                .TheFirst(2)
                    .Do(x => x.Property += String.Format("{0}{1}", generator.Generate(), generator.Generate()))
                .TheNext(6)
                    .Do(x => x.Property += generator.Generate())
                .Build();

            var expected = new[]{"item01", "item23", "item4", "item5", "item6", "item7", "item8", "item9", "item", "item"};
            var actual = build.Select(row => row.Property).ToArray();

            Assert.That(actual, Is.EquivalentTo(expected), string.Join(", ", expected));

        }
    }
}
