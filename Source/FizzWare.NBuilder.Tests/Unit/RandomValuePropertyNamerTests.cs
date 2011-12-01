using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomValuePropertyNamerTests
    {
        private RandomValuePropertyNamer propertyNamer;
        private IReflectionUtil reflectionUtil;
        private IRandomGenerator generator;

        [SetUp]
        public void SetUp()
        {
            reflectionUtil = Substitute.For<IReflectionUtil>();
            generator = Substitute.For<IRandomGenerator>();
            propertyNamer = new RandomValuePropertyNamer(generator, reflectionUtil, false);
        }

        [Test]
        public void SetValuesOfAllIn_ListOfTypeWithPrivateSetOnlyProperty_ValueIsNotSet()
        {
            var privateSetOnlyType = new MyClassWithGetOnlyPropertySpy();

            propertyNamer.SetValuesOfAllIn(new List<MyClassWithGetOnlyPropertySpy> { privateSetOnlyType });

            Assert.That(privateSetOnlyType.IsSet, Is.False);
        }
    }
}
