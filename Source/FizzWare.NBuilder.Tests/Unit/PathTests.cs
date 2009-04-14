using System;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using NUnit.Framework;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class PathTests
    {
        private Path path;

        [SetUp]
        public void SetUp()
        {
            path = new Path();
        }

        [Test]
        public void ShouldBeAbleToConvertToString()
        {
            Assert.That(path.ToString(), Is.EqualTo("1"));
        }

        [Test]
        public void ShouldBeAbleToIncreaseDepth()
        {
            path.IncreaseDepth();
            Assert.That(path.ToString(), Is.EqualTo("1.1"));
        }

        [Test]
        public void ShouldBeAbleToSetCurrent()
        {
            path.SetCurrent(2);
            Assert.That(path.ToString(), Is.EqualTo("2"));
        }

        [Test]
        public void ShouldBeAbleToDecreaseDepth()
        {
            path.IncreaseDepth();
            path.DecreaseDepth();

            Assert.That(path.ToString(), Is.EqualTo("1"));
        }

        [Test]
        public void SetCurrentShouldSetCurrentIdentifier()
        {
            path.IncreaseDepth();
            path.SetCurrent(2);
            Assert.That(path.ToString(), Is.EqualTo("1.2"));
        }
    }
}