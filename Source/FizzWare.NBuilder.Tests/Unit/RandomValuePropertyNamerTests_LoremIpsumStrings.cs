using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomValuePropertyNamerTests_LoremIpsumStrings
    {
        protected MockRepository mocks;
        protected IRandomGenerator generator;
        protected IList<MyClass> theList;
        protected const int listSize = 10;
        protected IReflectionUtil reflectionUtil;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            generator = MockRepository.GenerateStub<IRandomGenerator>();
            reflectionUtil = MockRepository.GenerateStub<IReflectionUtil>();

            reflectionUtil.Stub(x => x.IsDefaultValue(null)).IgnoreArguments().Return(true).Repeat.Any();

            theList = new List<MyClass>();

            for (int i = 0; i < listSize; i++)
                theList.Add(new MyClass());

            // The lorem ipsum string generator does this to get a random length of the string
            generator.Expect(x => x.Next(1, 10)).Return(5).Repeat.Any();

            new RandomValuePropertyNamer(generator, reflectionUtil, false, DateTime.MinValue, DateTime.MaxValue, true).SetValuesOfAllIn(theList);
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