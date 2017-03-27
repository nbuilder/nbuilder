using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class PropertyNamerTests
    {
        PropertyNamerStub propertyNamer;

        [SetUp]
        public void SetUp()
        {
            BuilderSettings builderSettings = new BuilderSettings();
            IReflectionUtil reflectionUtil = Substitute.For<IReflectionUtil>();
            reflectionUtil.IsDefaultValue(Arg.Any<int?>()).Returns(true);

            propertyNamer = new PropertyNamerStub(reflectionUtil, builderSettings);
        }

        [Test]
        public void SetValuesOf_GivenObjectWithNullableProperty_SetsTheValueOfTheProperty()
        {
            MyClass mc = new MyClass { NullableInt = null };

            propertyNamer.SetValuesOf(mc);

            Assert.That(mc.NullableInt.HasValue, Is.True);
        }

        [Test]
        public void SetValuesOf_ClassWithNullCharConst_CharConstantIsNotSetByNamer()
        {
            MyClassWithCharConst mc = new MyClassWithCharConst();

            propertyNamer.SetValuesOf(mc);

            Assert.That(mc.GetNullCharConst(), Is.EqualTo(MyClassWithCharConst.NullCharConst));
            Assert.That(mc.GetNonNullCharConst(), Is.EqualTo(MyClassWithCharConst.NonNullCharConst));

            Assert.Pass("A System.FieldAccessException was not thrown because NBuilder didn't try to set the value of the constant");
        }

        [Test]
        public void SetValuesOf_GetOnlyProperty_PropertyIsNotSet()
        {
            var myClass = new MyClassWithGetOnlyPropertySpy();
            propertyNamer.SetValuesOf(myClass);

            Assert.That(myClass.IsSet, Is.False);
        }

        private class PropertyNamerStub : PropertyNamer
        {
            public PropertyNamerStub(IReflectionUtil reflectionUtil,BuilderSettings builderSettings) : base(reflectionUtil, builderSettings) { }

            public override void SetValuesOfAllIn<T>(IList<T> objects)
            {
            }

            protected override short GetInt16(System.Reflection.MemberInfo memberInfo)
            {
                return default(short);
            }

            protected override int GetInt32(System.Reflection.MemberInfo memberInfo)
            {
                return default(int);
            }

            protected override long GetInt64(System.Reflection.MemberInfo memberInfo)
            {
                return default(long);
            }

            protected override decimal GetDecimal(System.Reflection.MemberInfo memberInfo)
            {
                return default(decimal);
            }

            protected override float GetSingle(System.Reflection.MemberInfo memberInfo)
            {
                return default(float);
            }

            protected override double GetDouble(System.Reflection.MemberInfo memberInfo)
            {
                return default(double);
            }

            protected override ushort GetUInt16(System.Reflection.MemberInfo memberInfo)
            {
                return default(ushort);
            }

            protected override uint GetUInt32(System.Reflection.MemberInfo memberInfo)
            {
                return default(uint);
            }

            protected override ulong GetUInt64(System.Reflection.MemberInfo memberInfo)
            {
                return default(ulong);
            }

            protected override sbyte GetSByte(System.Reflection.MemberInfo memberInfo)
            {
                return default(sbyte);
            }

            protected override byte GetByte(System.Reflection.MemberInfo memberInfo)
            {
                return default(byte);
            }

            protected override DateTime GetDateTime(System.Reflection.MemberInfo memberInfo)
            {
                return default(DateTime);
            }

            protected override string GetString(System.Reflection.MemberInfo memberInfo)
            {
                return default(string);
            }

            protected override bool GetBoolean(System.Reflection.MemberInfo memberInfo)
            {
                return default(bool);
            }

            protected override char GetChar(System.Reflection.MemberInfo memberInfo)
            {
                return default(char);
            }

            protected override Enum GetEnum(System.Reflection.MemberInfo memberInfo)
            {
                return default(Enum);
            }

            protected override Guid GetGuid(System.Reflection.MemberInfo memberInfo)
            {
                return default(Guid);
            }

            protected override TimeSpan GetTimeSpan(System.Reflection.MemberInfo memberInfo)
            {
                return default(TimeSpan);
            }
        }
    }


}
