using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ReflectionUtilTests
    {
        private const decimal decimalArg = 2m;
        private const string stringArg = "arg1";

        readonly IReflectionUtil reflectionUtil = new ReflectionUtil();

        [Test]
        public void RequiresConstructorArgsShouldReturnFalseIfTypeHasParameterlessConstructor()
        {
            Assert.That(reflectionUtil.RequiresConstructorArgs(typeof (MyClass)), Is.EqualTo(false));
        }

        [Test]
        public void RequiresConstructorArgsShouldReturnTrueIfTypeDoesNotHaveParameterlessConstructor()
        {
            Assert.That(reflectionUtil.RequiresConstructorArgs(typeof(MyClassWithConstructor)), Is.EqualTo(true));
        }

        [Test]
        public void RequiresConstructorArgsShouldWorkWithStructs()
        {
            Assert.That(reflectionUtil.RequiresConstructorArgs(typeof (MyStruct)), Is.False);
        }

        [Test]
        public void ShouldBeAbleToCreateInstanceOfClass()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClass>();
            Assert.That(instance, Is.TypeOf(typeof(MyClass)));
        }

        [Test]
        public void ShouldBeAbleToCreateInstanceOfClassThatHasPrivateParameterlessConstructor()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClassWithPrivateParameterlessConstructor>();
            Assert.That(instance, Is.TypeOf(typeof(MyClassWithPrivateParameterlessConstructor)));
        }

        [Test]
        public void ShouldBeAbleToCreateInstanceOfClassWithConstructorArgs()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(stringArg, decimalArg);
            Assert.That(instance, Is.TypeOf(typeof(MyClassWithConstructor)));
        }

        [Test]
        public void ShouldBeAbleToCreateInstanceOfStruct()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyStruct>();
            Assert.That(instance, Is.TypeOf(typeof(MyStruct)));
        }

        [Test]
        public void ShouldCorrectlySetPropertiesThroughConstructor()
        {
            var instance = reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(stringArg, decimalArg);
            
            Assert.That(instance.String, Is.EqualTo(stringArg));
            Assert.That(instance.Decimal, Is.EqualTo(decimalArg));
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void ShouldComplainIfConstructorArgsDoNotMatchSignatureOfAnyConstructor()
        {
            reflectionUtil.CreateInstanceOf<MyClassWithConstructor>(1m);
        }

        [Test]
        public void ShouldBeAbleToTellThatReferenceTypeIsDefaultValue()
        {
            const MyClass myObj = null;
            Assert.IsTrue(reflectionUtil.IsDefaultValue(myObj));
        }

        [Test]
        public void ShouldBeAbleToTellThatReferenceTypeIsNotDefaultValue()
        {
            MyClass myObj = new MyClass();
            Assert.IsFalse(reflectionUtil.IsDefaultValue(myObj));
        }

        [Test]
        public void ShouldBeAbleToTellThatValueTypeIsDefaultValue()
        {
            int i = 0;
            Assert.IsTrue(reflectionUtil.IsDefaultValue(i));
        }

        [Test]
        public void ShouldBeAbleToTellThatValueTypeIsNotDefaultValue()
        {
            int i = 1;
            Assert.IsFalse(reflectionUtil.IsDefaultValue(i));
        }
    }
}