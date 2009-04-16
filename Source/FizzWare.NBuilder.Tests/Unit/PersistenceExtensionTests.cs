using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    [TestFixture]
    public class PersistenceExtensionTests
    {
        private MockRepository mocks;
        private IPersistenceService persistenceService;
        private IOperable<MyClass> operable;
        private IListBuilderImpl<MyClass> listBuilderImpl;
        private ISingleObjectBuilder<MyClass> singleObjectBuilder;
        private IList<MyClass> theList;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            persistenceService = mocks.DynamicMock<IPersistenceService>();
            listBuilderImpl = mocks.DynamicMock<IListBuilderImpl<MyClass>>();
            operable = mocks.DynamicMultiMock<IOperable<MyClass>>(typeof(IDeclaration<MyClass>));
            singleObjectBuilder = mocks.DynamicMultiMock<ISingleObjectBuilder<MyClass>>();

            theList = new List<MyClass>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToPersistUsingSingleObjectBuilder()
        {
            var obj = new MyClass();

            using (mocks.Record())
            {
                singleObjectBuilder.Expect(x => x.Build()).Return(obj);
                persistenceService.Expect(x => x.Persist(obj));
            }

            BuilderSetup.RegisterPersistenceService(persistenceService);

            using (mocks.Playback())
            {
                PersistenceExtensions.Persist(singleObjectBuilder);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistUsingListBuilder()
        {
            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Build()).Return(theList);
                persistenceService.Expect(x => x.Persist(theList));
            }

            BuilderSetup.RegisterPersistenceService(persistenceService);

            using (mocks.Playback())
            {
                PersistenceExtensions.Persist(listBuilderImpl);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistFromADeclaration()
        {
            using (mocks.Record())
            {
                listBuilderImpl.Expect(x => x.Build()).Return(theList);
                persistenceService.Expect(x => x.Persist(theList));
                ((IDeclaration<MyClass>) operable).Expect(x => x.ListBuilderImpl).Return(listBuilderImpl);
            }

            BuilderSetup.RegisterPersistenceService(persistenceService);

            using (mocks.Playback())
            {
                PersistenceExtensions.Persist(operable);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldComplainIfOperableIsNotAlsoOfTypeIDeclaration()
        {
            var operableOnly = mocks.DynamicMock<IOperable<MyClass>>();

            using (mocks.Record())
            {}

            using (mocks.Playback())
            {
                PersistenceExtensions.Persist(operableOnly);
            }
        }
    }
    // ReSharper restore InvokeAsExtensionMethod
}