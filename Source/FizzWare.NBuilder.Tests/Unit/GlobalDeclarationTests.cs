using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class GlobalDeclarationTests
    {
        private IGlobalDeclaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private IListBuilderImpl<SimpleClass> listBuilderImpl;

        BuilderSettings builderSettings;
        [SetUp]
        public void SetUp()
        {
            builderSettings = new BuilderSettings();
            listBuilderImpl = Substitute.For<IListBuilderImpl<SimpleClass>>();
            objectBuilder = Substitute.For<IObjectBuilder<SimpleClass>>();
            listBuilderImpl.Capacity.Returns(2);
            listBuilderImpl.BuilderSettings.Returns(builderSettings);
            objectBuilder.BuilderSettings.Returns(builderSettings);
        }

        [Test]
        public void ShouldBeAbleToConstructItems()
        {
            objectBuilder.BuilderSettings.Returns(builderSettings);
            objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());
            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);

            declaration.Construct();
        }

        [Test]
        public void ShouldAddToMasterList()
        {
            var masterList = new SimpleClass[2];

            var obj1 = new SimpleClass();
            var obj2 = new SimpleClass();


            objectBuilder.BuilderSettings.Returns(builderSettings);
            objectBuilder.Construct(0).Returns(obj1);
            objectBuilder.Construct(1).Returns(obj2);
            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);

            declaration.Construct();
            declaration.AddToMaster(masterList);

            Assert.That(masterList[0], Is.SameAs(obj1));
            Assert.That(masterList[1], Is.SameAs(obj2));
        }

        [Test]
        public void ShouldRecordMasterListKeys()
        {

            SimpleClass[] masterList = new SimpleClass[19];


            objectBuilder.BuilderSettings.Returns(builderSettings);
            objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());

            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);
            declaration.Construct();

            declaration.AddToMaster(masterList);
            Assert.That(declaration.MasterListAffectedIndexes.Count, Is.EqualTo(2));
            Assert.That(declaration.MasterListAffectedIndexes[0], Is.EqualTo(0));
            Assert.That(declaration.MasterListAffectedIndexes[1], Is.EqualTo(1));
        }
    }
}