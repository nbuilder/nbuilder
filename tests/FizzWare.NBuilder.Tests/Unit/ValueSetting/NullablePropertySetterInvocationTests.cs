using FizzWare.NBuilder.Tests.TestClasses;
using Shouldly;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit.ValueSetting;

/// <summary>
/// Tests to verify that SetValue is actually invoked when setting nullable properties to null.
/// This is important because the fix changes from "return early" to "call SetValue with allowNull: true",
/// which ensures setters with side effects are properly triggered.
/// </summary>
public class NullablePropertySetterInvocationTests
{
    readonly BuilderSettings settings;
    readonly Builder builder;

    public NullablePropertySetterInvocationTests()
    {
        this.settings = new BuilderSettings();
        this.builder = new Builder(this.settings);
    }

    [Fact]
    public void UseNullAsDefaultValueForAllNullableTypes_ShouldInvokeSetterWhenSettingToNull()
    {
        // Arrange
        this.settings.UseNullAsDefaultValueForAllNullableTypes();

        // Act
        var result = builder.CreateNew<ClassWithNullablePropertyWithSetterSideEffect>().Build();

        // Assert
        result.NullableInt.ShouldBeNull();
        result.NullableGuid.ShouldBeNull();
        
        // Verify setters were actually called (not just left at default)
        result.NullableIntSetterCallCount.ShouldBe(1, "NullableInt setter should be called once when setting to null");
        result.NullableGuidSetterCallCount.ShouldBe(1, "NullableGuid setter should be called once when setting to null");
    }

    [Fact]
    public void UseNullAsDefaultValueForNullableType_ShouldInvokeSetterForSpecifiedType()
    {
        // Arrange
        this.settings.UseNullAsDefaultValueForNullableType(typeof(int?));

        // Act
        var result = builder.CreateNew<ClassWithNullablePropertyWithSetterSideEffect>().Build();

        // Assert
        result.NullableInt.ShouldBeNull();
        result.NullableIntSetterCallCount.ShouldBe(1, "NullableInt setter should be called when setting to null");
        
        // Other nullable properties should still be set to non-null values
        result.NullableGuid.ShouldNotBeNull();
        result.NullableGuidSetterCallCount.ShouldBe(1, "NullableGuid setter should be called when setting to non-null value");
    }

    [Fact]
    public void DefaultBehavior_ShouldInvokeSetterWhenSettingToNonNullValue()
    {
        // Act - default behavior sets nullable properties to non-null values
        var result = builder.CreateNew<ClassWithNullablePropertyWithSetterSideEffect>().Build();

        // Assert
        result.NullableInt.ShouldNotBeNull();
        result.NullableGuid.ShouldNotBeNull();
        
        // Verify setters were called
        result.NullableIntSetterCallCount.ShouldBe(1, "NullableInt setter should be called when setting to non-null value");
        result.NullableGuidSetterCallCount.ShouldBe(1, "NullableGuid setter should be called when setting to non-null value");
    }

    [Fact]
    public void UseNullAsDefaultValueForAllNullableTypes_WithListBuilder_ShouldInvokeSetterForAllItems()
    {
        // Arrange
        this.settings.UseNullAsDefaultValueForAllNullableTypes();

        // Act
        var results = builder.CreateListOfSize<ClassWithNullablePropertyWithSetterSideEffect>(3).Build();

        // Assert
        foreach (var result in results)
        {
            result.NullableInt.ShouldBeNull();
            result.NullableGuid.ShouldBeNull();
            result.NullableIntSetterCallCount.ShouldBe(1, "Setter should be called once per item");
            result.NullableGuidSetterCallCount.ShouldBe(1, "Setter should be called once per item");
        }
    }
}
