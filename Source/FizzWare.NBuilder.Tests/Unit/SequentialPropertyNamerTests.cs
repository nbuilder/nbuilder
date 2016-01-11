using System;
using System.Collections.Generic;
using System.Reflection;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class SequentialPropertyNamerTests
    {
        private SequentialPropertyNamer propertyNamer;
        private IReflectionUtil reflectionUtil;

        [SetUp]
        public void SetUp()
        {
            reflectionUtil = MockRepository.GenerateStub<IReflectionUtil>();
            propertyNamer = new SequentialPropertyNamer(reflectionUtil, new BuilderSetup());
        }

        [Test]
        public void SetValuesOfAllIn_ListOfTypeWithPrivateSetOnlyProperty_ValueIsNotSet()
        {
            var privateSetOnlyType = new MyClassWithGetOnlyPropertySpy();   
                        
            propertyNamer.SetValuesOfAllIn(new List<MyClassWithGetOnlyPropertySpy>{ privateSetOnlyType });

            Assert.That(privateSetOnlyType.IsSet, Is.False);
        }
    }
}