using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomValuePropertyNamerTests_LoremIpsumStrings
    {
        protected IRandomGenerator generator;
        protected IList<MyClass> theList;
        protected const int listSize = 10;
        protected IReflectionUtil reflectionUtil;

        [SetUp]
        public void SetUp()
        {
            generator = Substitute.For<IRandomGenerator>();
            reflectionUtil = Substitute.For<IReflectionUtil>();

            reflectionUtil.IsDefaultValue(null).Returns(true);

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            // The lorem ipsum string generator does this to get a random length of the string
            generator.Next(1, 10).Returns(5);

            new RandomValuePropertyNamer(generator, reflectionUtil, false, DateTime.MinValue, DateTime.MaxValue, true,new BuilderSettings()).SetValuesOfAllIn(theList);
        }

        [Test]
        public void ShouldNameStringsUsingLoremIpsumText()
        {
            string[] words = @"lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborum".Split(' ');

            string[] actual = theList[0].StringOne.Split(' ');

            var wordList = words.ToList();

            for (int i = 0; i < actual.Length; i++)
                Assert.That(wordList.Contains(actual[i]));
        }
    }
}