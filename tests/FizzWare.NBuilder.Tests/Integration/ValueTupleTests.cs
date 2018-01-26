using System;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    public class ValueTupleTests
    {
        [Fact]
        public void Build_ValueTuple_DoesNotReturnNullValues()
        {
            var target = new Builder().CreateNew<(MyClass Class1, MyClass Class2)>();
            var actual = target.Build();
            actual.Class1.ShouldNotBeNull();
            actual.Class2.ShouldNotBeNull();
        }
        
        [Fact]
        public void Build_ValueTuple_WithStatementsWorkOnArgumentLevelDoesNotReplace()
        {
            var expected = new MyClass {Guid = Guid.NewGuid()};
            var target = new Builder().CreateNew<(MyClass Class1, MyClass Class2)>()
                .With(x=>x.Class1 = expected );
            var actual = target.Build();
            actual.Class1.Guid.ShouldNotBe(expected.Guid);
        }

        [Fact]
        public void _Build_ValueTuple_WithStatementsWorkOnNestedClassLevel()
        {
            var expected = Guid.NewGuid();
            var target = new Builder().CreateNew<(MyClass Class1, MyClass Class2)>()
                .With(x=>x.Class1.Guid = expected );
            var actual = target.Build();
            actual.Class1.Guid.ShouldBe(expected);
        }

        [Fact]
        public void _Build_ValueTupleUsingRandomValuePropertyNamer_ReturnsRandomValues()
        {
            
            var builderSettings = new BuilderSettings();
            builderSettings.SetDefaultPropertyNamer(new RandomValuePropertyNamer(builderSettings));
            var target = new Builder(builderSettings).CreateNew<(MyClass Class1, MyClass Class2)>();
            var actual = target.Build();
            actual.Class1.Guid.ShouldNotBe(Guid.Empty);
            actual.Class1.StringOne.ShouldNotBe(string.Empty);
        }
    }
}