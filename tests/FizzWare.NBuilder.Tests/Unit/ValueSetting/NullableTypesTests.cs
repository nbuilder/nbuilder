using FizzWare.NBuilder.Tests.TestClasses;
using Shouldly;
using System;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit.ValueSetting;


public class NullableTypesTests
{
    readonly BuilderSettings settings;
    readonly Builder builder;

    public NullableTypesTests()
    {
        this.settings = new BuilderSettings();
        this.builder = new Builder(this.settings);
    }

    [Fact]
    public void WhenAllNullableTypesShouldBeNullByDefault()
    {
        this.settings.UseNullAsDefaultValueForAllNullableTypes();
        var value = builder.CreateNew<MyClass>().Build();
        value.NullableInt.HasValue.ShouldBeFalse();
        value.NullableGuid.HasValue.ShouldBeFalse();
    }

    [Fact]
    public void ValueIsDefaultForType_Int()
    {
        var value = builder.CreateNew<MyClass>().Build();
        value.NullableInt.HasValue.ShouldBeTrue();
        value.NullableInt.ShouldBe(1);
    }

    [Fact]
    public void UseNullAsDefaultValueForNullableType_Int_ValueIsNull()
    {
        this.settings.UseNullAsDefaultValueForNullableType(typeof(int?));
        var value = builder.CreateNew<MyClass>().Build();
        value.NullableInt.HasValue.ShouldBeFalse();
    }


    [Fact]
    public void ValueIsDefaultForType_Guid()
    {
        var value = builder.CreateNew<MyClass>().Build();
        value.NullableGuid.HasValue.ShouldBeTrue();
        value.NullableGuid.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public void UseNullAsDefaultValueForNullableType_Guid_ValueIsNull()
    {
        this.settings.UseNullAsDefaultValueForNullableType(typeof(Guid?));
        var value = builder.CreateNew<MyClass>().Build();
        value.NullableGuid.HasValue.ShouldBeFalse();
    }

}
