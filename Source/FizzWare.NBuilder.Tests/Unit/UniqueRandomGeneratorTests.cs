using System;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class UniqueRandomGeneratorTests
    {
        private const string exceptionMessage = "There are no more unique values available";
        private IUniqueRandomGenerator generator;
        private int min = 0;
        private int max = 4;

        public UniqueRandomGeneratorTests()
        {
            generator = new UniqueRandomGenerator();
        }

        [Fact]
        public void Next_Int16_ShouldGenerateWithinRange()
        {
            generator.Next((short)min, (short)max);
        }

        [Fact]
        public void Next_Int16_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((short)min, (short)max);

            // TODO FIX
            #if !SILVERLIGHT
            Should.Throw<InvalidOperationException>(() => generator.Next((short)min, (short)max), exceptionMessage);
            #endif
        }

        [Fact]
        public void Next_Int32_ShouldGenerateWithinRange()
        {
            generator.Next(min, max);
        }

        [Fact]
        public void Next_Int32_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next(min, max);
                
            // TODO FIX
            #if !SILVERLIGHT
            Should.Throw<InvalidOperationException>(() => generator.Next(min, max), exceptionMessage);
            #endif
        }

        [Fact]
        public void Next_Int64_ShouldGenerateWithinRange()
        {
            generator.Next((long)min, (long)max);
        }

        [Fact]
        public void Next_Int64_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((long)min, (long)max);

            // TODO FIX
            #if !SILVERLIGHT
            Should.Throw<InvalidOperationException>(() => generator.Next((long)min, (long)max), exceptionMessage);
            #endif
        }

        [Fact]
        public void Next_UInt16_ShouldGenerateWithinRange()
        {
            generator.Next((ushort)min, (ushort)max);
        }

        [Fact]
        public void Next_UInt16_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((ushort)min, (ushort)max);

            // TODO FIX
            #if !SILVERLIGHT
            Should.Throw<InvalidOperationException>(() => generator.Next((ushort)min, (ushort)max), exceptionMessage);
            #endif
        }

        [Fact]
        public void Next_UInt32_ShouldGenerateWithinRange()
        {
            generator.Next((uint)min, (uint)max);
        }

        [Fact]
        public void Next_UInt32_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((uint)min, (uint)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((uint)min, (uint)max), exceptionMessage);
        }

        [Fact]
        public void Next_UInt64_ShouldGenerateWithinRange()
        {
            generator.Next((ulong)min, (ulong)max);
        }

        [Fact]
        public void Next_UInt64_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((ulong)min, (ulong)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((ulong)min, (ulong)max), exceptionMessage);
        }

        [Fact]
        public void Next_Single_ShouldGenerateWithinRange()
        {
            generator.Next((float)min, (float)max);
        }

        [Fact]
        public void Next_Single_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((float)min, (float)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((float)min, (float)max), exceptionMessage);
        }

        [Fact]
        public void Next_Double_ShouldGenerateWithinRange()
        {
            double min = 1.0;
            var result = generator.Next(min, (double)max);
            result.ShouldBeGreaterThan(min);
            result.ShouldBeLessThan(max);
        }

        [Fact]
        public void Next_Double_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((double)min, (double)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((double)min, (double)max), exceptionMessage);
        }

        [Fact]
        public void Next_Decimal_ShouldGenerateWithinRange()
        {
            generator.Next((decimal)min, (decimal)max);
        }

        [Fact]
        public void Next_Decimal_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((decimal)min, (decimal)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((decimal)min, (decimal)max), exceptionMessage);
        }

        [Fact]
        public void Next_Byte_ShouldGenerateWithinRange()
        {
            generator.Next((byte)min, (byte)max);
        }
        
        [Fact]
        public void Next_Byte_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((byte)min, (byte)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((byte)min, (byte)max), exceptionMessage);
        }

        [Fact]
        public void Next_SByte_ShouldGenerateWithinRange()
        {
            var value = generator.Next((sbyte)min, (sbyte)max);

            value.ShouldBeGreaterThanOrEqualTo((sbyte)min);
            value.ShouldBeLessThanOrEqualTo((sbyte)max);
        }

        [Fact]
        public void Next_SByte_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((sbyte)min, (sbyte)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((sbyte)min, (sbyte)max), exceptionMessage);
        }

        [Fact]
        public void Next_Char_ShouldGenerateWithinRange()
        {
            generator.Next((char)min, (char)max);
        }

        [Fact]
        public void Next_Char_ShouldGenerateUniqueNumbers()
        {
            for (int i = 0; i < max; i++)
                generator.Next((char)min, (char)max);

            Should.Throw<InvalidOperationException>(() => generator.Next((char)min, (char)max), exceptionMessage);
        }

        [Fact]
        public void EnumerationOfT_EnumerateAllEnumerationsInEnum_GeneratesEachEnumValueWithoutThrowingAnException()
        {
            foreach (var enums in EnumHelper.GetValues<MyEnum>())
                Should.NotThrow(() => generator.Enumeration<MyEnum>());
        }

        [Fact]
        public void EnumerationOfType_EnumerateAllEnumerationsInEnum_GeneratesEachEnumValueWithoutThrowingAnException()
        {
            foreach (var enums in EnumHelper.GetValues<MyEnum>())
                Should.NotThrow(() => generator.Enumeration(typeof(MyEnum)));
        } 
    }
}