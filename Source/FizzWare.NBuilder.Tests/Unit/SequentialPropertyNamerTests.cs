using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class SequentialPropertyNamerTests
    {
        private IList<MyClass> theList;
        private const int listSize = 1000;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var reflectionUtil = MockRepository.GenerateStub<IReflectionUtil>();
            reflectionUtil.Stub(x => x.IsDefaultValue(null)).IgnoreArguments().Return(true).Repeat.Any();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());
            
            new SequentialPropertyNamer<MyClass>(reflectionUtil).SetValuesOfAllIn(theList);
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
        [Description("The desired behaviour is byte properties should be given the values 1...255, 1...255, 1...255 repeatedly")]
        public void ShouldRestartFromZeroToBytesWhenGoesAboveMaxValue()
        {
            Assert.That(theList[0].Byte, Is.EqualTo(1));
            Assert.That(theList[byte.MaxValue].Byte, Is.EqualTo(1));
            Assert.That(theList[byte.MaxValue * 2].Byte, Is.EqualTo(1));
            Assert.That(theList[byte.MaxValue * 3].Byte, Is.EqualTo(1));
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
        public void ShouldOnlyNamePropertiesThatAreNullOrDefault()
        {
            var myClass = new MyClass();
            var reflectionUtil = MockRepository.GenerateMock<IReflectionUtil>();

            reflectionUtil.Expect(x => x.IsDefaultValue(myClass.HasADefaultValue)).Return(false).Repeat.Times(listSize);

            reflectionUtil.Expect(x => x.IsDefaultValue(Arg<object>.Is.NotEqual(myClass.HasADefaultValue))).
                IgnoreArguments().Return(true).Repeat.AtLeastOnce();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            new SequentialPropertyNamer<MyClass>(reflectionUtil).SetValuesOfAllIn(theList);

            Assert.That(theList[0].HasADefaultValue, Is.EqualTo(myClass.HasADefaultValue));
            Assert.That(theList[9].HasADefaultValue, Is.EqualTo(myClass.HasADefaultValue));

            reflectionUtil.VerifyAllExpectations();
        }
    }
}