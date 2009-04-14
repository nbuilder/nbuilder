using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class GlobalDeclarationTests
    {
        private IGlobalDeclaration<SimpleClass> declaration;
        private IObjectBuilder<SimpleClass> objectBuilder;
        private Implementation.IListBuilderImpl<SimpleClass> listBuilderImpl;
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            listBuilderImpl = mocks.DynamicMock<Implementation.IListBuilderImpl<SimpleClass>>();
            objectBuilder = mocks.StrictMock<IObjectBuilder<SimpleClass>>();
            listBuilderImpl.Stub(x => x.Capacity).Return(2);

            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);
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
                objectBuilder.Expect(x => x.Construct()).Return(new SimpleClass()).Repeat.Times(2);
            }

            using (mocks.Playback())
            {
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
                objectBuilder.Expect(x => x.Construct()).Return(obj1);
                objectBuilder.Expect(x => x.Construct()).Return(obj2);
            }

            using (mocks.Playback())
            {
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
                objectBuilder.Expect(x => x.Construct()).Return(new SimpleClass()).Repeat.Times(2);

            declaration = new GlobalDeclaration<SimpleClass>(listBuilderImpl, objectBuilder);
            declaration.Construct();

            declaration.AddToMaster(masterList);

            Assert.That(declaration.MasterListAffectedIndexes, Has.Count(2));
            Assert.That(declaration.MasterListAffectedIndexes[0], Is.EqualTo(0));
            Assert.That(declaration.MasterListAffectedIndexes[1], Is.EqualTo(1));
        }
    }
}