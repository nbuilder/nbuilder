using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class ReflectionUtilTests
    {
        private const decimal decimalArg = 2m;
        private const string stringArg = "arg1";

        readonly IReflectionUtil reflectionUtil = new ReflectionUtil();

        [Fact]
        public void RequiresConstructorArgsShouldReturnFalseIfTypeHasParameterlessConstructor()
        {
            reflectionUtil.RequiresConstructorArgs(typeof(MyClass)).ShouldBe(false);
        }

        [Fact]
        public void RequiresConstructorArgsShouldReturnTrueIfTypeDoesNotHaveParameterlessConstructor()
        {
            reflectionUtil.RequiresConstructorArgs(typeof(MyClassWithConstructor)).ShouldBe(true);
        }

        [Fact]
        public void RequiresConstructorArgsShouldWorkWithStructs()
        {
            reflectionUtil.RequiresConstructorArgs(typeof (MyStruct)).ShouldBe(false);
        }

        [Fact]
        public void ShouldBeAbleToCreateInstanceOfClass()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClass>();
            instance.ShouldBeOfType<MyClass>();
        }

        // nb: These tests don't run under silverlight at the moment, so this is actually unnecessary, but if I
        //     do ever make them run under silverlight, I don't want to get caught out by this.
        [Fact]
        public void ShouldBeAbleToCreateInstanceOfClassThatHasPrivateParameterlessConstructor()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClassWithPrivateParameterlessConstructor>();
            instance.ShouldBeOfType<MyClassWithPrivateParameterlessConstructor>();
        }

        [Fact]
        public void WillComplainIfYouAttemptToCreateInstanceOfClassThatOnlyHasAPrivateParameterizedConstructor()
        {
            Should.Throw<TypeCreationException>(() =>
            {
                var instance = reflectionUtil.CreateInstanceOf<MyClassWithPrivateParameterizedConstructor>();
            });
            //instance.ShouldBeOfType<MyClassWithPrivateParameterizedConstructor)));
        }

        [Fact]
        public void ShouldBeAbleToCreateInstanceOfClassWithConstructorArgs()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(stringArg, decimalArg);
            instance.ShouldBeOfType<MyClassWithConstructor>();
        }

        [Fact]
        public void ShouldBeAbleToCreateInstanceOfStruct()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyStruct>();
            instance.ShouldBeOfType<MyStruct>();
        }

        [Fact]
        public void ShouldCorrectlySetPropertiesThroughConstructor()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(stringArg, decimalArg);
            
            instance.String.ShouldBe(stringArg);
            instance.Decimal.ShouldBe(decimalArg);
        }

        [Fact]
        public void ShouldComplainIfConstructorArgsDoNotMatchSignatureOfAnyConstructor()
        {
            Should.Throw<TypeCreationException>(() =>
            {
                reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(1m);
            });
        }

        [Fact]
        public void ShouldBeAbleToTellThatReferenceTypeIsDefaultValue()
        {
            const MyClass myObj = null;
            reflectionUtil.IsDefaultValue(myObj).ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeAbleToTellThatReferenceTypeIsNotDefaultValue()
        {
            MyClass myObj = new MyClass();
            reflectionUtil.IsDefaultValue(myObj).ShouldBeFalse();
        }

        [Fact]
        public void ShouldBeAbleToTellThatValueTypeIsDefaultValue()
        {
            int i = 0;
            reflectionUtil.IsDefaultValue(i).ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeAbleToTellThatValueTypeIsNotDefaultValue()
        {
            int i = 1;
            reflectionUtil.IsDefaultValue(i).ShouldBeFalse();
        }
    }
}