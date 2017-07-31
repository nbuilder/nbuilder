using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class GlobalDeclarationTests
    {
        private IGlobalDeclaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private IListBuilderImpl<SimpleClass> listBuilderImpl;

        BuilderSettings builderSettings;
        public GlobalDeclarationTests()
        {
            builderSettings = new BuilderSettings();
            listBuilderImpl = Substitute.For<IListBuilderImpl<SimpleClass>>();
            objectBuilder = Substitute.For<IObjectBuilder<SimpleClass>>();
            listBuilderImpl.Capacity.Returns(2);
            listBuilderImpl.BuilderSettings.Returns(builderSettings);
            objectBuilder.BuilderSettings.Returns(builderSettings);
        }

        [Fact]
        public void ShouldBeAbleToConstructItems()
        {
            objectBuilder.BuilderSettings.Returns(builderSettings);
            objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());
            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);

            declaration.Construct();
        }

        [Fact]
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

            masterList[0].ShouldBeSameAs(obj1);
            masterList[1].ShouldBeSameAs(obj2);
        }

        [Fact]
        public void ShouldRecordMasterListKeys()
        {

            SimpleClass[] masterList = new SimpleClass[19];


            objectBuilder.BuilderSettings.Returns(builderSettings);
            objectBuilder.Construct(Arg.Any<int>()).Returns(new SimpleClass());

            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);
            declaration.Construct();

            declaration.AddToMaster(masterList);
            declaration.MasterListAffectedIndexes.Count.ShouldBe(2);
            declaration.MasterListAffectedIndexes[0].ShouldBe(0);
            declaration.MasterListAffectedIndexes[1].ShouldBe(1);
        }
    }
}