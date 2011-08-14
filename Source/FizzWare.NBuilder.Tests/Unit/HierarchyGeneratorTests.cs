using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    // TODO: These tests aren't great and are in need of some attention
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
        private Func<MyHierarchicalClass, string, string> namingMethod;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            randomGenerator = mocks.DynamicMock<IRandomGenerator>();
            sourceList = mocks.DynamicMock<IList<MyHierarchicalClass>>();

            categoryCount = 1000;
            numberOfRoots = 4;
            depth = 3;
            minCategories = 0;
            maxCategories = 5;

            namingMethod = (x, y) => x.Title = y;
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

            hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, namingMethod, null);
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
                hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, namingMethod, null);
                hierarchyGenerator.Generate();
            }
        }

        [Test]
        public void ShouldTryToPersistIfPersistenceServiceSupplied()
        {
            IPersistenceService persistenceService = mocks.DynamicMock<IPersistenceService>();

            using (mocks.Record())
            {
                sourceList.Expect(x => x.Count).Return(categoryCount).Repeat.Any();
                randomGenerator.Expect(x => x.Next(minCategories, maxCategories)).Return(1).Repeat.Times(12);
                sourceList.Expect(x => x[0]).Return(new MyHierarchicalClass()).Repeat.Any();
                sourceList.Expect(x => x.RemoveAt(0)).Repeat.Any();

                persistenceService.Expect(x => x.Create(Arg<MyHierarchicalClass>.Is.TypeOf)).Repeat.Times(12);
                persistenceService.Expect(x => x.Update(Arg<IList<MyHierarchicalClass>>.Is.TypeOf)).Repeat.Times(1);
            }

            using (mocks.Playback())
            {
                hierarchyGenerator = new HierarchyGenerator<MyHierarchicalClass>(sourceList, (x, y) => x.AddChild(y), numberOfRoots, depth, minCategories, maxCategories, randomGenerator, namingMethod, persistenceService);
                hierarchyGenerator.Generate();
            }
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

            using (mocks.Record())
            {
                sourceList.Expect(x => x.Count).Return(requiredSizeOfList - 1);
            }

            using (mocks.Playback())
            {
                // TODO FIX
                #if !SILVERLIGHT
                Assert.Throws<ArgumentException>(
                    () =>
                        hierarchyGenerator =
                        new HierarchyGenerator<MyHierarchicalClass>(sourceList, null, numberOfRoots, depth, minCategories,
                                                                    maxCategories, randomGenerator, namingMethod, null)
                    );
                #endif
            }
        }
    }
}