using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    /// <summary>
    /// Tests for nullable property handling in PropertyNamer
    /// Testing the logic in GetMemberType() and SetMemberValue()
    /// </summary>
    public class PropertyNamerNullableTests
    {
        private BuilderSettings builderSettings;
        private IReflectionUtil reflectionUtil;

        public PropertyNamerNullableTests()
        {
            builderSettings = new BuilderSettings();
            reflectionUtil = new ReflectionUtil();
        }

        [Fact]
        public void SetValuesOf_NullableInt_ShouldBeSetToNonNullValueByDefault()
        {
            // Arrange
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.NullableInt.ShouldNotBeNull();
            myClass.NullableInt.ShouldBe(1);
        }

        [Fact]
        public void SetValuesOf_NullableGuid_ShouldBeSetToNonNullValueByDefault()
        {
            // Arrange
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.NullableGuid.ShouldNotBeNull();
        }

        [Fact]
        public void SetValuesOf_WithUseNullAsDefaultValueForAllNullableTypes_ShouldSetNullablePropertiesToNull()
        {
            // Arrange
            builderSettings.UseNullAsDefaultValueForAllNullableTypes();
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.NullableInt.ShouldBeNull();
            myClass.NullableGuid.ShouldBeNull();
        }

        [Fact]
        public void SetValuesOf_WithUseNullForSpecificNullableType_ShouldSetOnlyThatTypeToNull()
        {
            // Arrange
            builderSettings.UseNullAsDefaultValueForNullableType(typeof(int?));
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.NullableInt.ShouldBeNull();
            myClass.NullableGuid.ShouldNotBeNull(); // Should still be set
        }

        [Fact]
        public void SetValuesOfAllIn_WithUseNullAsDefaultValueForAllNullableTypes_ShouldSetAllNullablePropertiesToNull()
        {
            // Arrange
            builderSettings.UseNullAsDefaultValueForAllNullableTypes();
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var list = new List<MyClass> { new MyClass(), new MyClass(), new MyClass() };

            // Act
            propertyNamer.SetValuesOfAllIn(list);

            // Assert
            foreach (var item in list)
            {
                item.NullableInt.ShouldBeNull();
                item.NullableGuid.ShouldBeNull();
            }
        }

        [Fact]
        public void SetValuesOf_NonNullableProperties_ShouldNotBeAffectedByNullableSettings()
        {
            // Arrange
            builderSettings.UseNullAsDefaultValueForAllNullableTypes();
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.Int.ShouldBe(1); // Non-nullable int should still be set
            myClass.Guid.ShouldNotBe(Guid.Empty); // Non-nullable Guid should still be set
        }

        [Fact]
        public void SetValuesOf_NullableEnum_ShouldBeSetToNonNullValueByDefault()
        {
            // Arrange
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.EnumProperty.ShouldNotBeNull();
        }

        [Fact]
        public void SetValuesOf_NullableEnumWithUseNullForAll_ShouldBeSetToNull()
        {
            // Arrange
            builderSettings.UseNullAsDefaultValueForAllNullableTypes();
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.EnumProperty.ShouldBeNull();
        }

        [Fact]
        public void SetValuesOf_WithPresetNullableValue_ShouldNotOverwrite()
        {
            // Arrange
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass { NullableInt = 999 };

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.NullableInt.ShouldBe(999); // Should not be overwritten
        }

        [Fact]
        public void SetValuesOf_WithUseNullForSpecificType_OnlyAffectsThatType()
        {
            // Arrange
            builderSettings.UseNullAsDefaultValueForNullableType(typeof(Guid?));
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil, builderSettings);
            var myClass = new MyClass();

            // Act
            propertyNamer.SetValuesOf(myClass);

            // Assert
            myClass.NullableGuid.ShouldBeNull();
            myClass.NullableInt.ShouldNotBeNull();
            myClass.EnumProperty.ShouldNotBeNull();
        }

        [Fact]
        public void GetMemberType_WithNullableType_AndMaintainNullabilityFalse_ShouldReturnUnderlyingType()
        {
            // Arrange
            var propertyInfo = typeof(MyClass).GetProperty(nameof(MyClass.NullableInt));

            // Act
            var type = PropertyNamerTestHelper.GetMemberTypePublic(propertyInfo, maintainNullability: false);

            // Assert
            type.ShouldBe(typeof(int));
        }

        [Fact]
        public void GetMemberType_WithNullableType_AndMaintainNullabilityTrue_ShouldReturnNullableType()
        {
            // Arrange
            var propertyInfo = typeof(MyClass).GetProperty(nameof(MyClass.NullableInt));

            // Act
            var type = PropertyNamerTestHelper.GetMemberTypePublic(propertyInfo, maintainNullability: true);

            // Assert
            type.ShouldBe(typeof(int?));
        }

        [Fact]
        public void GetMemberType_WithNonNullableType_ShouldReturnSameTypeRegardlessOfMaintainNullability()
        {
            // Arrange
            var propertyInfo = typeof(MyClass).GetProperty(nameof(MyClass.Int));

            // Act
            var typeWithMaintain = PropertyNamerTestHelper.GetMemberTypePublic(propertyInfo, maintainNullability: true);
            var typeWithoutMaintain = PropertyNamerTestHelper.GetMemberTypePublic(propertyInfo, maintainNullability: false);

            // Assert
            typeWithMaintain.ShouldBe(typeof(int));
            typeWithoutMaintain.ShouldBe(typeof(int));
        }
    }

    /// <summary>
    /// Helper class to expose protected methods for testing
    /// </summary>
    internal static class PropertyNamerTestHelper
    {
        public static Type GetMemberTypePublic(System.Reflection.MemberInfo memberInfo, bool maintainNullability = false)
        {
            // Use reflection to call the protected static GetMemberType method
            var method = typeof(PropertyNamer).GetMethod("GetMemberType", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            return (Type)method.Invoke(null, new object[] { memberInfo, maintainNullability });
        }
    }
}
