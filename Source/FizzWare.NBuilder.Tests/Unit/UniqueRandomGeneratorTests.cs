using System;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class UniqueRandomGeneratorTests
    {
        private const string exceptionMessage = "There are no more unique values available";
        private IUniqueRandomGenerator generator;
        private int min = 0;
        private int max = 4;

        [SetUp]
        public void SetUp()
        {
            generator = new UniqueRandomGenerator();
        }

        [Test]
        public void Next_Int16_ShouldGenerateWithinRange()
        {
            generator.Next((short)min, (short)max);
        }

        [Test]
        public void Next_Int16_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((short)min, (short)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((short)min, (short)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Int32_ShouldGenerateWithinRange()
        {
            generator.Next(min, max);
        }

        [Test]
        public void Next_Int32_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next(min, max);
                
            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next(min, max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Int64_ShouldGenerateWithinRange()
        {
            generator.Next((long)min, (long)max);
        }

        [Test]
        public void Next_Int64_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((long)min, (long)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((long)min, (long)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_UInt16_ShouldGenerateWithinRange()
        {
            generator.Next((ushort)min, (ushort)max);
        }

        [Test]
        public void Next_UInt16_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((ushort)min, (ushort)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((ushort)min, (ushort)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_UInt32_ShouldGenerateWithinRange()
        {
            generator.Next((uint)min, (uint)max);
        }

        [Test]
        public void Next_UInt32_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((uint)min, (uint)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((uint)min, (uint)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_UInt64_ShouldGenerateWithinRange()
        {
            generator.Next((ulong)min, (ulong)max);
        }

        [Test]
        public void Next_UInt64_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((ulong)min, (ulong)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((ulong)min, (ulong)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Single_ShouldGenerateWithinRange()
        {
            generator.Next((float)min, (float)max);
        }

        [Test]
        public void Next_Single_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((float)min, (float)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((float)min, (float)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Double_ShouldGenerateWithinRange()
        {
            generator.Next((double)min, (double)max);
        }

        [Test]
        public void Next_Double_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((double)min, (double)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((double)min, (double)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Decimal_ShouldGenerateWithinRange()
        {
            generator.Next((decimal)min, (decimal)max);
        }

        [Test]
        public void Next_Decimal_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((decimal)min, (decimal)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((decimal)min, (decimal)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Byte_ShouldGenerateWithinRange()
        {
            generator.Next((byte)min, (byte)max);
        }
        
        [Test]
        public void Next_Byte_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((byte)min, (byte)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((byte)min, (byte)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_SByte_ShouldGenerateWithinRange()
        {
            var value = generator.Next((sbyte)min, (sbyte)max);

            Assert.That(value, Is.GreaterThanOrEqualTo(min));
            Assert.That(value, Is.LessThanOrEqualTo(max));
        }

        [Test]
        public void Next_SByte_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((sbyte)min, (sbyte)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((sbyte)min, (sbyte)max), exceptionMessage);
            #endif
        }

        [Test]
        public void Next_Char_ShouldGenerateWithinRange()
        {
            generator.Next((char)min, (char)max);
        }

        [Test]
        public void Next_Char_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((char)min, (char)max);

            // TODO FIX
            #if !SILVERLIGHT
            Assert.Throws<InvalidOperationException>(() => generator.Next((char)min, (char)max), exceptionMessage);
            #endif
        }

        [Test]
        public void EnumerationOfT_EnumerateAllEnumerationsInEnum_GeneratesEachEnumValueWithoutThrowingAnException()
        {
            // TODO FIX
            #if !SILVERLIGHT
            foreach (var enums in EnumHelper.GetValues<MyEnum>())
                Assert.DoesNotThrow(() => generator.Enumeration<MyEnum>());
            #endif
        }

        [Test]
        public void EnumerationOfType_EnumerateAllEnumerationsInEnum_GeneratesEachEnumValueWithoutThrowingAnException()
        {
            // TODO FIX
            #if !SILVERLIGHT
            foreach (var enums in EnumHelper.GetValues<MyEnum>())
                Assert.DoesNotThrow(() => generator.Enumeration(typeof(MyEnum)));
            #endif
        } 
    }
}