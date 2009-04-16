using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
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
        public void WhereTheFirstShouldReturnARangeDeclaration()
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
                var declaration = ListBuilderExtensions.WhereTheFirst(listBuilderImpl, 10);
                Assert.That(declaration, Is.SameAs(rangeDeclaration));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereTheFirstAmountMustBeOneOrGreater()
        {
            ListBuilderExtensions.WhereTheFirst(listBuilderImpl, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereTheFirstAmountShouldBeLessThanListCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereTheFirst(listBuilderImpl, 11);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereTheLastAmountMustBeOneOrGreater()
        {
            ListBuilderExtensions.WhereTheLast(listBuilderImpl, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereTheLastAmountShouldBeLessThanListCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereTheLast(listBuilderImpl, 11);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereRandomAmountMustBeOneOrGreater()
        {
            ListBuilderExtensions.WhereRandom(listBuilderImpl, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereRandomAmountShouldBeLessThanListCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereRandom(listBuilderImpl, 11);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereSectionStartMustBeGreaterThanZero()
        {
            ListBuilderExtensions.WhereSection(listBuilderImpl, -1, 10);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereSectionEndMustBeGreaterThanOne()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereSection(listBuilderImpl, 0, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereSectionStartMustBeLessThanEnd()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereSection(listBuilderImpl, 6, 5);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereSectionStartCannotEqualEnd()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereSection(listBuilderImpl, 5, 5);
        }

        [Test]
        public void WhereSectionCanCoverWholeList()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereSection(listBuilderImpl, 0, 9);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AndTheNextAmountShouldBeGreaterThanOne()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.AndTheNext(listBuilderImpl, 0);
        }

        // This doesn't seem to work - must be a bug in nunit
        //[ExpectedException(typeof(ArgumentException), ExpectedMessage = "start", MatchType = MessageMatch.Contains)]
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereSectionStartMustBeLessThanCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            try
            {
                ListBuilderExtensions.WhereSection(listBuilderImpl, 10, 10);
            }
            catch (Exception e)
            {
                Assert.That(e.Message.Contains("start"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhereSectionEndMustBeLessThanCapacity()
        {
            using (mocks.Record())
                listBuilderImpl.Expect(x => x.Capacity).Return(10);

            ListBuilderExtensions.WhereSection(listBuilderImpl, 9, 10);
        }

        [Test]
        public void WhereTheLastRangeShouldBeTheCapacityMinusTheRangeSizeAndOneLessThanTheCapacity()
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
                var declaration = ListBuilderExtensions.WhereTheLast(listBuilderImpl, 10);
                Assert.That(declaration, Is.SameAs(rangeDeclaration));
            }
        }

        [Test]
        public void AndTheNextShouldReturnRangeDeclaration()
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
                var andTheNextDeclaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.AndTheNext(listBuilderImpl, 10);

                Assert.That(andTheNextDeclaration.Start, Is.EqualTo(10));
                Assert.That(andTheNextDeclaration.End, Is.EqualTo(19));
            }
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void ShouldOnlyAddTheDeclarationIfTheRangeIsValid()
        {
            IDeclarationQueue<MyClass> declarationQueue = mocks.StrictMock<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 9);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(listSize);
                listBuilderImpl.Expect(x => x.CreateObjectBuilder()).Return(null);
                declarationQueue.Expect(x => x.GetLastItem()).Return(rangeDeclaration);
                listBuilderImpl.Expect(x => x.Declarations).Return(declarationQueue);
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RangeDeclaration<MyClass>>.Is.TypeOf)).Throw(new BuilderException(""));
            }

            using (mocks.Playback())
            {
                ListBuilderExtensions.AndTheNext(listBuilderImpl, 30);
            }
        }

        [Test]
        public void ShouldBeAbleToUseAndThePrevious()
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
                var andTheNextDeclaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.AndThePrevious(listBuilderImpl, 10);

                Assert.That(andTheNextDeclaration.Start, Is.EqualTo(0));
                Assert.That(andTheNextDeclaration.End, Is.EqualTo(9));
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
                var whereSection = (RangeDeclaration<MyClass>)ListBuilderExtensions.WhereSection(listBuilderImpl, 10, 19);

                Assert.That(whereSection.Start, Is.EqualTo(10));
                Assert.That(whereSection.End, Is.EqualTo(19));                
            }
        }

        [Test]
        public void WhereRandomShouldReturnRandomDeclarationOfRangeOfWholeList()
        {
            const int amount = 5;
            const int end = listSize - 1;

            var randomDeclaration = new RandomDeclaration<MyClass>(listBuilderImpl, null, null, amount, 0, end);

            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Capacity).Return(listSize).Repeat.Any();
                listBuilderImpl.Expect(x => x.AddDeclaration(Arg<RandomDeclaration<MyClass>>.Matches(y => y.Start == 0 && y.End == end))).Return(randomDeclaration);
            }

            IDeclaration<MyClass> declaration;
            using (mocks.Playback())
            {
                declaration = (IDeclaration<MyClass>)ListBuilderExtensions.WhereRandom(listBuilderImpl, amount);
            }

            Assert.That(declaration.Start, Is.EqualTo(0));
            Assert.That(declaration.End, Is.EqualTo(end));
        }

        [Test]
        public void WhereRandomCanReturnDeclarationForASectionOfTheList()
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

            
            var declaration = (IDeclaration<MyClass>)ListBuilderExtensions.WhereRandom(listBuilderImpl, amount, start, end);

            Assert.That(declaration.Start, Is.EqualTo(start));
            Assert.That(declaration.End, Is.EqualTo(end));
        }
        
        // TODO: Finish
        //[Test]
        //public void ShouldBeAbleToUseBuildHierarchy()
        //{
        //    var hierarchySpec = mocks.Stub<IHierarchySpec<MyClass>>();

        //    using (mocks.Record())
        //    {
        //        listBuilderImpl.Expect(x => x.Build()).Return(new List<MyClass>()).Repeat.Any();
        //    }

        //    using (mocks.Playback())
        //    {
                
        //        var list = ListBuilderExtensions.BuildHierarchy(listBuilderImpl, hierarchySpec);

        //        Assert.That(list, Is.TypeOf(typeof(List<MyClass>)));

        //   }
        //}
    }
    // ReSharper restore InvokeAsExtensionMethod
}