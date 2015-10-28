using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RangeDeclarationTests
    {
        private Declaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private IListBuilderImpl<SimpleClass> listBuilderImpl;
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            listBuilderImpl = mocks.DynamicMock<IListBuilderImpl<SimpleClass>>();
            objectBuilder = mocks.StrictMock<IObjectBuilder<SimpleClass>>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void DeclarationShouldUseObjectBuilderToConstructItems()
        {
          
            using (mocks.Record())
            {
                listBuilderImpl.Stub(x => x.BuilderSetup).Return(new BuilderSetup());
                objectBuilder.Stub(x => x.BuilderSetup).Return(new BuilderSetup());

                objectBuilder.Expect(x => x.Construct(Arg<int>.Is.Anything)).Return(new SimpleClass()).Repeat.Times(10);
            }

            using (mocks.Playback())
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

            using (mocks.Record())
            {
                listBuilderImpl.Stub(x => x.BuilderSetup).Return(new BuilderSetup());
                objectBuilder.Stub(x => x.BuilderSetup).Return(new BuilderSetup());
                objectBuilder.Expect(x => x.Construct(9)).Return(obj1);
                objectBuilder.Expect(x => x.Construct(10)).Return(obj2);
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
            IList<SimpleClass> masterList = mocks.StrictMock<IList<SimpleClass>>();

            using (mocks.Record())
            {
                masterList.Expect(x => x[4]).Return(null);
                masterList.Expect(x => x[5]).Return(null);

                objectBuilder.Expect(x => x.CallFunctions(null, 0)).IgnoreArguments().Repeat.Times(2);
            }

            using (mocks.Playback())
            {
                declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 11);
                declaration.MasterListAffectedIndexes = new List<int> { 4, 5 };
                declaration.CallFunctions(masterList);
            }
        }

        [Test]
        public void ShouldBeAbleToUseAll()
        {
          
            using (mocks.Record())
            {
                listBuilderImpl.Stub(x => x.BuilderSetup).Return(new BuilderSetup());
                objectBuilder.Stub(x => x.BuilderSetup).Return(new BuilderSetup());

                listBuilderImpl.Expect(x => x.All()).Return(declaration);
            }

            using (mocks.Playback())
            {
                declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 10);

                declaration.All();
            }
        }

        [Test]
        public void ShouldRecordMasterListKeys()
        {
            SimpleClass[] masterList = new SimpleClass[19];

            using (mocks.Record())
                objectBuilder.Expect(x => x.Construct(Arg<int>.Is.Anything)).Return(new SimpleClass()).Repeat.Times(2);

            declaration = new RangeDeclaration<SimpleClass>(listBuilderImpl, objectBuilder, 9, 10);
            declaration.Construct();
            
            declaration.AddToMaster(masterList);

            Assert.That(declaration.MasterListAffectedIndexes.Count, Is.EqualTo(2));
            Assert.That(declaration.MasterListAffectedIndexes[0], Is.EqualTo(9));
            Assert.That(declaration.MasterListAffectedIndexes[1], Is.EqualTo(10));
         
        }
    }
}