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
    public class RandomValuePropertyNamerTests_Default : RandomValuePropertyNamerTestFixture
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
                generator.Expect(x => x.Next(short.MinValue, short.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(int.MinValue, int.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(long.MinValue, long.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(ushort.MinValue, ushort.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(uint.MinValue, uint.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(ulong.MinValue, ulong.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(float.MinValue, float.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(double.MinValue, double.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(decimal.MinValue, decimal.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(byte.MinValue, byte.MaxValue)).Return(1).Repeat.Times(listSize);
                generator.Expect(x => x.Next(char.MinValue, char.MaxValue)).Return('A').Repeat.Times(listSize);
                generator.Expect(x => x.Next(DateTime.MinValue, DateTime.MaxValue)).Return(DateTime.Today.Date).Repeat.Times(listSize);
                generator.Expect(x => x.Next()).Return(true).Repeat.Times(listSize);

                generator.Expect(x => x.Next(0, 255)).Return(5).Repeat.Any();
            }

            using (mocks.Playback())
            {
                new RandomValuePropertyNamer(generator, reflectionUtil, false).SetValuesOfAllIn(theList);
            }
        }

        [Test]
        public void ShouldBeAbleToCreateUsingDefaultConstructor()
        {
            new RandomValuePropertyNamer();
        }

        [Test]
        public void SetValuesOfAllIn_ClassWithNullCharConst_CharConstantIsNotSetByNamer()
        {
            var propertyNamer = new RandomValuePropertyNamer();

            List<MyClassWithCharConst> list = new List<MyClassWithCharConst>() { new MyClassWithCharConst() };

            propertyNamer.SetValuesOfAllIn(list);

            foreach (var item in list)
            {
                Assert.That(item.GetNullCharConst(), Is.EqualTo(MyClassWithCharConst.NullCharConst));
                Assert.That(item.GetNonNullCharConst(), Is.EqualTo(MyClassWithCharConst.NonNullCharConst));
            }            

            Assert.Pass("A System.FieldAccessException was not thrown because NBuilder didnt try to set the value of the constant");

        }
    }
}