using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class GlobalDeclarationTests
    {
        private IGlobalDeclaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private IListBuilderImpl<SimpleClass> listBuilderImpl;
        private MockRepository mocks;

        BuilderSetup builderSetup;
        [SetUp]
        public void SetUp()
        {
            builderSetup = new BuilderSetup();
            mocks = new MockRepository();
            listBuilderImpl = mocks.DynamicMock<IListBuilderImpl<SimpleClass>>();
            objectBuilder = mocks.StrictMock<IObjectBuilder<SimpleClass>>();
            listBuilderImpl.Stub(x => x.Capacity).Return(2);
            listBuilderImpl.Stub(x => x.BuilderSetup).Return(builderSetup);
            objectBuilder.Stub(x => x.BuilderSetup).Return(builderSetup).Repeat.Any(); ;
         }
            
        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToConstructItems()
        {
            using (mocks.Record())
            {
                objectBuilder.Stub(x => x.BuilderSetup).Return(builderSetup);
                objectBuilder.Expect(x => x.Construct(Arg<int>.Is.Anything)).Return(new SimpleClass()).Repeat.Times(2);
            }

            using (mocks.Playback())
            {
                declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);

                declaration.Construct();
            }
        }

        [Test]
        public void ShouldAddToMasterList()
        {
            var masterList = new SimpleClass[2];

            var obj1 = new SimpleClass();
            var obj2 = new SimpleClass();

            using (mocks.Record())
            {

                objectBuilder.Stub(x => x.BuilderSetup).Return(builderSetup);
                objectBuilder.Expect(x => x.Construct(0)).Return(obj1);
                objectBuilder.Expect(x => x.Construct(1)).Return(obj2);
            }

            using (mocks.Playback())
            {
                declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);

                declaration.Construct();
                declaration.AddToMaster(masterList);
            }

            Assert.That(masterList[0], Is.SameAs(obj1));
            Assert.That(masterList[1], Is.SameAs(obj2));
        }

        [Test]
        public void ShouldRecordMasterListKeys()
        {
          
            SimpleClass[] masterList = new SimpleClass[19];

            using (mocks.Record())
            {

                objectBuilder.Stub(x => x.BuilderSetup).Return(builderSetup);
                objectBuilder.Expect(x => x.Construct(Arg<int>.Is.Anything)).Return(new SimpleClass()).Repeat.Times(2);
            }
            using (mocks.Playback())
            {
                declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);
                declaration.Construct();

                declaration.AddToMaster(masterList);
            }
            Assert.That(declaration.MasterListAffectedIndexes.Count, Is.EqualTo(2));
            Assert.That(declaration.MasterListAffectedIndexes[0], Is.EqualTo(0));
            Assert.That(declaration.MasterListAffectedIndexes[1], Is.EqualTo(1));
        }
    }
}