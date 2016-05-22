using System;
using System.Data;
using System.Linq;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Support;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    /// <remarks>
    /// To run these tests, create a local database named 'NBuilderTests'
    /// </remarks>
    [TestFixture]
    public class ListBuilderTests
    {
        /// <summary>
        /// Tests the fixture set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Need to call this explicitly here to overcome a bug in resharper's test runner
            new RepositoryBuilderSetup().SetUp();
        }

        

        [Test]
        public void CreatingAList()
        {
            var builderSetup = new BuilderSetup();
            var products = new Builder<Product>(builderSetup).CreateListOfSize(10).Build();

            // Note: The asserts here are intentionally verbose to show how NBuilder works

            // It sets strings to their name plus their 1-based sequence number
            Assert.That(products[0].Title, Is.EqualTo("Title1"));
            Assert.That(products[1].Title, Is.EqualTo("Title2"));
            Assert.That(products[2].Title, Is.EqualTo("Title3"));
            Assert.That(products[3].Title, Is.EqualTo("Title4"));
            Assert.That(products[4].Title, Is.EqualTo("Title5"));
            Assert.That(products[5].Title, Is.EqualTo("Title6"));
            Assert.That(products[6].Title, Is.EqualTo("Title7"));
            Assert.That(products[7].Title, Is.EqualTo("Title8"));
            Assert.That(products[8].Title, Is.EqualTo("Title9"));
            Assert.That(products[9].Title, Is.EqualTo("Title10"));

            // Ints are set to their 1-based sequence number
            Assert.That(products[0].Id, Is.EqualTo(1));
            // ... 2, 3, 4, 5, 6, 7, 8 ...
            Assert.That(products[9].Id, Is.EqualTo(10));

            // Any other numeric types are set to their 1-based sequence number
            Assert.That(products[0].PriceBeforeTax, Is.EqualTo(1m));
            // ... 2m, 3m, 4m, 5m, 6m, 7m, 8m ...
            Assert.That(products[9].PriceBeforeTax, Is.EqualTo(10m));
        }

        public void UsingAllToSetValues()
        {
            var builderSetup = new BuilderSetup();
            var products = new Builder<Product>(builderSetup)
                .CreateListOfSize(10)
                .All()
                    .With(x => x.Title = "A special title")
                .Build();

            for (int i = 0; i < products.Count; i++)
                Assert.That(products[i].Title, Is.EqualTo("A special title"));
        }

        [Test]
        public void SettingTheValueOfAProperty()
        {
            var builderSetup = new BuilderSetup();
            var products = new Builder<Product>(builderSetup)
                .CreateListOfSize(10)
                .TheFirst(2)
                    .With(x => x.Title = "A special title")
                .Build();

            Assert.That(products[0].Title, Is.EqualTo("A special title"));
            Assert.That(products[1].Title, Is.EqualTo("A special title"));

            // After that the naming would carry on
            Assert.That(products[2].Title, Is.EqualTo("Title3"));
            Assert.That(products[3].Title, Is.EqualTo("Title4"));
            // ...4, 5, 6, 7, 8...
            Assert.That(products[9].Title, Is.EqualTo("Title10"));
        }

        [Test]
        public void UsingSingularSyntaxInstead()
        {
            var builderSetup = new BuilderSetup();
            var products = new Builder<Product>(builderSetup)
                            .CreateListOfSize(10)
                            .TheFirst(1)
                                .Has(x => x.Title = "Special title 1")
                            .Build();

            Assert.That(products[0].Title, Is.EqualTo("Special title 1"));
        }

        [Test]
        public void UsingAGenerator()
        {
            var builderSetup = new BuilderSetup();
            var generator = new RandomGenerator();

            var products = new Builder<Product>(builderSetup)
                .CreateListOfSize(10)
                .All()
                    .With(x => x.PriceBeforeTax = generator.Next(50, 1000))
                .Build();


            foreach (var product in products)
            {
                Assert.That(product.PriceBeforeTax, Is.AtLeast(50m));
                Assert.That(product.PriceBeforeTax, Is.AtMost(1000m));
            }
        }

        [Test]
        public void DeclaringThatARandomNumberOfElementsShouldHaveCertainProperties()
        {
            var builderSetup = new BuilderSetup();
            const string specialdescription = "SpecialDescription";
            const decimal specialPrice = 10m;
            var products = new Builder<Product>(builderSetup).CreateListOfSize(10)
                .Random(5)
                    .With(x => x.Description = specialdescription)
                    .And(x => x.PriceBeforeTax = specialPrice)
                .Build();

            int count = 0;

            foreach (var product in products)
            {
                if (product.Description == specialdescription &&
                    product.PriceBeforeTax == specialPrice)
                {
                    count++;
                }
            }

            Assert.That(count, Is.EqualTo(5));
        }

        [Test]
        public void CreatingAListOfProductsAndAddingThemToCategories()
        {
            var builderSetup = new BuilderSetup();
            var categories = new Builder<Category>(builderSetup).CreateListOfSize(50).Build();

            var products = new Builder<Product>(builderSetup)
                            .CreateListOfSize(500)
                            .All()
                                .With(x => x.Categories = Pick<Category>.UniqueRandomList(With.Between(5, 10).Elements).From(categories).ToList())
                            .Build();

            foreach (var product in products)
            {
                Assert.That(product.Categories.Count, Is.AtLeast(5));
                Assert.That(product.Categories.Count, Is.AtMost(10));
            }
        }

        [Test]
        public void UsingTheSequentialGenerator()
        {
            var builderSetup = new BuilderSetup();
            var generator = new SequentialGenerator<int> { Direction = GeneratorDirection.Descending, Increment = 2 };
            generator.StartingWith(6);

            var products = new Builder<Product>(builderSetup)
                .CreateListOfSize(3)
                .All()
                    .With(x => x.Id = generator.Generate())
                .Build();

            Assert.That(products[0].Id, Is.EqualTo(6));
            Assert.That(products[1].Id, Is.EqualTo(4));
            Assert.That(products[2].Id, Is.EqualTo(2));
        }


        [Test]
        public void UsingSequentialGenerators()
        {
            var builderSetup = new BuilderSetup();
            // Arrange
            var decimalGenerator = new SequentialGenerator<decimal>
            {
                Increment = 10,
                Direction = GeneratorDirection.Descending
            };

            decimalGenerator.StartingWith(2000);

            var intGenerator = new SequentialGenerator<int> { Increment = 10000 };

            // Act
            var list = new Builder<Product>(builderSetup).CreateListOfSize(3)
                .All()
                    .With(x => x.PriceBeforeTax = decimalGenerator.Generate())
                    .And(x => x.Id = intGenerator.Generate())
                .Build();

            // Assert
            Assert.That(list[0].PriceBeforeTax, Is.EqualTo(2000));
            Assert.That(list[1].PriceBeforeTax, Is.EqualTo(1990));
            Assert.That(list[2].PriceBeforeTax, Is.EqualTo(1980));

            Assert.That(list[0].Id, Is.EqualTo(0));
            Assert.That(list[1].Id, Is.EqualTo(10000));
            Assert.That(list[2].Id, Is.EqualTo(20000));
        }

        [Test]
        public void SequentialGenerator_DateTimeGeneration()
        {
            var builderSetup = new BuilderSetup();
            const int increment = 2;
            var dateTimeGenerator = new SequentialGenerator<DateTime>
            {
                IncrementDateBy = IncrementDate.Day,
                IncrementDateValueBy = increment
            };

            var startingDate = DateTime.MinValue;

            dateTimeGenerator.StartingWith(startingDate);


            var list = new Builder<Product>(builderSetup).CreateListOfSize(2)
                .All()
                    .With(x => x.Created = dateTimeGenerator.Generate())
                .Build();

            Assert.That(list[0].Created, Is.EqualTo(startingDate));
            Assert.That(list[1].Created, Is.EqualTo(startingDate.AddDays(increment)));
        }

        [Test]
        public void UsingTheWithBetween_And_SyntaxForGreaterReadability()
        {
            var builderSetup  = new BuilderSetup();
            var categories = new Builder<Category>(builderSetup).CreateListOfSize(50).Build();

            var products = new Builder<Product>(builderSetup)
                            .CreateListOfSize(500)
                            .All()
                                .With(x => x.Categories = Pick<Category>.UniqueRandomList(With.Between(5).And(10).Elements).From(categories).ToList())
                            .Build();

            foreach (var product in products)
            {
                Assert.That(product.Categories.Count, Is.AtLeast(5));
                Assert.That(product.Categories.Count, Is.AtMost(10));
            }
        }
        
        [Test]
        public void CreatingAListOfATypeWithAConstructor()
        {
            var builderSetup = new BuilderSetup();
            // ctor: BasketItem(ShoppingBasket basket, Product product, int quantity)

            var basket = new Builder<ShoppingBasket>(builderSetup).CreateNew().Build();
            var product = new Builder<Product>(builderSetup).CreateNew().Build();
            const int quantity = 5;

            var basketItems =
               new Builder<BasketItem>(builderSetup).CreateListOfSize(10)
                    .All()
                        .AreConstructedWith(basket, product, quantity) // passes these arguments to the constructor
                    .Build();

            foreach (var basketItem in basketItems)
            {
                Assert.That(basketItem.Basket, Is.EqualTo(basket));
                Assert.That(basketItem.Product, Is.EqualTo(product));
                Assert.That(basketItem.Quantity, Is.EqualTo(quantity));
            }
        }

        [Test]
        public void DifferentPartsOfTheListCanBeConstructedDifferently()
        {
            var builderSetup = new BuilderSetup();
            var basket1 = new ShoppingBasket();
            var product1 = new Product();
            const int quantity1 = 5;

            var basket2 = new ShoppingBasket();
            var product2 = new Product();
            const int quantity2 = 7;

            var items = new Builder<BasketItem>(builderSetup)
                .CreateListOfSize(4)
                .TheFirst(2)
                    .AreConstructedWith(basket1, product1, quantity1)
                .TheNext(2)
                    .AreConstructedWith(basket2, product2, quantity2)
                .Build();

            Assert.That(items[0].Basket, Is.EqualTo(basket1));
            Assert.That(items[0].Basket, Is.EqualTo(basket1));
            Assert.That(items[0].Basket, Is.EqualTo(basket1));
            Assert.That(items[0].Basket, Is.EqualTo(basket1));
            Assert.That(items[0].Basket, Is.EqualTo(basket1));
            Assert.That(items[0].Basket, Is.EqualTo(basket1));
        }

        [Test]
        public void WillComplainIfYouTryToBuildAClassThatCannotBeInstantiatedDirectly()
        {
            Assert.Throws<BuilderException>(() =>
            {
                var builderSetup = new BuilderSetup();
                new Builder<ChuckNorris>(builderSetup).CreateListOfSize(10).Build();
            });
        }

        [Test]
        public void UsingWith_WithImmutableClassProperties()
        {
            var builderSetup = new BuilderSetup();
            var invoices = new Builder<Invoice>(builderSetup)
                .CreateListOfSize(2)
                .TheFirst(2)
                    .With(p => p.Amount, 100)
                .Build();

            Assert.That(invoices[0].Amount, Is.EqualTo(100));
            Assert.That(invoices[1].Amount, Is.EqualTo(100));
        }

        [Test]
        public void UsingWith_WithAnIndex()
        {
            var builderSetup = new BuilderSetup();
            var invoices = new Builder<Product>(builderSetup)
                .CreateListOfSize(2)
                .TheFirst(2)
                    .With((p, idx) => p.Description = "Description" + (idx + 5))
                .Build();

            Assert.That(invoices[0].Description, Is.EqualTo("Description5"));
            Assert.That(invoices[1].Description, Is.EqualTo("Description6"));
        }

        [Test]
        public void UsingAndWithAnIndex()
        {
            var builderSetup = new BuilderSetup();
            var invoices = new Builder<Product>(builderSetup)
                .CreateListOfSize(2)
                .TheFirst(2)
                    .With(p => p.Title = "Title")
                    .And((p, idx) => p.Description = "Description" + (idx + 5))
                .Build();

            Assert.That(invoices[0].Title, Is.EqualTo("Title"));
            Assert.That(invoices[0].Description, Is.EqualTo("Description5"));
            Assert.That(invoices[1].Title, Is.EqualTo("Title"));
            Assert.That(invoices[1].Description, Is.EqualTo("Description6"));
        }

        [Test]
        public void UsingAndWithImmutableClassProperties()
        {
            var builderSetup = new BuilderSetup();
            var invoices = new Builder<Invoice>(builderSetup)
                .CreateListOfSize(2)
                .TheFirst(2)
                    .With(p => p.Amount, 100)
                    .And(p => p.Id, 200)
                .Build();

            Assert.That(invoices[0].Amount, Is.EqualTo(100));
            Assert.That(invoices[1].Amount, Is.EqualTo(100));
            Assert.That(invoices[0].Id, Is.EqualTo(200));
            Assert.That(invoices[1].Id, Is.EqualTo(200));
        }

        [Test]
        public void UsingHasWithImmutableClassProperties()
        {
            var builderSetup = new BuilderSetup();
            var invoices = new Builder<Invoice>(builderSetup)
                .CreateListOfSize(1)
                .TheFirst(1)
                    .Has(p => p.Amount, 100)
                .Build();

            Assert.That(invoices[0].Amount, Is.EqualTo(100));
        }

        [Test]
        public void WillComplainIfYouDoNotSupplyArgsMatchingOneOfTheConstructors()
        {
            var builderSetup = new BuilderSetup();

            Assert.Throws<TypeCreationException>(() =>
            {
                new Builder<BasketItem>(builderSetup)
                     .CreateListOfSize(10)
                     .All()
                     .AreConstructedWith().Build();
            });
        }

        [Test]
        public void ChainingDeclarationsTogether()
        {
            var builderSetup = new BuilderSetup();
            var list = new Builder<Product>(builderSetup)
                .CreateListOfSize(30)
                .TheFirst(10)
                    .With(x => x.Title = "Special Title 1")
                .TheNext(10)
                    .With(x => x.Title = "Special Title 2")
                .TheNext(10)
                    .With(x => x.Title = "Special Title 3")
                .Build();

            Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[9].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[10].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[19].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[20].Title, Is.EqualTo("Special Title 3"));
            Assert.That(list[29].Title, Is.EqualTo("Special Title 3"));
        }

        ////[Test]
        ////public void UsingAndTheRemaining()
        ////{ 
        ////    var list = Builder<Product>
        ////        .CreateListOfSize(4)
        ////        .TheFirst(2)
        ////            .With(x => x.Title = "Special Title 1")
        ////        .TheRemainder()
        ////            .With(x => x.Title = "Special Title 2")
        ////        .Build();

        ////    Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
        ////    Assert.That(list[1].Title, Is.EqualTo("Special Title 1"));
        ////    Assert.That(list[2].Title, Is.EqualTo("Special Title 2"));
        ////    Assert.That(list[3].Title, Is.EqualTo("Special Title 2"));
        ////}

        [Test]
        public void UsingAndThePrevious()
        {
            var builderSetup = new BuilderSetup();
            var list = new Builder<Product>(builderSetup)
                .CreateListOfSize(30)
                .TheLast(10)
                    .With(x => x.Title = "Special Title 1")
                .ThePrevious(10)
                    .With(x => x.Title = "Special Title 2")
                .Build();

            Assert.That(list[10].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[19].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[20].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[29].Title, Is.EqualTo("Special Title 1"));
        }

        [Test]
        public void UsingSection()
        {
            var builderSetup = new BuilderSetup();
            var list = new Builder<Product>(builderSetup)
                .CreateListOfSize(30)
                .All()
                    .With(x => x.Title = "Special Title 1")
                .Section(12, 14)
                    .With(x => x.Title = "Special Title 2")
                .Section(16, 18)
                    .With(x => x.Title = "Special Title 3")
                .Build();

            // All
            Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[1].Title, Is.EqualTo("Special Title 1"));
            // ...
            Assert.That(list[29].Title, Is.EqualTo("Special Title 1"));

            // Section 1 - 12 - 14
            Assert.That(list[12].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[13].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[14].Title, Is.EqualTo("Special Title 2"));

            // Section 2 - 16 - 18
            Assert.That(list[16].Title, Is.EqualTo("Special Title 3"));
            Assert.That(list[17].Title, Is.EqualTo("Special Title 3"));
            Assert.That(list[18].Title, Is.EqualTo("Special Title 3"));
        }

        [Test]
        public void UsingSectionAndTheNext()
        {
            var builderSetup = new BuilderSetup();
            var list = new Builder<Product>(builderSetup)
                .CreateListOfSize(30)
                .All()
                    .With(x => x.Title = "Special Title 1")
                .Section(12, 14)
                    .With(x => x.Title = "Special Title 2")
                .TheNext(2)
                    .With(x => x.Title = "Special Title 3")
                .Build();

            Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[15].Title, Is.EqualTo("Special Title 3"));
        }

        [Test]
        [Description("You can use Do to do something to all the items in the declaration")]
        public void UsingDo()
        {
            var builderSetup = new BuilderSetup();
            var children = new Builder<Category>(builderSetup).CreateListOfSize(3).Build();

            var categories = new Builder<Category>(builderSetup)
                .CreateListOfSize(10)
                .TheFirst(2)
                    .Do(x => x.AddChild(children[0]))
                    .And(x => x.AddChild(children[1]))
                .TheNext(2)
                    .Do(x => x.AddChild(children[2]))
                .Build();
                
            Assert.That(categories[0].Children[0], Is.EqualTo(children[0]));
            Assert.That(categories[0].Children[1], Is.EqualTo(children[1]));
            Assert.That(categories[1].Children[0], Is.EqualTo(children[0]));
            Assert.That(categories[1].Children[1], Is.EqualTo(children[1]));
            Assert.That(categories[2].Children[0], Is.EqualTo(children[2]));
            Assert.That(categories[3].Children[0], Is.EqualTo(children[2]));
        }

        [Test]
        public void UsingDoAndPickTogether()
        {
            var builderSetup = new BuilderSetup();
            var children = new Builder<Category>(builderSetup).CreateListOfSize(10).Build();

            var categories = new Builder<Category>(builderSetup)
                .CreateListOfSize(10)
                .TheFirst(2)
                    .Do(x => x.AddChild(Pick<Category>.RandomItemFrom(children)))
                .Build();

            Assert.That(categories[0].Children.Count, Is.EqualTo(1));
            Assert.That(categories[1].Children.Count, Is.EqualTo(1));
        }

        [Test]
        [Description("Multi functions allow you to call a method on an object on each item in a list")]
        public void UsingMultiFunctions()
        {
            var builderSetup = new BuilderSetup();
            var categories = new Builder<Category>(builderSetup).CreateListOfSize(5).Build();

            // Here we are saying, add every product to all of the categories

            var products = new Builder<Product>(builderSetup)
                .CreateListOfSize(10)
                .All().DoForEach((product, category) => product.AddToCategory(category), categories)
                .Build();

            // Assertions are intentionally verbose for clarity
            foreach (var product in products)
            {
                Assert.That(product.Categories.Count, Is.EqualTo(5));
                Assert.That(product.Categories[0], Is.EqualTo(categories[0]));
                Assert.That(product.Categories[1], Is.EqualTo(categories[1]));
                Assert.That(product.Categories[2], Is.EqualTo(categories[2]));
                Assert.That(product.Categories[3], Is.EqualTo(categories[3]));
                Assert.That(product.Categories[4], Is.EqualTo(categories[4]));
            }
        }

        [Test]
        public void UsingRandomGenerator()
        {
            var builderSetup = new BuilderSetup();
            var generator = new RandomGenerator();

            var list = new Builder<Product>(builderSetup).CreateListOfSize(3)
                .All()
                .With(x => x.QuantityInStock = generator.Next(1000, 2000))
                .Build();

            Assert.That(list[0].QuantityInStock, Is.AtLeast(1000));
            Assert.That(list[0].QuantityInStock, Is.AtMost(2000));

            Assert.That(list[1].QuantityInStock, Is.AtLeast(1000));
            Assert.That(list[1].QuantityInStock, Is.AtMost(2000));

            Assert.That(list[2].QuantityInStock, Is.AtLeast(1000));
            Assert.That(list[2].QuantityInStock, Is.AtMost(2000));
        }

        [Test]
        public void WillNotLetYouDoThingsThatDoNotMakeSense()
        {
            var builderSetup = new BuilderSetup();

            Assert.Throws<BuilderException>(() =>
            {
                new Builder<Product>(builderSetup)
                    .CreateListOfSize(10)
                    .TheFirst(5)
                        .With(x => x.Title = "titleone")
                    .TheNext(10)
                        .With(x => x.Title = "titletwo")
                    .Build();

            });
        }

        [Test]
        public void SupportsStructsButDoesNotSupportAutomaticallyNamingTheProperties()
        {
            var builderSetup = new BuilderSetup();
            var locations = new Builder<WarehouseLocation>(builderSetup)
                .CreateListOfSize(10)
                .Build();

            Assert.That(locations.Count, Is.EqualTo(10));
            Assert.That(locations[0].Location, Is.EqualTo(0));
            Assert.That(locations[1].Location, Is.EqualTo(0));
        }

        [Test]
        public void StructsCanHavePropertyAssignments()
        {
            var builderSetup = new BuilderSetup();
            var locations = new Builder<WarehouseLocation>(builderSetup)
                .CreateListOfSize(10)
                .Section(5, 6)
                .WithConstructor(() => new WarehouseLocation('A', 1, 2))
                .Build();

            Assert.That(locations[5].Aisle, Is.EqualTo('A'));
            Assert.That(locations[6].Aisle, Is.EqualTo('A'));

            Assert.That(locations[5].Shelf, Is.EqualTo(1));
            Assert.That(locations[6].Shelf, Is.EqualTo(1));

            Assert.That(locations[5].Location, Is.EqualTo(2));
            Assert.That(locations[6].Location, Is.EqualTo(2));
        }

        [Test]
        public void AutomaticPropertyAndPublicFieldNamingCanBeSwitchedOff()
        {
            var builderSetup = new BuilderSetup();
            builderSetup.AutoNameProperties = false;

            var products = new Builder<Product>(builderSetup).CreateListOfSize(10).Build();

            Assert.That(products[0].Title, Is.Null);
            Assert.That(products[9].Title, Is.Null);

            Assert.That(products[0].Id, Is.EqualTo(0));
            Assert.That(products[9].Id, Is.EqualTo(0));
        }

        [Test]
        public void ItIsPossibleToSwitchOffAutomaticPropertyNamingForASinglePropertyOfASpecificType()
        {
            var builderSetup = new BuilderSetup();
            builderSetup.DisablePropertyNamingFor<Product, int>(x => x.Id);

            var products = new Builder<Product>(builderSetup).CreateListOfSize(10).Build();

            // The Product.Id will always be its default value
            Assert.That(products[0].Id, Is.EqualTo(0));
            Assert.That(products[9].Id, Is.EqualTo(0));

            // Other properties are still given automatic values as normal
            Assert.That(products[0].QuantityInStock, Is.EqualTo(1));
            Assert.That(products[9].QuantityInStock, Is.EqualTo(10));
        }

        [Test]
        public void SpecifyingADifferentDefaultPropertyNamer()
        {

            var builderSetup = new BuilderSetup();
            builderSetup.SetDefaultPropertyNamer(new RandomValuePropertyNamer(new RandomGenerator(), new ReflectionUtil(), true, DateTime.Now, DateTime.Now.AddDays(10), true,new BuilderSetup()));

            var products = new Builder<Product>(builderSetup).CreateListOfSize(10).Build();

            Assert.That(products[0].Title, Is.Not.EqualTo("StringOne1"));
            Assert.That(products[9].Title, Is.Not.EqualTo("StringOne10"));
        }
    }
}