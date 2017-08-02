using System.Collections.Generic;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class PersistenceServiceTests
    {
        public PersistenceServiceTests()
        {
            repository = Substitute.For<IMyClassRepository>();
            repository2 = Substitute.For<IMyClassRepository>();
        }

        private IMyClassRepository repository;
        private IMyClassRepository repository2;

        [Fact]
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

        [Fact]
        public void ShouldBeAbleToPersistAList_Update()
        {
            IList<MyClass> list = new List<MyClass>();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceUpdateMethod<IList<MyClass>>(x => repository.SaveAll(x));
            repository.SaveAll(list);
            persistenceService.Update(list);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public void ShouldComplainIfNoCreatePersistenceServiceFound()
        {

            {
                var persistenceService = new PersistenceService();

                Should.Throw<PersistenceMethodNotFoundException>(() => { persistenceService.Create(new MyClass()); });
            }
        }

        [Fact]
        public void ShouldComplainIfNoCreatePersistenceServiceFoundForList()
        {

            {
                var persistenceService = new PersistenceService();
                Should.Throw<PersistenceMethodNotFoundException>(
                    () => { persistenceService.Create((IList<MyClass>) new List<MyClass>()); });
            }
        }

        [Fact]
        public void ShouldComplainIfNoUpdatePersistenceServiceFound()
        {


            {
                var persistenceService = new PersistenceService();
                Should.Throw<PersistenceMethodNotFoundException>(() => { persistenceService.Update(new MyClass()); });
            }
        }

        [Fact]
        public void ShouldComplainIfNoUpdatePersistenceServiceFoundForList()
        {

            {
                var persistenceService = new PersistenceService();
                Should.Throw<PersistenceMethodNotFoundException>(
                    () => { persistenceService.Update((IList<MyClass>) new List<MyClass>()); });
            }
        }

        [Fact]
        public void ShouldReplaceExistingCreatePersister()
        {
            var obj = new MyClass();

            var persistenceService = new PersistenceService();
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository.Save(x));
            persistenceService.SetPersistenceCreateMethod<MyClass>(x => repository2.Save(x));
            repository2.Save(obj);
            persistenceService.Create(obj);
        }

        [Fact]
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