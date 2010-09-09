using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class UniqueRandomGeneratorTests
    {
        private const string exceptionMessage = "There are no more unique values available";
        private IUniqueRandomGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new UniqueRandomGenerator();
        }

        [Test]
        public void ShouldGenerateWithinRange_Int16()
        {
            generator.Next((short)0, (short)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Int16()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((short)0, (short)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Int32()
        {
            generator.Next(0, 4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Int32()
        {
            for (int i = 0; i < 5; i++)
                generator.Next(0, 4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Int64()
        {
            generator.Next((long)0, (long)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Int64()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((long)0, (long)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_UInt16()
        {
            generator.Next((ushort)0, (ushort)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_UInt16()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((ushort)0, (ushort)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_UInt32()
        {
            generator.Next((uint)0, (uint)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_UInt32()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((uint)0, (uint)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_UInt64()
        {
            generator.Next((ulong)0, (ulong)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_UInt64()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((ulong)0, (ulong)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Single()
        {
            generator.Next((float)0, (float)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Single()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((float)0, (float)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Double()
        {
            generator.Next((double)0, (double)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Double()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((double)0, (double)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Decimal()
        {
            generator.Next((decimal)0, (decimal)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Decimal()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((decimal)0, (decimal)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Byte()
        {
            generator.Next((byte)0, (byte)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_SByte()
        {
            var value =  generator.Next((sbyte)0, (sbyte)4);

            Assert.That(value, Is.GreaterThanOrEqualTo(0));
            Assert.That(value, Is.LessThanOrEqualTo(4));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Byte()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((byte)0, (byte)4);
        }

        [Test]
        public void ShouldGenerateWithinRange_Char()
        {
            generator.Next((char)0, (char)4);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = exceptionMessage)]
        public void ShouldGenerateUniqueNumbers_Char()
        {
            for (int i = 0; i < 5; i++)
                generator.Next((char)0, (char)4);
        }

        [Test]
        public void EnumerationOfT_EnumerateAllEnumerationsInEnum_GeneratesEachEnumValueWithoutThrowingAnException()
        {
            foreach (var enums in EnumHelper.GetValues<MyEnum>())
                Assert.DoesNotThrow(() => generator.Enumeration<MyEnum>());
        }

        [Test]
        public void EnumerationOfType_EnumerateAllEnumerationsInEnum_GeneratesEachEnumValueWithoutThrowingAnException()
        {
            foreach (var enums in EnumHelper.GetValues<MyEnum>())
                Assert.DoesNotThrow(() => generator.Enumeration(typeof(MyEnum)));
        } 
    }
}