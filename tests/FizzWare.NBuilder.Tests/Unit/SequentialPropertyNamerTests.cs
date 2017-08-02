using System;
using System.Collections.Generic;
using System.Reflection;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;

using NSubstitute;
using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class SequentialPropertyNamerTests
    {
        private SequentialPropertyNamer propertyNamer;
        private IReflectionUtil reflectionUtil;

        public SequentialPropertyNamerTests()
        {
            reflectionUtil = Substitute.For<IReflectionUtil>();
            propertyNamer = new SequentialPropertyNamer(reflectionUtil, new BuilderSettings());
        }

        [Fact]
        public void SetValuesOfAllIn_ListOfTypeWithPrivateSetOnlyProperty_ValueIsNotSet()
        {
            var privateSetOnlyType = new MyClassWithGetOnlyPropertySpy();

            propertyNamer.SetValuesOfAllIn(new List<MyClassWithGetOnlyPropertySpy>{ privateSetOnlyType });

            privateSetOnlyType.IsSet.ShouldBeFalse();
        }
    }
}