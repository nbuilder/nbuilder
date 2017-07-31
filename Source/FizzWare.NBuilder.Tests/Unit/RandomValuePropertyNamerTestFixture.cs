using System;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using System.Collections.Generic;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    public abstract class RandomValuePropertyNamerTestFixture
    {
        protected IRandomGenerator generator;
        protected IList<MyClass> theList;
        protected const int listSize = 10;
        protected IReflectionUtil reflectionUtil;

        public RandomValuePropertyNamerTestFixture()
        {
            this.TestFixtureSetUp();
        }

        public abstract void TestFixtureSetUp();


        [Fact]
        public void ShouldNameInt16Properties()
        {
            theList[0].Short.ShouldBe((short)1);
        }

        [Fact]
        public void ShouldNameInt32Properties()
        {
            theList[0].Int.ShouldBe(1);
        }

        [Fact]
        public void ShouldNameInt64Properties()
        {
            theList[0].Long.ShouldBe(1);
        }

        [Fact]
        public void ShouldNameUInt16Properties()
        {
            theList[0].Ushort.ShouldBe((ushort)1);
        }

        [Fact]
        public void ShouldNameUInt32Properties()
        {
            theList[0].Uint.ShouldBe((uint)1);
        }

        [Fact]
        public void ShouldNameUInt64Properties()
        {
            theList[0].Ulong.ShouldBe((ulong)1);
        }

        [Fact]
        public void ShouldNameSingleProperties()
        {
            theList[0].Float.ShouldBe(1);
        }

        [Fact]
        public void ShouldNameDoubleProperties()
        {
            theList[0].Double.ShouldBe(1);
        }

        [Fact]
        public void ShouldNameDecimalProperties()
        {
            theList[0].Decimal.ShouldBe(1);
        }

        [Fact]
        public void ShouldNameByteProperties()
        {
            theList[0].Byte.ShouldBe((byte)1);
        }

        [Fact]
        public void ShouldNameCharProperties()
        {
            theList[0].Char.ShouldBe('A');
        }

        [Fact]
        public void ShouldNameDateTimeProperties()
        {
            theList[0].DateTime.ShouldBe(DateTime.Today);
        }

        [Fact]
        public void ShouldNameBooleanProperties()
        {
            theList[0].Bool.ShouldBe(true);
        }

        [Fact]
        public void ShouldNameStringProperties()
        {
            theList[0].StringOne.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldNameEnumProperties()
        {
            theList[0].EnumProperty.ShouldNotBeNull();
        }
    }
}