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
        public void ShouldBeAbleToPersistAnObject_Create()
        {
            var obj = new MyClass();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository.Save(x));

            using (mocks.Record())
            {
                repository.Expect(x => x.Save(obj));
            }

            using (mocks.Playback())
            {
                persistenceService.Create(obj);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistAList_Create()
        {
            IList<MyClass> list = new List<MyClass>();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<IList<MyClass>>(x => repository.SaveAll(x));

            using (mocks.Record())
            {
                repository.Expect(x => x.SaveAll(list));
            }

            using (mocks.Playback())
            {
                persistenceService.Create(list);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistAnObject_Update()
        {
            var obj = new MyClass();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<MyClass>(x => repository.Save(x));

            using (mocks.Record())
            {
                repository.Expect(x => x.Save(obj));
            }

            using (mocks.Playback())
            {
                persistenceService.Update(obj);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistAList_Update()
        {
            IList<MyClass> list = new List<MyClass>();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<IList<MyClass>>(x => repository.SaveAll(x));

            using (mocks.Record())
            {
                repository.Expect(x => x.SaveAll(list));
            }

            using (mocks.Playback())
            {
                persistenceService.Update(list);
            }
        }

        [Test]
        public void ShouldReplaceExistingCreatePersister()
        {
            var obj = new MyClass();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository.Save(x));
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository2.Save(x));

            using (mocks.Record())
            {
                repository2.Expect(x => x.Save(obj));
            }

            using (mocks.Playback())
            {
                persistenceService.Create(obj);
            }
        }

        [Test]
        public void ShouldReplaceExistingUpdatePersister()
        {
            var obj = new MyClass();

            PersistenceService persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<MyClass>(x => repository.Save(x));
            persistenceService.SetPersistenceUpdateMethod<MyClass>(x => repository2.Save(x));

            using (mocks.Record())
            {
                repository2.Expect(x => x.Save(obj));
            }

            using (mocks.Playback())
            {
                persistenceService.Update(obj);
            }
        }

        [Test]
        [ExpectedException(typeof(PersistenceMethodNotFoundException))]
        public void ShouldComplainIfNoCreatePersistenceServiceFound()
        {
            using (mocks.Record()) { }

            using (mocks.Playback())
            {
                PersistenceService persistenceService = new PersistenceService();
                persistenceService.Create(new MyClass());
            }
        }

        [Test]
        [ExpectedException(typeof(PersistenceMethodNotFoundException))]
        public void ShouldComplainIfNoUpdatePersistenceServiceFound()
        {
            using (mocks.Record()) { }

            using (mocks.Playback())
            {
                PersistenceService persistenceService = new PersistenceService();
                persistenceService.Update(new MyClass());
            }
        }

        [Test]
        [ExpectedException(typeof(PersistenceMethodNotFoundException))]
        public void ShouldComplainIfNoCreatePersistenceServiceFoundForList()
        {
            using (mocks.Record()) { }

            using (mocks.Playback())
            {
                PersistenceService persistenceService = new PersistenceService();
                persistenceService.Create((IList<MyClass>)new List<MyClass>());
            }
        }

        [Test]
        [ExpectedException(typeof(PersistenceMethodNotFoundException))]
        public void ShouldComplainIfNoUpdatePersistenceServiceFoundForList()
        {
            using (mocks.Record()) { }

            using (mocks.Playback())
            {
                PersistenceService persistenceService = new PersistenceService();
                persistenceService.Update((IList<MyClass>)new List<MyClass>());
            }
        }
    }
}