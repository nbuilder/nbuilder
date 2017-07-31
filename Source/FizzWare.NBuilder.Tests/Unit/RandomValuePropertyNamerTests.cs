using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class RandomValuePropertyNamerTests
    {
        private RandomValuePropertyNamer propertyNamer;
        private IReflectionUtil reflectionUtil;
        private IRandomGenerator generator;

        public RandomValuePropertyNamerTests()
        {
            var builderSetup = new BuilderSettings();
            reflectionUtil = Substitute.For<IReflectionUtil>();
            generator = Substitute.For<IRandomGenerator>();
            propertyNamer = new RandomValuePropertyNamer(generator, reflectionUtil, false,builderSetup);
        }

        [Fact]
        public void SetValuesOfAllIn_ListOfTypeWithPrivateSetOnlyProperty_ValueIsNotSet()
        {
            var privateSetOnlyType = new MyClassWithGetOnlyPropertySpy();

            propertyNamer.SetValuesOfAllIn(new List<MyClassWithGetOnlyPropertySpy> { privateSetOnlyType });

            privateSetOnlyType.IsSet.ShouldBeFalse();
        }
    }
}
