using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;

using NSubstitute;
using Shouldly;
using Xunit;
using Arg = NSubstitute.Arg;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class RangeDeclarationTests
    {
        private Declaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private IListBuilderImpl<SimpleClass> listBuilderImpl;

        public RangeDeclarationTests()
        {
            listBuilderImpl = Substitute.For<IListBuilderImpl<SimpleClass>>();
            objectBuilder = Substitute.For<IObjectBuilder<SimpleClass>>();
        }

        [Fact]
        public void DeclarationShouldUseObjectBuilderToConstructItems()
        {

            {
                listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
                objectBuilder.BuilderSettings.Returns(new BuilderSettings());

                objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());
            }

            {
                declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 0, 9);

                declaration.Construct();
            }
        }

        [Fact]
        public void DeclarationShouldAddToMasterListInCorrectPlace()
        {
            SimpleClass[] masterList = new SimpleClass[19];
            var obj1 = new SimpleClass();
            var obj2 = new SimpleClass();

            {
                listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
                objectBuilder.BuilderSettings.Returns(new BuilderSettings());
                objectBuilder.Construct(9).Returns(obj1);
                objectBuilder.Construct(10).Returns(obj2);
            }

            declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 10);
            declaration.Construct();
            declaration.AddToMaster(masterList);

            masterList[9].ShouldBeSameAs(obj1);
            masterList[10].ShouldBeSameAs(obj2);
        }

        [Fact]
        public void ShouldCallFunctionsOnItemsInTheMasterList()
        {
            IList<SimpleClass> masterList = Substitute.For<IList<SimpleClass>>();

            {
                masterList[4].Returns((SimpleClass)null);
                masterList[5].Returns((SimpleClass)null);

            }

            {
                declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 11);
                declaration.MasterListAffectedIndexes = new List<int> { 4, 5 };
                declaration.CallFunctions(masterList);
            }

            objectBuilder.Received().CallFunctions(null, 0);

        }

        [Fact]
        public void ShouldBeAbleToUseAll()
        {

            {
                listBuilderImpl.BuilderSettings.Returns(new BuilderSettings());
                objectBuilder.BuilderSettings.Returns(new BuilderSettings());
                listBuilderImpl.All().Returns(declaration);

                declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 10);

                declaration.All();
            }
        }

        [Fact]
        public void ShouldRecordMasterListKeys()
        {
            SimpleClass[] masterList = new SimpleClass[19];

            objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());

            declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 10);
            declaration.Construct();

            declaration.AddToMaster(masterList);

            declaration.MasterListAffectedIndexes.Count.ShouldBe(2);
            declaration.MasterListAffectedIndexes[0].ShouldBe(9);
            declaration.MasterListAffectedIndexes[1].ShouldBe(10);

        }
    }
}