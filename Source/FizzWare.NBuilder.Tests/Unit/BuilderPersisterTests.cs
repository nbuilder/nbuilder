using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class BuilderPersisterTests
    {
        private MockRepository mocks;
        private IMyClassRepository repository;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            repository = mocks.DynamicMock<IMyClassRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToAddASinglePersister()
        {
            using (mocks.Record())
            {
            }

            using (mocks.Playback())
            {
                Action<MyClass> func = x => repository.Save(x);

                PersistenceService persistenceService = new PersistenceService();
                persistenceService.SetPersistenceMethod(func);

                Assert.That(persistenceService.Persisters, Has.Count(1));
            }
        }

        [Test]
        public void ShouldBeAbleToAddAListPersister()
        {
            using (mocks.Record())
            {
            }

            using (mocks.Playback())
            {
                Action<IList<MyClass>> func = x => repository.SaveAll(x);

                PersistenceService persistenceService = new PersistenceService();
                persistenceService.SetPersistenceMethod(func);

                Assert.That(persistenceService.Persisters, Has.Count(1));
            }
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
            persistenceService.SetPersistenceMethod<MyClass>(x => repository.SaveAll(x));

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
            using (mocks.Record())
            {

            }

            using (mocks.Playback())
            {
                PersistenceService persistenceService = new PersistenceService();
                persistenceService.SetPersistenceMethod<MyClass>(x => repository.SaveAll(x));

                persistenceService.SetPersistenceMethod<MyClass>(x => repository.SaveAll(x));
            }
        }
    }
}