using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class UniqueRandomGeneratorTests
    {
        [Test]
        public void ShouldGenerateUniqueNumbers()
        {
            int[] numbers = new int[5];

            var generator = new UniqueRandomGenerator<int>();
            numbers[0] = generator.Generate(6, 10);
            numbers[1] = generator.Generate(6, 10);
            numbers[2] = generator.Generate(6, 10);
            numbers[3] = generator.Generate(6, 10);
            numbers[4] = generator.Generate(6, 10);

            Assert.That(numbers[0], Is.Not.EqualTo(0));
            Assert.That(numbers[1], Is.Not.EqualTo(0));
            Assert.That(numbers[2], Is.Not.EqualTo(0));
            Assert.That(numbers[3], Is.Not.EqualTo(0));
            Assert.That(numbers[4], Is.Not.EqualTo(0));
        }
    }
}