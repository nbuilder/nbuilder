using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
    }
}
