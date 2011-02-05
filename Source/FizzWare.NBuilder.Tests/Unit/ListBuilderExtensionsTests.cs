using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    [TestFixture]
    public class ListBuilderExtensionsTests
    {
        private const int listSize = 30;
        private IListBuilderImpl<MyClass> listBuilderImpl;

        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            listBuilderImpl = mocks.DynamicMock<IListBuilderImpl<MyClass>>();
        }

        [Test]
        public void TheFirstShouldReturnARangeDeclaration()
        {
            var rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 0);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(30);
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Matches(y => y.Start == 0 && y.End == 9))).Return(rangeDeclaration);
            }

            using (mocks.Playback())
            {
                var declaration = ListBuilderExtensions.TheFirst(listBuilderImpl, 10);
                Assert.That(declaration, Is.SameAs(rangeDeclaration));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TheFirstAmountMustBeOneOrGreater()
        {
            ListBuilderExtensions.TheFirst(listBuilderImpl, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TheFirstAmountShouldBeLessThanListCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.TheFirst(listBuilderImpl, 11);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TheLastAmountMustBeOneOrGreater()
        {
            ListBuilderExtensions.TheLast(listBuilderImpl, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TheLastAmountShouldBeLessThanListCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.TheLast(listBuilderImpl, 11);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomAmountMustBeOneOrGreater()
        {
            ListBuilderExtensions.Random(listBuilderImpl, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomAmountShouldBeLessThanListCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.Random(listBuilderImpl, 11);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SectionStartMustBeGreaterThanZero()
        {
            ListBuilderExtensions.Section(listBuilderImpl, -1, 10);
        }

        [Test]
        public void SectionEndMustBeGreaterThanOne()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            Assert.Throws<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 0, 0));
        }

        [Test]
        public void SectionStartMustBeLessThanEnd()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            Assert.Throws<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 6, 5));
        }

        [Test]
        public void SectionStartCannotEqualEnd()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            Assert.Throws<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 5, 5));
        }

        [Test]
        public void SectionCanCoverWholeList()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.Section(listBuilderImpl, 0, 9);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TheNextAmountShouldBeGreaterThanOne()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.TheNext(listBuilderImpl, 0);
        }

        [Test]
        public void SectionStartMustBeLessThanCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            var ex = Assert.Throws<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 10, 10));

            Assert.That(ex.Message.Contains("start"));
        }

        [Test]
        public void SectionEndMustBeLessThanCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            Assert.Throws<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 9, 10));
        }

        [Test]
        public void LastRangeShouldBeTheCapacityMinusTheRangeSizeAndOneLessThanTheCapacity()
        {
            const int rangeSize = 10;
            const int startIndex = listSize - rangeSize; // 20
            const int endIndex = listSize - 1; // 29

            var rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 0);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(listSize).Repeat.Times(3);
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Matches(y => y.Start == startIndex && y.End == endIndex))).Return(rangeDeclaration);
            }

            using (mocks.Playback())
            {
                var declaration = ListBuilderExtensions.TheLast(listBuilderImpl, 10);
                Assert.That(declaration, Is.SameAs(rangeDeclaration));
            }
        }

        [Test]
        public void TheNextShouldReturnRangeDeclaration()
        {
            IDeclarationQueue<MyClass> declarationQueue = mocks.StrictMock<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 9);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                declarationQueue.Expect(x => x.GetLastItem()).Return(rangeDeclaration);
                listBuilderImpl.Expect(x => x.Declarations).Return(declarationQueue);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Is.TypeOf)).Return(null);
            }

            using (mocks.Playback())
            {
                var andTheNextDeclaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.TheNext(listBuilderImpl, 10);

                Assert.That(andTheNextDeclaration.Start, Is.EqualTo(10));
                Assert.That(andTheNextDeclaration.End, Is.EqualTo(19));
            }
        }

        [Test]
        public void ShouldOnlyAddTheDeclarationIfTheRangeIsValid()
        {
            IDeclarationQueue<MyClass> declarationQueue = mocks.StrictMock<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 9);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                declarationQueue.Expect(x => x.GetLastItem()).Return(rangeDeclaration);
                listBuilderImpl.Expect(x => x.Declarations).Return(declarationQueue);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Is.TypeOf)).Throw(new BuilderException(""));
            }

            using (mocks.Playback())
            {
                Assert.Throws<BuilderException>(
                    () => ListBuilderExtensions.TheNext(listBuilderImpl, 30)
                );
            }
        }

        [Test]
        public void ShouldBeAbleToUseThePrevious()
        {
            IDeclarationQueue<MyClass> declarationQueue = mocks.StrictMock<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 10, 19);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                declarationQueue.Expect(x => x.GetLastItem()).Return(rangeDeclaration);
                listBuilderImpl.Expect(x => x.Declarations).Return(declarationQueue);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Is.TypeOf)).Return(null);
            }

            using (mocks.Playback())
            {
                var thePreviousDeclaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.ThePrevious(listBuilderImpl, 10);

                Assert.That(thePreviousDeclaration.Start, Is.EqualTo(0));
                Assert.That(thePreviousDeclaration.End, Is.EqualTo(9));
            }
        }

        [Test]
        public void ShouldBeAbleToUseSection()
        {
            var rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 10, 19);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(20);
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Matches(y => y.Start == 10 && y.End == 19))).Return(rangeDeclaration);
            }

            using (mocks.Playback())
            {
                var whereSection = (RangeDeclaration<MyClass>)ListBuilderExtensions.Section(listBuilderImpl, 10, 19);

                Assert.That(whereSection.Start, Is.EqualTo(10));
                Assert.That(whereSection.End, Is.EqualTo(19));                
            }
        }

        [Test]
        public void RandomShouldReturnRandomDeclarationOfRangeOfWholeList()
        {
            const int amount = 5;
            const int end = listSize;

            var randomDeclaration = new RandomDeclaration<MyClass>(listBuilderImpl, null, null, amount, 0, end);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(listSize).Repeat.Any();
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RandomDeclaration<MyClass>>.Matches(y => y.Start == 0 && y.End == end))).Return(randomDeclaration);
            }

            IDeclaration<MyClass> declaration;
            using (mocks.Playback())
            {
                declaration = (IDeclaration<MyClass>)ListBuilderExtensions.Random(listBuilderImpl, amount);
            }

            Assert.That(declaration.Start, Is.EqualTo(0));
            Assert.That(declaration.End, Is.EqualTo(end));
        }

        [Test]
        public void RandomCanReturnDeclarationForASectionOfTheList()
        {
            const int amount = 5;
            const int start = 10;
            const int end = 20;

            var randomDeclaration = new RandomDeclaration<MyClass>(listBuilderImpl, null, null, amount, start, end);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(listSize).Repeat.Any();
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RandomDeclaration<MyClass>>.Matches(y => y.Start == start && y.End == end))).Return(randomDeclaration);
            }

            
            var declaration = (IDeclaration<MyClass>)ListBuilderExtensions.Random(listBuilderImpl, amount, start, end);

            Assert.That(declaration.Start, Is.EqualTo(start));
            Assert.That(declaration.End, Is.EqualTo(end));
        }
        
        [Test]
        public void ShouldBeAbleToUseBuildHierarchy()
        {
            var hierarchySpec = mocks.Stub<IHierarchySpec<MyClass>>();

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Build()).Return(new List<MyClass>()).Repeat.Any();
            }

            using (mocks.Playback())
            {
                
                var list = ListBuilderExtensions.BuildHierarchy(listBuilderImpl, hierarchySpec);

                Assert.That(list, Is.TypeOf(typeof(List<MyClass>)));
           }
        }

        [Test]
        public void ShouldBeAbleToUsePersistHierarchy()
        {
            var hierarchySpec = mocks.Stub<IHierarchySpec<MyClass>>();
            var persistenceService = mocks.DynamicMock<IPersistenceService>();

            BuilderSetup.SetPersistenceService(persistenceService);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Build()).Return(new List<MyClass>()).Repeat.Any();
                persistenceService.Expect(x => x.Create(Arg<MyClass>.Is.TypeOf)).Repeat.Any();
                persistenceService.Expect(x => x.Update(Arg<IList<MyClass>>.Is.TypeOf)).Repeat.Once();
            }

            using (mocks.Playback())
            {
                ListBuilderExtensions.PersistHierarchy(listBuilderImpl, hierarchySpec);
            }
        }

        ////[Test]
        ////public void ShouldBeAbleToUseAndTheRemaining()
        ////{
        ////    const int start = 0;
        ////    const int end = 9;
        ////    const int capacity = 10;

        ////    IDeclarationQueue<MyClass> declarationQueue = mocks.StrictMock<IDeclarationQueue<MyClass>>();
        ////    RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, start, end);

        ////    using (mocks.Record())
        ////    {
        ////        declarationQueue.Expect(x => x.GetLastItem()).Return(rangeDeclaration);
        ////        listBuilderImpl.Expect(x => x.Declarations).Return(declarationQueue);
        ////        listBuilderImpl.Expect(x => x.Capacity).Return(capacity).Repeat.Any();
        ////        listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Is.TypeOf)).Return(rangeDeclaration);
        ////    }

        ////    using (mocks.Playback())
        ////    {
        ////        var declaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.TheRemainder(listBuilderImpl);

        ////        Assert.That(declaration.Start, Is.EqualTo(start));
        ////        Assert.That(declaration.End, Is.EqualTo(end));
        ////    }
        ////}
    }
    // ReSharper restore InvokeAsExtensionMethod
}