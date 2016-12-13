using System.Collections.Generic;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class PersistenceServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            repository = Substitute.For<IMyClassRepository>();
            repository2 = Substitute.For<IMyClassRepository>();
        }

        private IMyClassRepository repository;
        private IMyClassRepository repository2;

        [Test]
        public void ShouldBeAbleToPersistAList_Create()
        {
            IList<MyClass> list = new List<MyClass>();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<IList<MyClass>>(x => repository.SaveAll(x));


            {
                repository.SaveAll(list);
            }


            {
                persistenceService.Create(list);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistAList_Update()
        {
            IList<MyClass> list = new List<MyClass>();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<IList<MyClass>>(x => repository.SaveAll(x));
            repository.SaveAll(list);
            persistenceService.Update(list);
        }

        [Test]
        public void ShouldBeAbleToPersistAnObject_Create()
        {
            var obj = new MyClass();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository.Save(x));


            {
                repository.Save(obj);
            }


            {
                persistenceService.Create(obj);
            }
        }

        [Test]
        public void ShouldBeAbleToPersistAnObject_Update()
        {
            var obj = new MyClass();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<MyClass>(x => repository.Save(x));


            {
                repository.Save(obj);
            }


            {
                persistenceService.Update(obj);
            }
        }

        [Test]
        public void ShouldComplainIfNoCreatePersistenceServiceFound()
        {

            {
                var persistenceService = new PersistenceService();

                Assert.Throws<PersistenceMethodNotFoundException>(() => { persistenceService.Create(new MyClass()); });
            }
        }

        [Test]
        public void ShouldComplainIfNoCreatePersistenceServiceFoundForList()
        {

            {
                var persistenceService = new PersistenceService();
                Assert.Throws<PersistenceMethodNotFoundException>(
                    () => { persistenceService.Create((IList<MyClass>) new List<MyClass>()); });
            }
        }

        [Test]
        public void ShouldComplainIfNoUpdatePersistenceServiceFound()
        {


            {
                var persistenceService = new PersistenceService();
                Assert.Throws<PersistenceMethodNotFoundException>(() => { persistenceService.Update(new MyClass()); });
            }
        }

        [Test]
        public void ShouldComplainIfNoUpdatePersistenceServiceFoundForList()
        {

            {
                var persistenceService = new PersistenceService();
                Assert.Throws<PersistenceMethodNotFoundException>(
                    () => { persistenceService.Update((IList<MyClass>) new List<MyClass>()); });
            }
        }

        [Test]
        public void ShouldReplaceExistingCreatePersister()
        {
            var obj = new MyClass();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository.Save(x));
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository2.Save(x));
            repository2.Save(obj);
            persistenceService.Create(obj);
        }

        [Test]
        public void ShouldReplaceExistingUpdatePersister()
        {
            var obj = new MyClass();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<MyClass>(x => repository.Save(x));
            persistenceService.SetPersistenceUpdateMethod<MyClass>(x => repository2.Save(x));
            repository2.Save(obj);
            persistenceService.Update(obj);
        }
    }
}