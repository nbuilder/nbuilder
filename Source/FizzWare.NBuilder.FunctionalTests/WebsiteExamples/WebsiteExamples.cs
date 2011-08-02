using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Dates;
using FizzWare.NBuilder.FunctionalTests.Extensibility;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.FunctionalTests.WebsiteExamples
{
    public class Product
    {
        public Product(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }

        public void AddToCategory(Category category)
        {
        }
    }

    // I want to use this file as a sanity check to make sure the syntax of the examples on the website is valid
    // Not actually tests, I just want to make sure they compile.
    //
    public class WebsiteExamples
    {
        public void HomePage_1()
        {
            var products = Builder<Product>.CreateListOfSize(10)
                               .TheFirst(2)
                                   .With(x => x.Title = "special title")
                               .Build();
        }

        public void HomePage_2()
        {
            var products = Builder<Product>.CreateListOfSize(10)
                               .All()
                                   .WithConstructor(() => new Product("my title"))
                               .Random(5)
                                   .WithConstructor(() => new Product("my other title"))
                                   .And(x => x.Created = August.The17th.At(09, 00))
                               .Build();
        }

        public void HomePage_3()
        {
            var generator = new UniqueRandomGenerator();

            var products = Builder<Product>.CreateListOfSize(10)
                                           .TheFirst(2)
                                               .With(x => x.Title = "special title 1")
                                               .And(x => x.Description = "special description 1")
                                           .TheNext(3)
                                               .With(x => x.Title = "special title 2")
                                           .TheNext(5)
                                               .With(x => x.Title = "special title 3")
                                               .And(x => x.Price = generator.Next(0m, 10m))
                                           .Build();
        }

        public void Building_single_objects_1()
        {
            var product = Builder<Product>.CreateNew().Build();
        }

        public void Building_single_objects_2()
        {
            var product = Builder<Product>
                                .CreateNew()
                                    .With(x => x.Description = "A custom description here")
                                .Build();
        }

        public void Building_single_objects_3()
        {
            var product = Builder<Product>
                            .CreateNew()
                                .With(x => x.Title = "Special title")
                                .And(x => x.Description = "Special description")
                                .And(x => x.Id = 2)
                            .Build();
        }

        public void Building_single_objects_4()
        {
            var basket = new ShoppingBasket();
            var product = new Model.Product();
            var quantity = 1;

            var basketItem = Builder<BasketItem>
                    .CreateNew()
                        .WithConstructor(() => new BasketItem(basket, product, quantity))
                    .Build();
        }

        public void Building_single_objects_5()
        {
            var child = new Category();
            var category = Builder<Category>
                .CreateNew()
                    .Do(x => x.AddChild(child))
                .Build();
        }

        public void Building_single_objects_6()
        {
            var child = new Category();
            var category = Builder<Category>
                .CreateNew()
                    .Do(x => x.AddChild(child))
                .Build();
        }

        public void Building_single_objects_7()
        {
            var child = new Category();
            var anotherChild = new Category();
            var category = Builder<Category>
                    .CreateNew()
                        .Do(x => x.AddChild(child))
                        .And(x => x.AddChild(anotherChild))
                    .Build();
        }

        public void Building_single_objects_8()
        {
            var categories = Builder<Category>.CreateListOfSize(5).Build();

            var product = Builder<Product>
                .CreateNew()
                    .DoForAll((prod, cat) => prod.AddToCategory(cat), categories)
                .Build();
        }

        public void Lists_1()
        {
            var products = Builder<Product>.CreateListOfSize(10).Build();
        }

        public void Lists_2()
        {
            var products = Builder<Product>
                .CreateListOfSize(10)
                .All()
                    .With(x => x.Title = "A special title")
                .Build();
        }

        public void Lists_3()
        {
            var products = Builder<Product>
                .CreateListOfSize(10)
                .TheFirst(2)
                    .With(x => x.Title = "A special title")
                .Build();
        }

        public void Lists_4()
        {
            var list = Builder<Product>
                        .CreateListOfSize(30)
                        .TheFirst(10)
                            .With(x => x.Title = "Special Title 1")
                        .TheNext(10)
                            .With(x => x.Title = "Special Title 2")
                        .TheNext(10)
                            .With(x => x.Title = "Special Title 3")
                        .Build();    
        }

        public void Lists_5()
        {
            var list = Builder<Product>
                    .CreateListOfSize(30)
                    .TheLast(10)
                        .With(x => x.Title = "Special Title 1")
                    .ThePrevious(10)
                        .With(x => x.Title = "Special Title 2")
                    .Build();
        }

        public void Lists_6()
        {
            var list = Builder<Product>
                    .CreateListOfSize(30)
                    .All()
                        .With(x => x.Title = "Special Title 1")
                    .Section(12, 14)
                        .With(x => x.Title = "Special Title 2")
                    .Section(16, 18)
                        .With(x => x.Title = "Special Title 3")
                    .Build();
        }

        public void Lists_7()
        {
            var children = Builder<Category>.CreateListOfSize(3).Build();

            var list = Builder<Category>
                .CreateListOfSize(10)
                .TheFirst(2)
                    .Do(x => x.AddChild(children[0]))
                    .And(x => x.AddChild(children[1]))
                .TheNext(2)
                    .Do(x => x.AddChild(children[2]))
                .Build();
        }

        public void Lists_8()
        {
            var children = Builder<Category>.CreateListOfSize(10).Build();

            var categories = Builder<Category>
                .CreateListOfSize(10)
                .TheFirst(2)
                            .Do(x => x.AddChild(Pick<Category>.RandomItemFrom(children)))
                .Build();
        }

        public void Lists_9()
        {
            var products = Builder<Product>.CreateListOfSize(5).Build();
            Pick<Product>.UniqueRandomList(With.Between(5).And(10).Elements).From(products);
        }

        public void Persistence_1_and_2()
        {
            // Not identical, but enough to know they're ok
            var productRepository = new ProductRepository();
            BuilderSetup.SetCreatePersistenceMethod<Model.Product>(productRepository.Create);
            BuilderSetup.SetCreatePersistenceMethod<IList<Model.Product>>(productRepository.CreateAll);

            Builder<Product>.CreateNew().Persist();
        }

        public void Dates_1()
        {
            The.Year(2006).On.May.The10th.At(09, 00);

            On.July.The21st.At(07, 00);

            var aug11 = August.The11th;

            July.The(1);

            November.The(10);

            The.Year(2008).On.January.The10th.At(05, 49, 38);

            var created = On.May.The14th;

            Today.At(09, 00);

            // (These generate TimeSpans)
            The.Time(08, 31);
            At.Time(09, 46);
        }

        public void Dates_2()
        {
            var generator = new RandomGenerator();

            var products = Builder<Product>
                .CreateListOfSize(100)
                .All()
                    .With(x => x.Created = generator.Next(July.The(1), November.The(10)))
                .Build();
        }

        public void Hierarchies_1()
        {
            var hierarchySpec = new HierarchySpec<Category>
            {
                AddMethod = (parent, child) => parent.AddChild(child),
                Depth = 3,
                MinimumChildren = 3,
                MaximumChildren = 8,
                NamingMethod = (item, index) => item.Title = "Category " + index,
                NumberOfRoots = 5
            };

            var categories = Builder<Category>.CreateListOfSize(10000)
                                                    .PersistHierarchy(hierarchySpec);
        }

        public void Configuration_1()
        {
            BuilderSetup.SetPersistenceService(new MyCustomPersistenceService());
        }

        public void Configuration_2()
        {
            var namer = new RandomValuePropertyNamer(new RandomGenerator(),
                                            new ReflectionUtil(),
                                            true,
                                            DateTime.Now,
                                            DateTime.Now.AddDays(10),
                                            true);

            BuilderSetup.SetDefaultPropertyNamer(namer);
        }

        public void Configuration_3()
        {
            BuilderSetup.SetPropertyNamerFor<Product>(new CustomProductPropertyNamer(new ReflectionUtil()));
        }

        public void Configuration_4()
        {
            BuilderSetup.DisablePropertyNamingFor<Product, int>(x => x.Id);
        }

        private class MyCustomPersistenceService : IPersistenceService
        {
            public void Create<T>(T obj)
            {
                throw new NotImplementedException();
            }

            public void Create<T>(IList<T> obj)
            {
                throw new NotImplementedException();
            }

            public void Update<T>(T obj)
            {
                throw new NotImplementedException();
            }

            public void Update<T>(IList<T> obj)
            {
                throw new NotImplementedException();
            }

            public void SetPersistenceCreateMethod<T>(Action<T> saveMethod)
            {
                throw new NotImplementedException();
            }

            public void SetPersistenceUpdateMethod<T>(Action<T> saveMethod)
            {
                throw new NotImplementedException();
            }
        }
    }

    
}