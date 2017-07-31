using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Xunit;
using Arg = NSubstitute.Arg;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    
    public class ListBuilderExtensionsTests
    {
        private const int listSize = 30;
        private IListBuilderImpl<MyClass> listBuilderImpl;

        public ListBuilderExtensionsTests()
        {
            listBuilderImpl = Substitute.For<IListBuilderImpl<MyClass>>();
        }

        [Fact]
        public void TheFirstShouldReturnARangeDeclaration()
        {
            var rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 0);

            listBuilderImpl.Capacity.Returns(30);
            listBuilderImpl.CreateObjectBuilder().Returns((IObjectBuilder<MyClass>)null);
            listBuilderImpl.AddDeclaration(Arg.Is<RangeDeclaration<MyClass>>(y => y.Start == 0 && y.End == 9)).Returns(rangeDeclaration);

            var declaration = ListBuilderExtensions.TheFirst(listBuilderImpl, 10);
            declaration.ShouldBeSameAs(rangeDeclaration);

        }


        [Fact]
        public void TheFirstAmountMustBeOneOrGreater()
        {
            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.TheFirst(listBuilderImpl, 0);
            });
        }

        [Fact]
        public void TheFirstAmountShouldBeLessThanListCapacity()
        {
            listBuilderImpl.Capacity.Returns(10);

            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.TheFirst(listBuilderImpl, 11);
            });
        }

        [Fact]
        public void TheLastAmountMustBeOneOrGreater()
        {
            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.TheLast(listBuilderImpl, 0);
            });
        }

        [Fact]
        public void TheLastAmountShouldBeLessThanListCapacity()
        {
            listBuilderImpl.Capacity.Returns(10);

            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.TheLast(listBuilderImpl, 11);
            });
        }

        [Fact]
        public void RandomAmountMustBeOneOrGreater()
        {
            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.Random(listBuilderImpl, 0);
            });
        }

        [Fact]
        public void RandomAmountShouldBeLessThanListCapacity()
        {
            listBuilderImpl.Capacity.Returns(10);

            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.Random(listBuilderImpl, 11);
            });
        }

        [Fact]
        public void SectionStartMustBeGreaterThanZero()
        {
            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.Section(listBuilderImpl, -1, 10);
            });
        }

        [Fact]
        public void SectionEndMustBeGreaterThanOne()
        {
            listBuilderImpl.Capacity.Returns(10);

            // TODO FIX
            Should.Throw<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 0, 0));

        }

        [Fact]
        public void SectionStartMustBeLessThanEnd()
        {

            listBuilderImpl.Capacity.Returns(10);

            // TODO FIX
            Should.Throw<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 6, 5));

        }

        [Fact]
        public void SectionStartCannotEqualEnd()
        {

            listBuilderImpl.Capacity.Returns(10);

            Should.Throw<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 5, 5));

        }

        private class MyDeclaration<T> : IDeclaration<T>, IOperable<T>
        {
            public void Construct()
            {
            }

            public void CallFunctions(IList<T> masterList)
            {
                throw new NotImplementedException();
            }

            public void AddToMaster(T[] masterList)
            {
                throw new NotImplementedException();
            }

            public int NumberOfAffectedItems { get; }
            public IList<int> MasterListAffectedIndexes { get; }
            public int Start { get; }
            public int End { get; }
            public IListBuilderImpl<T> ListBuilderImpl { get; }
            public IObjectBuilder<T> ObjectBuilder { get; }
            public BuilderSettings BuilderSettings { get; set; }
            public IList<T> Build()
            {
                throw new NotImplementedException();
            }

            public IOperable<T> All()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void SectionCanCoverWholeList()
        {
            listBuilderImpl.Capacity.Returns(10);
            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.AddDeclaration(Arg.Any<IDeclaration<MyClass>>())
                .Returns(new MyDeclaration<MyClass>());

            ListBuilderExtensions.Section(listBuilderImpl, 0, 9);
        }

        [Fact]
        public void TheNextAmountShouldBeGreaterThanOne()
        {

            listBuilderImpl.Capacity.Returns(10);

            Should.Throw<ArgumentException>(() =>
            {
                ListBuilderExtensions.TheNext(listBuilderImpl, 0);
            });
        }

        [Fact]
        public void SectionStartMustBeLessThanCapacity()
        {

            listBuilderImpl.Capacity.Returns(10);

            var ex = Should.Throw<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 10, 10));

            ex.Message.Contains("start");
        }

        [Fact]
        public void SectionEndMustBeLessThanCapacity()
        {
            listBuilderImpl.Capacity.Returns(10);

            Should.Throw<ArgumentException>(
                () => ListBuilderExtensions.Section(listBuilderImpl, 9, 10));
        }

        [Fact]
        public void LastRangeShouldBeTheCapacityMinusTheRangeSizeAndOneLessThanTheCapacity()
        {
            const int rangeSize = 10;
            const int startIndex = listSize - rangeSize; // 20
            const int endIndex = listSize - 1; // 29

            var rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 0);


            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.Capacity.Returns(listSize);
            listBuilderImpl.CreateObjectBuilder().Returns((IObjectBuilder<MyClass>)null);
            listBuilderImpl.AddDeclaration(Arg.Is<RangeDeclaration<MyClass>>(y => y.Start == startIndex && y.End == endIndex)).Returns(rangeDeclaration);

            var declaration = ListBuilderExtensions.TheLast(listBuilderImpl, 10);
            declaration.ShouldBeSameAs(rangeDeclaration);

        }

        [Fact]
        public void TheNextShouldReturnRangeDeclaration()
        {
            IDeclarationQueue<MyClass> declarationQueue = Substitute.For<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 9);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.CreateObjectBuilder().Returns((IObjectBuilder<MyClass>)null);
            declarationQueue.GetLastItem().Returns(rangeDeclaration);
            listBuilderImpl.Declarations.Returns(declarationQueue);
            listBuilderImpl.AddDeclaration(Arg.Any<RangeDeclaration<MyClass>>()).Returns((IDeclaration<MyClass>)null);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            var andTheNextDeclaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.TheNext(listBuilderImpl, 10);

            andTheNextDeclaration.Start.ShouldBe(10);
            andTheNextDeclaration.End.ShouldBe(19);

        }

        [Fact]
        public void ShouldOnlyAddTheDeclarationIfTheRangeIsValid()
        {
            IDeclarationQueue<MyClass> declarationQueue = Substitute.For<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 0, 9);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.CreateObjectBuilder().Returns((IObjectBuilder<MyClass>)null);
            declarationQueue.GetLastItem().Returns(rangeDeclaration);
            listBuilderImpl.Declarations.Returns(declarationQueue);
            listBuilderImpl.AddDeclaration(Arg.Any<RangeDeclaration<MyClass>>()).Throws(new BuilderException(""));
            Should.Throw<BuilderException>(
                () => ListBuilderExtensions.TheNext(listBuilderImpl, 30)
            );
        }

        [Fact]
        public void ShouldBeAbleToUseThePrevious()
        {
            IDeclarationQueue<MyClass> declarationQueue = Substitute.For<IDeclarationQueue<MyClass>>();
            RangeDeclaration<MyClass> rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 10, 19);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.CreateObjectBuilder().Returns((IObjectBuilder<MyClass>)null);
            declarationQueue.GetLastItem().Returns(rangeDeclaration);
            listBuilderImpl.Declarations.Returns(declarationQueue);
            listBuilderImpl.AddDeclaration(Arg.Any<RangeDeclaration<MyClass>>()).Returns((IDeclaration<MyClass>)null);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            var thePreviousDeclaration = (RangeDeclaration<MyClass>)ListBuilderExtensions.ThePrevious(listBuilderImpl, 10);

            thePreviousDeclaration.Start.ShouldBe(0);
            thePreviousDeclaration.End.ShouldBe(9);
        }

        [Fact]
        public void ShouldBeAbleToUseSection()
        {
            var rangeDeclaration = new RangeDeclaration<MyClass>(listBuilderImpl, null, 10, 19);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.Capacity.Returns(20);
            listBuilderImpl.CreateObjectBuilder().Returns((IObjectBuilder<MyClass>)null);
            listBuilderImpl.AddDeclaration(Arg.Is<RangeDeclaration<MyClass>>(y => y.Start == 10 && y.End == 19)).Returns(rangeDeclaration);

            var whereSection = (RangeDeclaration<MyClass>)ListBuilderExtensions.Section(listBuilderImpl, 10, 19);

            whereSection.Start.ShouldBe(10);
            whereSection.End.ShouldBe(19);

        }

        [Fact]
        public void RandomShouldReturnRandomDeclarationOfRangeOfWholeList()
        {
            const int amount = 5;
            const int end = listSize;

            var randomDeclaration = new RandomDeclaration<MyClass>(listBuilderImpl, null, null, amount, 0, end);

            listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
            listBuilderImpl.Capacity.Returns(listSize);
            listBuilderImpl.AddDeclaration(Arg.Is<RandomDeclaration<MyClass>>(y => y.Start == 0 && y.End == end)).Returns(randomDeclaration);

            IDeclaration<MyClass> declaration;
            declaration = (IDeclaration<MyClass>)ListBuilderExtensions.Random(listBuilderImpl, amount);

            declaration.Start.ShouldBe(0);
            declaration.End.ShouldBe(end);
        }

        [Fact]
        public void RandomCanReturnDeclarationForASectionOfTheList()
        {
            const int amount = 5;
            const int start = 10;
            const int end = 20;

            var randomDeclaration = new RandomDeclaration<MyClass>(listBuilderImpl, null, null, amount, start, end);


            listBuilderImpl.Capacity.Returns(listSize);
            listBuilderImpl.AddDeclaration(Arg.Is<RandomDeclaration<MyClass>>(y => y.Start == start && y.End == end)).Returns(randomDeclaration);



            var declaration = (IDeclaration<MyClass>)ListBuilderExtensions.Random(listBuilderImpl, amount, start, end);

            declaration.Start.ShouldBe(start);
            declaration.End.ShouldBe(end);
        }

        [Fact]
        public void ShouldBeAbleToUseBuildHierarchy()
        {
            var hierarchySpec = Substitute.For<IHierarchySpec<MyClass>>();

            listBuilderImpl.Build().Returns(new List<MyClass>());


            var list = ListBuilderExtensions.BuildHierarchy(listBuilderImpl, hierarchySpec);

            list.ShouldBeOfType<List<MyClass>>();
        }

        [Fact]
        public void ShouldBeAbleToUsePersistHierarchy()
        {
            var buildersetup = new BuilderSettings();
            var hierarchySpec = Substitute.For<IHierarchySpec<MyClass>>();
            var persistenceService = Substitute.For<IPersistenceService>();


            listBuilderImpl.BuilderSettings.Returns(buildersetup);
            listBuilderImpl.Build().Returns(new List<MyClass>());
            persistenceService.Create(Arg.Any<MyClass>());
            persistenceService.Update(Arg.Any<IList<MyClass>>());

            buildersetup.SetPersistenceService(persistenceService);
            ListBuilderExtensions.PersistHierarchy(listBuilderImpl, hierarchySpec);
        }

    }
    // ReSharper restore InvokeAsExtensionMethod
}