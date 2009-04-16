using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using Rhino.Mocks;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class PersistenceServiceTests
    {
        private MockRepository mocks;
        private IMyClassRepository repository;
        private IMyClassRepository repository2;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            repository = mocks.DynamicMock<IMyClassRepository>();
            repository2 = mocks.DynamicMock<IMyClassRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToPersistAnObject()
        {
            var obj = new MyClass();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceMethod<MyClass>(x => repository.Save(x));

            using (mocks.Record())
            {
                repository.Expect(x => x.Save(obj));
            }

            using (mocks.Playback())
            {
                persistenceService.Persist(obj);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistAList()
        {
            IList<MyClass> list = new List<MyClass>();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceMethod<IList<MyClass>>(x => repository.SaveAll(x));

            using (mocks.Record())
            {
                repository.Expect(x => x.SaveAll(list));
            }

            using (mocks.Playback())
            {
                persistenceService.Persist(list);
            }
        }

        [Test]
        public void ShouldReplaceExistingPersister()
        {
            var obj = new MyClass();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceMethod<MyClass>(x => repository.Save(x));
            persistenceService.SetPersistenceMethod<MyClass>(x => repository2.Save(x));

            using (mocks.Record())
            {
                repository2.Expect(x => x.Save(obj));
            }

            using (mocks.Playback())
            {
                persistenceService.Persist(obj);
            }
        }
    }
}