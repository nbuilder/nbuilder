using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder.Tests.TestClasses;
using FizzWare.NBuilder.Implementation;
using NSubstitute;
using FizzWare.NBuilder.PropertyNaming;
using System.Reflection;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class SequentialPropertyNamerSequenceTests
    {
        private IList<MyClass> theList;
        private const int listSize = 1000;
        private IReflectionUtil reflectionUtil;

        [SetUp]
        public void SetUp()
        {
            reflectionUtil = new ReflectionUtil();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            new SequentialPropertyNamer(reflectionUtil,new BuilderSettings())
                .SetValuesOfAllIn(theList);
        }

        [Test]
        public void ShouldNameStringPropertiesWithTheirNameAndSequenceNumber()
        {
            Assert.That(theList[0].StringOne, Is.EqualTo("StringOne" + 1));
            Assert.That(theList[9].StringOne, Is.EqualTo("StringOne" + 10));
        }

        [Test]
        public void ShouldHandleMultiplePropertiesOfTheSameType()
        {
            Assert.That(theList[9].StringOne, Is.EqualTo("StringOne" + 10));
            Assert.That(theList[9].StringTwo, Is.EqualTo("StringTwo" + 10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToBytes()
        {
            Assert.That(theList[0].Byte, Is.EqualTo(1));
            Assert.That(theList[9].Byte, Is.EqualTo(10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToSBytes()
        {
            Assert.That(theList[0].SByte, Is.EqualTo(1));
            Assert.That(theList[9].SByte, Is.EqualTo(10));
        }

        [Test]
        [Description("The desired behaviour is byte properties should be given the values 1...255, 1...255, 1...255 repeatedly")]
        public void ShouldRestartFromZeroToBytesWhenGoesAboveMaxValue()
        {
            Assert.That(theList[0].Byte, Is.EqualTo(1));
            Assert.That(theList[byte.MaxValue].Byte, Is.EqualTo(1));
            Assert.That(theList[byte.MaxValue * 2].Byte, Is.EqualTo(1));
            Assert.That(theList[byte.MaxValue * 3].Byte, Is.EqualTo(1));
        }

        [Test]
        [Description("The desired behaviour is byte properties should be given the values 1...255, 1...255, 1...255 repeatedly")]
        public void ShouldRestartFromZeroToSBytesWhenGoesAboveMaxValue()
        {
            Assert.That(theList[0].SByte, Is.EqualTo(1));
            Assert.That(theList[sbyte.MaxValue].SByte, Is.EqualTo(1));
            Assert.That(theList[sbyte.MaxValue * 2].SByte, Is.EqualTo(1));
            Assert.That(theList[sbyte.MaxValue * 3].SByte, Is.EqualTo(1));
        }

        [Test]
        public void ShouldAssignSequenceNumberToInts()
        {
            Assert.That(theList[0].Int, Is.EqualTo(1));
            Assert.That(theList[9].Int, Is.EqualTo(10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToLongs()
        {
            Assert.That(theList[0].Long, Is.EqualTo(1));
            Assert.That(theList[9].Long, Is.EqualTo(10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToDecimals()
        {
            Assert.That(theList[0].Decimal, Is.EqualTo(1m));
            Assert.That(theList[9].Decimal, Is.EqualTo(10m));
        }

        [Test]
        public void ShouldAssignSequenceNumberToFloats()
        {
            Assert.That(theList[0].Float, Is.EqualTo(1f));
            Assert.That(theList[9].Float, Is.EqualTo(10f));
        }

        [Test]
        public void ShouldAssignSequenceNumberToDoubles()
        {
            Assert.That(theList[0].Double, Is.EqualTo(1d));
            Assert.That(theList[9].Double, Is.EqualTo(10d));
        }

        [Test]
        public void ShouldAssignUppercaseLetterBetweenAandZToChars()
        {
            Assert.That(theList[0].Char, Is.EqualTo('A'));
            Assert.That(theList[9].Char, Is.EqualTo('J'));
            Assert.That(theList[25].Char, Is.EqualTo('Z'));
        }

        [Test]
        public void ShouldRestartAtAWhenOn26ForChars()
        {
            Assert.That(theList[0].Char, Is.EqualTo('A'));
            Assert.That(theList[26].Char, Is.EqualTo('A'));
            Assert.That(theList[52].Char, Is.EqualTo('A'));
        }

        [Test]
        public void ShouldAssignSequenceNumberToUShorts()
        {
            Assert.That(theList[0].Ushort, Is.EqualTo(1));
            Assert.That(theList[9].Ushort, Is.EqualTo(10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToUInts()
        {
            Assert.That(theList[0].Uint, Is.EqualTo(1));
            Assert.That(theList[9].Uint, Is.EqualTo(10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToULongs()
        {
            Assert.That(theList[0].Ulong, Is.EqualTo(1));
            Assert.That(theList[9].Ulong, Is.EqualTo(10));
        }

        [Test]
        public void ShouldAssignSequenceNumberToGuids()
        {
            Assert.That(theList[0].Guid.ToString(), Is.EqualTo("00000000-0000-0000-0000-000000000001"));
            Assert.That(theList[9].Guid.ToString(), Is.EqualTo("00000000-0000-0000-0000-00000000000a"));
            Assert.That(theList[254].Guid.ToString(), Is.EqualTo("00000000-0000-0000-0000-0000000000ff"));
        }

        [Test]
        public void ShouldAlternateBooleanValues()
        {
            Assert.That(theList[0].Bool, Is.False);
            Assert.That(theList[1].Bool, Is.True);
            Assert.That(theList[2].Bool, Is.False);
            Assert.That(theList[3].Bool, Is.True);
        }

        [Test]
        public void ShouldAssignCurrentDateThenCurrentDatePlus_N_DaysToDateTimeProperties()
        {
            Assert.That(theList[0].DateTime, Is.EqualTo(DateTime.Now.Date));
            Assert.That(theList[9].DateTime, Is.EqualTo(DateTime.Now.Date.AddDays(9)));
        }

        [Test]
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

            Assert.That(theList[0].HasADefaultValue, Is.EqualTo(myClass.HasADefaultValue));
            Assert.That(theList[9].HasADefaultValue, Is.EqualTo(myClass.HasADefaultValue));

        }

        [Test]
        public void ShouldNotTouchInternalProperties()
        {
            Assert.That(theList[0].InternalInt, Is.EqualTo(0));
            Assert.That(theList[9].InternalInt, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNotTouchPrivateProperties()
        {
            var prop0 = typeof(MyClass).GetProperty("PrivateInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0], null);
            var prop9 = typeof(MyClass).GetProperty("PrivateInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9], null);

            Assert.That(prop0, Is.EqualTo(0));
            Assert.That(prop9, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNotTouchProtectedIntProperties()
        {
            var prop0 = typeof(MyClass).GetProperty("ProtectedInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0], null);
            var prop9 = typeof(MyClass).GetProperty("ProtectedInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9], null);

            Assert.That(prop0, Is.EqualTo(0));
            Assert.That(prop9, Is.EqualTo(0));
        }

        [Test]
        public void SupportsInheritedClasses()
        {
            var myClassInheritor = new MyClassInheritor();
            new SequentialPropertyNamer(reflectionUtil,new BuilderSettings()).SetValuesOf(myClassInheritor);

            Assert.That(myClassInheritor.Int, Is.EqualTo(1));
            Assert.That(myClassInheritor.AnotherProperty, Is.EqualTo(1));
        }

        [Test]
        public void DoesNotNameStaticProperties()
        {
            Assert.That(MyClass.StaticInt, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNamePublicFields()
        {
            Assert.That(theList[0].PublicFieldInt, Is.EqualTo(1));
            Assert.That(theList[9].PublicFieldInt, Is.EqualTo(10));
        }

        [Test]
        public void ShouldNotTouchPrivateFields()
        {
            var prop0 = typeof(MyClass).GetField("PrivateFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0]);
            var prop9 = typeof(MyClass).GetField("PrivateFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9]);

            Assert.That(prop0, Is.EqualTo(0));
            Assert.That(prop9, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNotTouchProtectedFields()
        {
            var prop0 = typeof(MyClass).GetField("ProtectedFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0]);
            var prop9 = typeof(MyClass).GetField("ProtectedFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9]);

            Assert.That(prop0, Is.EqualTo(0));
            Assert.That(prop9, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNotTouchInternalFields()
        {
            var prop0 = typeof(MyClass).GetField("InternalFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[0]);
            var prop9 = typeof(MyClass).GetField("InternalFieldInt", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(theList[9]);

            Assert.That(prop0, Is.EqualTo(0));
            Assert.That(prop9, Is.EqualTo(0));
        }

        // TODO FIX
        #if !SILVERLIGHT
        [Test]
        public void SetValuesOfAllIn_ClassWithNullCharConst_CharConstantIsNotSetByNamer()
        {
            var propertyNamer = new SequentialPropertyNamer(reflectionUtil,new BuilderSettings());

            List<MyClassWithCharConst> list = new List<MyClassWithCharConst>() { new MyClassWithCharConst() };

            propertyNamer.SetValuesOfAllIn(list);

            foreach (var item in list)
            {
                Assert.That(item.GetNullCharConst(), Is.EqualTo(MyClassWithCharConst.NullCharConst));
                Assert.That(item.GetNonNullCharConst(), Is.EqualTo(MyClassWithCharConst.NonNullCharConst));
            }

            Assert.Pass("A System.FieldAccessException was not thrown because NBuilder didn't try to set the value of the constant");
        }
        #endif

        // TODO: Add this
        //[Test]
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
