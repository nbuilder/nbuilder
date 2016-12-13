using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;

namespace FizzWare.NBuilder.Tests.Unit
{
    // TODO: These tests aren't great and are in need of some attention
    [TestFixture]
    public class HierarchyGeneratorTests
    {
        private IRandomGenerator randomGenerator;
        private int numberOfRoots;
        private int depth;
        private int minCategories;
        private int maxCategories;
        private IList<MyHierarchicalClass> sourceList;
        private int categoryCount;
        private HierarchyGenerator<MyHierarchicalClass> hierarchyGenerator;
        private Func<MyHierarchicalClass, string, string> namingMethod;

        [SetUp]
        public void SetUp()
        {
            randomGenerator = Substitute.For<IRandomGenerator>();
            sourceList = Substitute.For<IList<MyHierarchicalClass>>();

            categoryCount = 1000;
            numberOfRoots = 4;
            depth = 3;
            minCategories = 0;
            maxCategories = 5;

            namingMethod = (x, y) => x.Title = y;
        }

        [Test]
        public void ShouldGenerateTheCorrectNumberOfRoots()
        {
            sourceList.Count.Returns(categoryCount);
            sourceList[0].Returns(new MyHierarchicalClass());

            hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, namingMethod, null);
            IList<MyHierarchicalClass> hierarchy = hierarchyGenerator.Generate();

            Assert.That(hierarchy.Count, Is.EqualTo(numberOfRoots));
        }

        [Test]
        public void ShouldAddChildren()
        {
            sourceList.Count.Returns(categoryCount);
            randomGenerator.Next(minCategories, maxCategories).Returns(1);
            sourceList[0].Returns(new MyHierarchicalClass());
            sourceList.RemoveAt(0);

            hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, namingMethod, null);
            hierarchyGenerator.Generate();

        }

        [Test]
        public void ShouldTryToPersistIfPersistenceServiceSupplied()
        {
            IPersistenceService persistenceService = Substitute.For<IPersistenceService>();

            sourceList.Count.Returns(categoryCount);
            randomGenerator.Next(minCategories, maxCategories).Returns(1);
            sourceList[0].Returns(new MyHierarchicalClass());

            sourceList.RemoveAt(0);

            persistenceService.Create(Arg.Any<MyHierarchicalClass>());
            persistenceService.Update(Arg.Any<IList<MyHierarchicalClass>>());

            hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, namingMethod, persistenceService);
            hierarchyGenerator.Generate();

        }

        [Test]
        public void HierarchyGeneratorConstructor_SourceListNotBigEnough_Complains()
        {
            // Formula is (maxCategories^0 + maxCategories^1 + maxCategories^2) * numRoots

            // = (5^0 + 5^1 + 5^2) * 4
            // = (1 + 5 + 25) * 4
            // = 31 * 4
            // = 124

            const int requiredSizeOfList = 124;

            sourceList.Count.Returns(requiredSizeOfList - 1);

            Assert.Throws<ArgumentException>(
                () =>
                    hierarchyGenerator =
                    new HierarchyGenerator<MyHierarchicalClass>(sourceList, null, numberOfRoots, depth, minCategories,
                                                                maxCategories, randomGenerator, namingMethod, null)
                );
        }
    }
}