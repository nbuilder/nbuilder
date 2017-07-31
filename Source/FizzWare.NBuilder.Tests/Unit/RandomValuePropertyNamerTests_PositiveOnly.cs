using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class RandomValuePropertyNamerTests_PositiveOnly : RandomValuePropertyNamerTestFixture
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
                generator.Next((short) 0, short.MaxValue).Returns((short) 1);
                generator.Next(0, int.MaxValue).Returns(1);
                generator.Next((long) 0, long.MaxValue).Returns(1);
                generator.Next(ushort.MinValue, ushort.MaxValue).Returns((ushort) 1);
                generator.Next(uint.MinValue, uint.MaxValue).Returns((uint) 1);
                generator.Next(ulong.MinValue, ulong.MaxValue).Returns((ulong) 1);
                generator.Next((float) 0, float.MaxValue).Returns(1);
                generator.Next((double) 0, double.MaxValue).Returns(1);
                generator.Next((decimal) 0, decimal.MaxValue).Returns(1);
                generator.Next(byte.MinValue, byte.MaxValue).Returns((byte) 1);
                generator.Next(char.MinValue, char.MaxValue).Returns('A');
                generator.Next(DateTime.MinValue, DateTime.MaxValue).Returns(DateTime.Today.Date);
                generator.Next().Returns(true);
                generator.Guid().Returns(Guid.NewGuid());
            }
            new RandomValuePropertyNamer(generator, reflectionUtil, true, builderSetup)
                .SetValuesOfAllIn(theList)
                ;
        }
    }
}