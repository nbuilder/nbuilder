using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomValuePropertyNamerTests_Default : RandomValuePropertyNamerTestFixture
    {
        public override void TestFixtureSetUp()
        {
            var builderSetup = new BuilderSettings();

            generator = Substitute.For<IRandomGenerator>();

            reflectionUtil = Substitute.For<IReflectionUtil>();
            reflectionUtil.IsDefaultValue(null).Returns(true);

            reflectionUtil = new ReflectionUtil();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            {
                generator.Next(short.MinValue, short.MaxValue).Returns((short) 1);
                generator.Next(int.MinValue, int.MaxValue).Returns(1);
                generator.Next(long.MinValue, long.MaxValue).Returns(1);
                generator.Next(ushort.MinValue, ushort.MaxValue).Returns((ushort) 1);
                generator.Next(uint.MinValue, uint.MaxValue).Returns((uint) 1);
                generator.Next(ulong.MinValue, ulong.MaxValue).Returns((ulong) 1);
                generator.Next(float.MinValue, float.MaxValue).Returns(1);
                generator.Next(double.MinValue, double.MaxValue).Returns(1);
                generator.Next(decimal.MinValue, decimal.MaxValue).Returns(1);
                generator.Next(byte.MinValue, byte.MaxValue).Returns((byte) 1);
                generator.Next(char.MinValue, char.MaxValue).Returns('A');
                generator.Next(DateTime.MinValue, DateTime.MaxValue).Returns(DateTime.Today.Date);
                generator.Next().Returns(true);

                generator.Next(0, 255).Returns(5);
            }
            new RandomValuePropertyNamer(generator, reflectionUtil, false, builderSetup)
                .SetValuesOfAllIn(theList);
        }

        [Test]
        public void ShouldBeAbleToCreateUsingDefaultConstructor()
        {
            var builderSetup = new BuilderSettings();
            new RandomValuePropertyNamer(builderSetup);
        }

        // TODO FIX
        #if !SILVERLIGHT
        [Test]
        public void SetValuesOfAllIn_ClassWithNullCharConst_CharConstantIsNotSetByNamer()
        {
            var builderSetup = new BuilderSettings();
            var propertyNamer = new RandomValuePropertyNamer(builderSetup);

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
    }
}