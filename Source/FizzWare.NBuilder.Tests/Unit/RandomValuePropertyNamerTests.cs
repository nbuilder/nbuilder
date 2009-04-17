using System;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using NUnit.Framework;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomValuePropertyNamerTests
    {
        private IPropertyNamer propertyNamer = new RandomValuePropertyNamer(new ReflectionUtil(), false);

        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void ShouldNameProperties()
        {
            
        }
    }
}