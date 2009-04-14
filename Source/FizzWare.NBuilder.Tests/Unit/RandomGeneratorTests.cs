using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomGeneratorTests
    {
        [Test]
        public void ShouldBeAbleToGenerateARandomNumber()
        {
            var generator = new RandomGenerator<int>();
            int number = generator.Generate(0, 10);

            // What can I test for?!
            Assert.That(number, Is.GreaterThanOrEqualTo(0));
            Assert.That(number, Is.LessThanOrEqualTo(10));
        }
    }
}