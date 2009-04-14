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
        private IRandomGenerator<int> randomGenerator;
        private int numberOfRoots;
        private int depth;
        private int minCategories;
        private int maxCategories;
        private IList<MyHierarchicalClass> sourceList;
        private int categoryCount;
        private HierarchyGenerator<MyHierarchicalClass> hierarchyGenerator;
        private IPropertyNamer<MyHierarchicalClass> propertyNamer;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            randomGenerator = mocks.DynamicMock<IRandomGenerator<int>>();
            sourceList = mocks.DynamicMock<IList<MyHierarchicalClass>>();
            propertyNamer = mocks.DynamicMock<IPropertyNamer<MyHierarchicalClass>>();

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
                randomGenerator.Expect(x => x.Generate(minCategories, maxCategories)).Return(1).Repeat.Times(12);
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
            int requiredSizeOfList = numberOfRoots*depth*maxCategories;

            using (mocks.Record())
            {
                sourceList.Expect(x => x.Count).Return(requiredSizeOfList - 1);
            }

            using (mocks.Playback())
            {
                hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, propertyNamer);
            }
        }

        [Test]
        public void ShouldApplyNamingStrategy()
        {
            var initialList = Builder<MyHierarchicalClass>.CreateListOfSize(60).Build();

            using (mocks.Record())
            {
                randomGenerator.Expect(x => x.Generate(minCategories, maxCategories)).Return(1).Repeat.Any();
                
                propertyNamer.Expect(x => x.SetValuesOf(initialList[0], 1, "1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[1], 2, "2"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[2], 3, "3"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[3], 4, "4"));

                propertyNamer.Expect(x => x.SetValuesOf(initialList[4], 1, "1.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[5], 1, "1.1.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[6], 1, "1.1.1.1"));

                propertyNamer.Expect(x => x.SetValuesOf(initialList[7], 1, "2.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[8], 1, "2.1.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[9], 1, "2.1.1.1"));

                propertyNamer.Expect(x => x.SetValuesOf(initialList[10], 1, "3.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[11], 1, "3.1.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[12], 1, "3.1.1.1"));

                propertyNamer.Expect(x => x.SetValuesOf(initialList[13], 1, "4.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[14], 1, "4.1.1"));
                propertyNamer.Expect(x => x.SetValuesOf(initialList[15], 1, "4.1.1.1"));
            }

            using (mocks.Playback())
            {
                hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(initialList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, propertyNamer);
                hierarchyGenerator.Generate();
            }
        }
    }
}