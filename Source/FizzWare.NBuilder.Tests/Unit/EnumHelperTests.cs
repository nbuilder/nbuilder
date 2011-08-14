using System;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class EnumHelperTests
    {
        [Test]
        public void GetValuesOfT_Enum_ReturnAllValues()
        {
            // Act
            MyEnum[] results = EnumHelper.GetValues<MyEnum>();

            // Assert
            Assert.That(results[0], Is.EqualTo(MyEnum.EnumValue1));
            Assert.That(results[1], Is.EqualTo(MyEnum.EnumValue2));
            Assert.That(results[2], Is.EqualTo(MyEnum.EnumValue3));
            Assert.That(results[3], Is.EqualTo(MyEnum.EnumValue4));
            Assert.That(results[4], Is.EqualTo(MyEnum.EnumValue5));
        }

        [Test]
        public void GetValuesOfT_NotAnEnumType_Throws()
        {
            // Act, Assert
            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<ArgumentException>(() => EnumHelper.GetValues<MyClass>());
            #endif

        }

        [Test]
        public void GetValues_NotAnEnumType_Throws()
        {
            // Act, Assert
            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<ArgumentException>(() => EnumHelper.GetValues(typeof(MyClass)));
            #endif
        }
    }
}