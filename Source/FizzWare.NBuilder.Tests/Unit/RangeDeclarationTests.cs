using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;
using Arg = NSubstitute.Arg;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RangeDeclarationTests
    {
        private Declaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private IListBuilderImpl<SimpleClass> listBuilderImpl;

        [SetUp]
        public void SetUp()
        {
            listBuilderImpl = Substitute.For<IListBuilderImpl<SimpleClass>>();
            objectBuilder = Substitute.For<IObjectBuilder<SimpleClass>>();
        }

        [Test]
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

        [Test]
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

            Assert.That(masterList[9], Is.SameAs(obj1));
            Assert.That(masterList[10], Is.SameAs(obj2));
        }

        [Test]
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

        [Test]
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

        [Test]
        public void ShouldRecordMasterListKeys()
        {
            SimpleClass[] masterList = new SimpleClass[19];

            objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());

            declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 10);
            declaration.Construct();

            declaration.AddToMaster(masterList);

            Assert.That(declaration.MasterListAffectedIndexes.Count, Is.EqualTo(2));
            Assert.That(declaration.MasterListAffectedIndexes[0], Is.EqualTo(9));
            Assert.That(declaration.MasterListAffectedIndexes[1], Is.EqualTo(10));

        }
    }
}