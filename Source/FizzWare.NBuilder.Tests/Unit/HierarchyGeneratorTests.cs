using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class HierarchyGeneratorTests
    {
        private MockRepository mocks;
        private IRandomGenerator randomGenerator;
        private int numberOfRoots;
        private int depth;
        private int minCategories;
        private int maxCategories;
        private IList<MyHierarchicalClass> sourceList;
        private int categoryCount;
        private HierarchyGenerator<MyHierarchicalClass> hierarchyGenerator;
        private IPropertyNamer propertyNamer;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            randomGenerator = mocks.DynamicMock<IRandomGenerator>();
            sourceList = mocks.DynamicMock<IList<MyHierarchicalClass>>();
            propertyNamer = mocks.DynamicMock<IPropertyNamer>();

            categoryCount = 1000;
            numberOfRoots = 4;
            depth = 3;
            minCategories = 0;
            maxCategories = 5;
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldGenerateTheCorrectNumberOfRoots()
        {
            using (mocks.Record())
            {
                sourceList.Expect(x => x.Count).Return(categoryCount).Repeat.Any();
                sourceList.Expect(x => x[0]).Return(new MyHierarchicalClass()).Repeat.Times(numberOfRoots);
            }

            hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, propertyNamer);
            IList<MyHierarchicalClass> hierarchy = hierarchyGenerator.Generate();

            Assert.That(hierarchy.Count, Is.EqualTo(numberOfRoots));
        }

        [Test]
        public void ShouldAddChildren()
        {
            using (mocks.Record())
            {
                sourceList.Expect(x => x.Count).Return(categoryCount).Repeat.Any();
                randomGenerator.Expect(x => x.Next(minCategories, maxCategories)).Return(1).Repeat.Times(12);
                sourceList.Expect(x => x[0]).Return(new MyHierarchicalClass()).Repeat.Any();
                sourceList.Expect(x => x.RemoveAt(0)).Repeat.Any();
            }

            using (mocks.Playback())
            {
                hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, propertyNamer);
                hierarchyGenerator.Generate();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfInitialListIsNotBigEnough()
        {
            int requiredSizeOfList = numberOfRoots * depth * maxCategories;

            using (mocks.Record())
            {
                sourceList.Expect(x => x.Count).Return(requiredSizeOfList - 1);
            }

            using (mocks.Playback())
            {
                hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, propertyNamer);
            }
        }
    }
}