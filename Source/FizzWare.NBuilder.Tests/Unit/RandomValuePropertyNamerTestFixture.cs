using System;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

namespace FizzWare.NBuilder.Tests.Unit
{
    public abstract class RandomValuePropertyNamerTestFixture
    {
        protected MockRepository mocks;
        protected IRandomGenerator generator;
        protected IList<MyClass> theList;
        protected const int listSize = 10;
        protected IReflectionUtil reflectionUtil;

        [OneTimeSetUp]
        public abstract void TestFixtureSetUp();

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldNameInt16Properties()
        {
            Assert.That(theList[0].Short, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameInt32Properties()
        {
            Assert.That(theList[0].Int, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameInt64Properties()
        {
            Assert.That(theList[0].Long, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameUInt16Properties()
        {
            Assert.That(theList[0].Ushort, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameUInt32Properties()
        {
            Assert.That(theList[0].Uint, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameUInt64Properties()
        {
            Assert.That(theList[0].Ulong, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameSingleProperties()
        {
            Assert.That(theList[0].Float, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameDoubleProperties()
        {
            Assert.That(theList[0].Double, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameDecimalProperties()
        {
            Assert.That(theList[0].Decimal, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameByteProperties()
        {
            Assert.That(theList[0].Byte, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNameCharProperties()
        {
            Assert.That(theList[0].Char, Is.EqualTo('A'));
        }

        [Test]
        public void ShouldNameDateTimeProperties()
        {
            Assert.That(theList[0].DateTime, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void ShouldNameBooleanProperties()
        {
            Assert.That(theList[0].Bool, Is.EqualTo(true));
        }

        [Test]
        public void ShouldNameStringProperties()
        {
            Assert.That(theList[0].StringOne, Is.Not.Null);
        }

        [Test]
        public void ShouldNameEnumProperties()
        {
            Assert.That(theList[0].EnumProperty, Is.Not.Null);
        }
    }
}