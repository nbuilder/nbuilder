using System;
using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class EnumHelperTests
    {
        [Fact]
        public void GetValuesOfT_Enum_ReturnAllValues()
        {
            // Act
            MyEnum[] results = EnumHelper.GetValues<MyEnum>();

            // Assert
            results[0].ShouldBe(MyEnum.EnumValue1);
            results[1].ShouldBe(MyEnum.EnumValue2);
            results[2].ShouldBe(MyEnum.EnumValue3);
            results[3].ShouldBe(MyEnum.EnumValue4);
            results[4].ShouldBe(MyEnum.EnumValue5);
        }

        [Fact]
        public void GetValuesOfT_NotAnEnumType_Throws()
        {
            // Act, Assert
            // TODO FIX
            #if !SILVERLIGHT
            Should.Throw<ArgumentException>(() => EnumHelper.GetValues<MyClass>());
            #endif

        }

        [Fact]
        public void GetValues_NotAnEnumType_Throws()
        {
            // Act, Assert
            // TODO FIX
            #if !SILVERLIGHT
            Should.Throw<ArgumentException>(() => EnumHelper.GetValues(typeof(MyClass)));
            #endif
        }
    }
}