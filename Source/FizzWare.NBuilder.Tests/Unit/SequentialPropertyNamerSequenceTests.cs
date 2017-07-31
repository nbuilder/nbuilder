using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using FizzWare.NBuilder.Tests.TestClasses;
using FizzWare.NBuilder.Implementation;
using NSubstitute;
using FizzWare.NBuilder.PropertyNaming;
using System.Reflection;
using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class SequentialPropertyNamerSequenceTests
    {
        private IList<MyClass> theList;
        private const int listSize = 1000;
        private IReflectionUtil reflectionUtil;

        public SequentialPropertyNamerSequenceTests()
        {
            reflectionUtil = new ReflectionUtil();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            new SequentialPropertyNamer(reflectionUtil,new BuilderSettings())
                .SetValuesOfAllIn(theList);
        }

        [Fact]
        public void ShouldNameStringPropertiesWithTheirNameAndSequenceNumber()
        {
            theList[0].StringOne.ShouldBe("StringOne" + 1);
            theList[9].StringOne.ShouldBe("StringOne" + 10);
        }

        [Fact]
        public void ShouldHandleMultiplePropertiesOfTheSameType()
        {
            theList[9].StringOne.ShouldBe("StringOne" + 10);
            theList[9].StringTwo.ShouldBe("StringTwo" + 10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToBytes()
        {
            theList[0].Byte.ShouldBe((byte)1);
            theList[9].Byte.ShouldBe((byte)10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToSBytes()
        {
            theList[0].SByte.ShouldBe((sbyte)1);
            theList[9].SByte.ShouldBe((sbyte)10);
        }

        [Fact]
        [Description("The desired behaviour is byte properties should be given the values 1...255, 1...255, 1...255 repeatedly")]
        public void ShouldRestartFromZeroToBytesWhenGoesAboveMaxValue()
        {
            theList[0].Byte.ShouldBe((byte)1);
            theList[byte.MaxValue].Byte.ShouldBe((byte)1);
            theList[byte.MaxValue * 2].Byte.ShouldBe((byte)1);
            theList[byte.MaxValue * 3].Byte.ShouldBe((byte)1);
        }

        [Fact]
        [Description("The desired behaviour is byte properties should be given the values 1...255, 1...255, 1...255 repeatedly")]
        public void ShouldRestartFromZeroToSBytesWhenGoesAboveMaxValue()
        {
            theList[0].SByte.ShouldBe((sbyte)1);
            theList[sbyte.MaxValue].SByte.ShouldBe((sbyte)1);
            theList[sbyte.MaxValue * 2].SByte.ShouldBe((sbyte)1);
            theList[sbyte.MaxValue * 3].SByte.ShouldBe((sbyte)1);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToInts()
        {
            theList[0].Int.ShouldBe(1);
            theList[9].Int.ShouldBe(10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToLongs()
        {
            theList[0].Long.ShouldBe(1);
            theList[9].Long.ShouldBe(10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToDecimals()
        {
            theList[0].Decimal.ShouldBe(1m);
            theList[9].Decimal.ShouldBe(10m);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToFloats()
        {
            theList[0].Float.ShouldBe(1f);
            theList[9].Float.ShouldBe(10f);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToDoubles()
        {
            theList[0].Double.ShouldBe(1d);
            theList[9].Double.ShouldBe(10d);
        }

        [Fact]
        public void ShouldAssignUppercaseLetterBetweenAandZToChars()
        {
            theList[0].Char.ShouldBe('A');
            theList[9].Char.ShouldBe('J');
            theList[25].Char.ShouldBe('Z');
        }

        [Fact]
        public void ShouldRestartAtAWhenOn26ForChars()
        {
            theList[0].Char.ShouldBe('A');
            theList[26].Char.ShouldBe('A');
            theList[52].Char.ShouldBe('A');
        }

        [Fact]
        public void ShouldAssignSequenceNumberToUShorts()
        {
            theList[0].Ushort.ShouldBe((ushort)1);
            theList[9].Ushort.ShouldBe((ushort)10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToUInts()
        {
            theList[0].Uint.ShouldBe((uint)1);
            theList[9].Uint.ShouldBe((uint)10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToULongs()
        {
            theList[0].Ulong.ShouldBe((ulong)1);
            theList[9].Ulong.ShouldBe((ulong)10);
        }

        [Fact]
        public void ShouldAssignSequenceNumberToGuids()
        {
            theList[0].Guid.ToString().ShouldBe("00000000-0000-0000-0000-000000000001");
            theList[9].Guid.ToString().ShouldBe("00000000-0000-0000-0000-00000000000a");
            theList[254].Guid.ToString().ShouldBe("00000000-0000-0000-0000-0000000000ff");
        }

        [Fact]
        public void ShouldAlternateBooleanValues()
        {
            theList[0].Bool.ShouldBeFalse();
            theList[1].Bool.ShouldBeTrue();
            theList[2].Bool.ShouldBeFalse();
            theList[3].Bool.ShouldBeTrue();
        }

        [Fact]
        public void ShouldAssignCurrentDateThenCurrentDatePlus_N_DaysToDateTimeProperties()
        {
            theList[0].DateTime.ShouldBe(DateTime.Now.Date);
            theList[9].DateTime.ShouldBe(DateTime.Now.Date.AddDays(9));
        }

        [Fact]
        public void ShouldOnlyNamePropertiesThatAreNullOrDefault()
        {
            var myClass = new MyClass();
            var reflectionUtil = Substitute.For<IReflectionUtil>();

            reflectionUtil.IsDefaultValue(myClass.HasADefaultValue).Returns(false);

            reflectionUtil.IsDefaultValue(Arg.Is<object>(o => o != myClass.HasADefaultValue)).Returns(true);

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            new SequentialPropertyNamer(reflectionUtil,new BuilderSettings()).SetValuesOfAllIn(theList);

            theList[0].HasADefaultValue.ShouldBe(myClass.HasADefaultValue);
            theList[9].HasADefaultValue.ShouldBe(myClass.HasADefaultValue);

        }

        [Fact]
        public void ShouldNotTouchInternalProperties()
        {
            theList[0].InternalInt.ShouldBe(0);
            theList[9].InternalInt.ShouldBe(0);
        }

        [Fact]
        public void ShouldNotTouchPrivateProperties()
        {
            var prop0 = typeof(MyClass).GetProperty("PrivateInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0], null);
            var prop9 = typeof(MyClass).GetProperty("PrivateInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9], null);

            prop0.ShouldBe(0);
            prop9.ShouldBe(0);
        }

        [Fact]
        public void ShouldNotTouchProtectedIntProperties()
        {
            var prop0 = typeof(MyClass).GetProperty("ProtectedInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0], null);
            var prop9 = typeof(MyClass).GetProperty("ProtectedInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9], null);

            prop0.ShouldBe(0);
            prop9.ShouldBe(0);
        }

        [Fact]
        public void SupportsInheritedClasses()
        {
            var myClassInheritor = new MyClassInheritor();
            new SequentialPropertyNamer(reflectionUtil,new BuilderSettings()).SetValuesOf(myClassInheritor);

            myClassInheritor.Int.ShouldBe(1);
            myClassInheritor.AnotherProperty.ShouldBe(1);
        }

        [Fact]
        public void DoesNotNameStaticProperties()
        {
            MyClass.StaticInt.ShouldBe(0);
        }

        [Fact]
        public void ShouldNamePublicFields()
        {
            theList[0].PublicFieldInt.ShouldBe(1);
            theList[9].PublicFieldInt.ShouldBe(10);
        }

        [Fact]
        public void ShouldNotTouchPrivateFields()
        {
            var prop0 = typeof(MyClass).GetField("PrivateFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0]);
            var prop9 = typeof(MyClass).GetField("PrivateFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9]);

            prop0.ShouldBe(0);
            prop9.ShouldBe(0);
        }

        [Fact]
        public void ShouldNotTouchProtectedFields()
        {
            var prop0 = typeof(MyClass).GetField("ProtectedFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0]);
            var prop9 = typeof(MyClass).GetField("ProtectedFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9]);

            prop0.ShouldBe(0);
            prop9.ShouldBe(0);
        }

        [Fact]
        public void ShouldNotTouchInternalFields()
        {
            var prop0 = typeof(MyClass).GetField("InternalFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0]);
            var prop9 = typeof(MyClass).GetField("InternalFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9]);

            prop0.ShouldBe(0);
            prop9.ShouldBe(0);
        }

        [Fact]
        public void SetValuesOfAllIn_ClassWithNullCharConst_CharConstantIsNotSetByNamer()
        {
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil,new BuilderSettings());

            List<MyClassWithCharConst> list = new List<MyClassWithCharConst>() { new MyClassWithCharConst() };

            propertyNamer.SetValuesOfAllIn(list);

            foreach (var item in list)
            {
                item.GetNullCharConst().ShouldBe(MyClassWithCharConst.NullCharConst);
                item.GetNonNullCharConst().ShouldBe(MyClassWithCharConst.NonNullCharConst);
            }

        }

        // TODO: Add this
        //[Fact]
        //public void ShouldBeAbleToAddCustomHandler()
        //{
        //    SequentialPropertyNamer.AddHandlerFor(
        //        (memberInfo, sequenceNumber) =>
        //            {
        //                return new SimpleClass();
        //            });
        //}
    }
}
