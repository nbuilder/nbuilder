using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomValuePropertyNamerTests_PositiveOnly : RandomValuePropertyNamerTestFixture
    {
        public override void TestFixtureSetUp()
        {
            mocks = new MockRepository();

            generator = mocks.DynamicMock<IRandomGenerator>();

            reflectionUtil = MockRepository.GenerateStub<IReflectionUtil>();
            reflectionUtil.Stub(x => x.IsDefaultValue(null)).IgnoreArguments().Return(true).Repeat.Any();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            using (mocks.Record())
            {
                generator.Expect(x => x.Next((short)0, short.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(0, int.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next((long)0, long.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(ushort.MinValue, ushort.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(uint.MinValue, uint.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(ulong.MinValue, ulong.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next((float)0, float.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next((double)0, double.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next((decimal)0, decimal.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(byte.MinValue, byte.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(char.MinValue, char.MaxValue)).Return('A').Repeat.Times(listSize);
                generator.Expect(x => x.Next(DateTime.MinValue, DateTime.MaxValue)).Return(DateTime.Today.Date).Repeat.Times(listSize);
                generator.Expect(x => x.Next()).Return(true).Repeat.Times(listSize);
                generator.Expect(x => x.Guid()).Return(Guid.NewGuid()).Repeat.Times(listSize);
            }

            using (mocks.Playback())
            {
                new RandomValuePropertyNamer(generator, reflectionUtil, true).SetValuesOfAllIn(theList);
            }
        }
    }
}