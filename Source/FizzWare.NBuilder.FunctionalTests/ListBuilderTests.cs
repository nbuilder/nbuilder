using System;
using System.Data;
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
            new SetupFixture().SetUp();

            // Clear all the database tables
            Database.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            BuilderSetup.ResetToDefaults();
        }

        [Test]
        public void CreatingAList()
        {
            var products = Builder<Product>.CreateListOfSize(10).Build();

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

        public void UsingWhereAllToSetValues()
        {
            var products = Builder<Product>
                .CreateListOfSize(10)
                .WhereAll()
                    .Have(x => x.Title = "A special title")
                .Build();

            for (int i = 0; i < products.Count; i++)
                Assert.That(products[i].Title, Is.EqualTo("A special title"));
        }

        [Test]
        public void SettingTheValueOfAProperty()
        {
            var products = Builder<Product>
                .CreateListOfSize(10)
                .WhereTheFirst(2)
                    .Have(x => x.Title = "A special title")
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
            var products = Builder<Product>
                            .CreateListOfSize(10)
                            .WhereTheFirst(1)
                                .Has(x => x.Title = "Special title 1")
                            .Build();

            Assert.That(products[0].Title, Is.EqualTo("Special title 1"));
        }

        [Test]
        public void UsingAGenerator()
        {
            var generator = new RandomGenerator();

            var products = Builder<Product>
                .CreateListOfSize(10)
                .WhereAll()
                    .Have(x => x.PriceBeforeTax = generator.Next(50, 1000))
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
            const string specialdescription = "SpecialDescription";
            const decimal specialPrice = 10m;
            var products = Builder<Product>.CreateListOfSize(10)
                .WhereRandom(5)
                    .Have(x => x.Description = specialdescription)
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
            var categories = Builder<Category>.CreateListOfSize(50).Build();

            var products = Builder<Product>
                            .CreateListOfSize(500)
                            .WhereAll()
                                .Have(x => x.Categories = Pick<Category>.UniqueRandomList(With.Between(5, 10).Elements).From(categories))
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
            var generator = new SequentialGenerator<int> { Direction = GeneratorDirection.Descending, Increment = 2 };
            generator.StartingWith(6);

            var products = Builder<Product>
                .CreateListOfSize(3)
                .WhereAll()
                    .Have(x => x.Id = generator.Generate())
                .Build();

            Assert.That(products[0].Id, Is.EqualTo(6));
            Assert.That(products[1].Id, Is.EqualTo(4));
            Assert.That(products[2].Id, Is.EqualTo(2));
        }


        [Test]
        public void UsingSequentialGenerators()
        {
            var decimalGenerator = new SequentialGenerator<decimal>
            {
                Increment = 10,
                Direction = GeneratorDirection.Descending
            };

            decimalGenerator.StartingWith(2000);

            var intGenerator = new SequentialGenerator<int> { Increment = 10000 };

            var list = Builder<Product>.CreateListOfSize(3)
                .WhereAll()
                    .Have(x => x.PriceBeforeTax = decimalGenerator.Generate())
                    .And(x => x.Id = intGenerator.Generate())
                .Build();

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
            const int increment = 2;
            var dateTimeGenerator = new SequentialGenerator<DateTime>
            {
                IncrementDateBy = IncrementDate.Day,
                IncrementDateValueBy = increment
            };

            var startingDate = DateTime.MinValue;

            dateTimeGenerator.StartingWith(startingDate);


            var list = Builder<Product>.CreateListOfSize(2)
                .WhereAll()
                    .Have(x => x.Created = dateTimeGenerator.Generate())
                .Build();

            Assert.That(list[0].Created, Is.EqualTo(startingDate));
            Assert.That(list[1].Created, Is.EqualTo(startingDate.AddDays(increment)));
        }

        [Test]
        public void UsingTheWithBetween_And_SyntaxForGreaterReadability()
        {
            var categories = Builder<Category>.CreateListOfSize(50).Build();

            var products = Builder<Product>
                            .CreateListOfSize(500)
                            .WhereAll()
                                .Have(x => x.Categories = Pick<Category>.UniqueRandomList(With.Between(5).And(10).Elements).From(categories))
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
            // ctor: BasketItem(ShoppingBasket basket, Product product, int quantity)

            var basket = Builder<ShoppingBasket>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            const int quantity = 5;

            var basketItems =
                Builder<BasketItem>.CreateListOfSize(10)
                    .WhereAll()
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
            var basket1 = new ShoppingBasket();
            var product1 = new Product();
            const int quantity1 = 5;

            var basket2 = new ShoppingBasket();
            var product2 = new Product();
            const int quantity2 = 7;

            var items = Builder<BasketItem>
                .CreateListOfSize(4)
                .WhereTheFirst(2)
                    .AreConstructedWith(basket1, product1, quantity1)
                .AndTheNext(2)
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
        [ExpectedException(typeof(BuilderException))]
        public void WillComplainIfYouDoNotSupplyConstructorArgsWhenRequired()
        {
            Builder<Invoice>.CreateListOfSize(10).Build();
        }

        [Test]
        public void UsingHaveWithImmutableClassProperties()
        {
            var invoices = Builder<Invoice>
                .CreateListOfSize(2)
                .WhereTheFirst(2)
                    .Have(p => p.Amount, 100)
                .Build();

            Assert.That(invoices[0].Amount, Is.EqualTo(100));
            Assert.That(invoices[1].Amount, Is.EqualTo(100));
        }

        [Test]
        public void UsingAndWithImmutableClassProperties()
        {
            var invoices = Builder<Invoice>
                .CreateListOfSize(2)
                .WhereTheFirst(2)
                    .Have(p => p.Amount, 100)
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
            var invoices = Builder<Invoice>
                .CreateListOfSize(1)
                .WhereTheFirst(1)
                    .Has(p => p.Amount, 100)
                .Build();

            Assert.That(invoices[0].Amount, Is.EqualTo(100));
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void WillComplainIfYouDoNotSupplyArgsMatchingOneOfTheConstructors()
        {
            Builder<BasketItem>
                 .CreateListOfSize(10)
                 .WhereAll()
                 .AreConstructedWith().Build();
        }

        [Test]
        public void ChainingDeclarationsTogether()
        {
            var list = Builder<Product>
                .CreateListOfSize(30)
                .WhereTheFirst(10)
                    .Have(x => x.Title = "Special Title 1")
                .AndTheNext(10)
                    .Have(x => x.Title = "Special Title 2")
                .AndTheNext(10)
                    .Have(x => x.Title = "Special Title 3")
                .Build();

            Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[9].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[10].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[19].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[20].Title, Is.EqualTo("Special Title 3"));
            Assert.That(list[29].Title, Is.EqualTo("Special Title 3"));
        }

        [Test]
        public void UsingAndTheRemaining()
        { 
            var list = Builder<Product>
                .CreateListOfSize(4)
                .WhereTheFirst(2)
                    .Have(x => x.Title = "Special Title 1")
                .AndTheRemaining()
                    .Have(x => x.Title = "Special Title 2")
                .Build();

            Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[1].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[2].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[3].Title, Is.EqualTo("Special Title 2"));
        }

        [Test]
        public void UsingAndThePrevious()
        {
            var list = Builder<Product>
                .CreateListOfSize(30)
                .WhereTheLast(10)
                    .Have(x => x.Title = "Special Title 1")
                .AndThePrevious(10)
                    .Have(x => x.Title = "Special Title 2")
                .Build();

            Assert.That(list[10].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[19].Title, Is.EqualTo("Special Title 2"));
            Assert.That(list[20].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[29].Title, Is.EqualTo("Special Title 1"));
        }

        [Test]
        public void UsingWhereSection()
        {
            var list = Builder<Product>
                .CreateListOfSize(30)
                .WhereAll()
                    .Have(x => x.Title = "Special Title 1")
                .WhereSection(12, 14)
                    .Have(x => x.Title = "Special Title 2")
                .WhereSection(16, 18)
                    .Have(x => x.Title = "Special Title 3")
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
        public void UsingWhereSectionAndAndTheNext()
        {
            var list = Builder<Product>
                .CreateListOfSize(30)
                .WhereAll()
                    .Have(x => x.Title = "Special Title 1")
                .WhereSection(12, 14)
                    .Have(x => x.Title = "Special Title 2")
                .AndTheNext(2)
                    .Have(x => x.Title = "Special Title 3")
                .Build();

            Assert.That(list[0].Title, Is.EqualTo("Special Title 1"));
            Assert.That(list[15].Title, Is.EqualTo("Special Title 3"));
        }

        [Test]
        [Description("You can use HaveDoneToThemForAll to do something to all the items in the declaration")]
        public void UsingHaveDoneToThem()
        {
            var children = Builder<Category>.CreateListOfSize(3).Build();

            var categories = Builder<Category>
                .CreateListOfSize(10)
                .WhereTheFirst(2)
                    .HaveDoneToThem(x => x.AddChild(children[0]))
                    .And(x => x.AddChild(children[1]))
                .AndTheNext(2)
                    .HaveDoneToThem(x => x.AddChild(children[2]))
                .Build();
                
            Assert.That(categories[0].Children[0], Is.EqualTo(children[0]));
            Assert.That(categories[0].Children[1], Is.EqualTo(children[1]));
            Assert.That(categories[1].Children[0], Is.EqualTo(children[0]));
            Assert.That(categories[1].Children[1], Is.EqualTo(children[1]));
            Assert.That(categories[2].Children[0], Is.EqualTo(children[2]));
            Assert.That(categories[3].Children[0], Is.EqualTo(children[2]));
        }

        [Test]
        public void UsingHaveDoneToThemAndPickTogether()
        {
            var children = Builder<Category>.CreateListOfSize(10).Build();

            var categories = Builder<Category>
                .CreateListOfSize(10)
                .WhereTheFirst(2)
                    .HaveDoneToThem(x => x.AddChild(Pick<Category>.RandomItemFrom(children)))
                .Build();

            Assert.That(categories[0].Children.Count, Is.EqualTo(1));
            Assert.That(categories[1].Children.Count, Is.EqualTo(1));
        }

        [Test]
        [Description("Multi functions allow you to call a method on an object on each item in a list")]
        public void UsingMultiFunctions()
        {
            var categories = Builder<Category>.CreateListOfSize(5).Build();

            // Here we are saying, add every product to all of the categories

            var products = Builder<Product>
                .CreateListOfSize(10)
                .WhereAll().HaveDoneToThemForAll((product, category) => product.AddToCategory(category), categories)
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
            var generator = new RandomGenerator();

            var list = Builder<Product>.CreateListOfSize(3)
                .WhereAll()
                .Have(x => x.QuantityInStock = generator.Next(1000, 2000))
                .Build();

            Assert.That(list[0].QuantityInStock, Is.AtLeast(1000));
            Assert.That(list[0].QuantityInStock, Is.AtMost(2000));

            Assert.That(list[1].QuantityInStock, Is.AtLeast(1000));
            Assert.That(list[1].QuantityInStock, Is.AtMost(2000));

            Assert.That(list[2].QuantityInStock, Is.AtLeast(1000));
            Assert.That(list[2].QuantityInStock, Is.AtMost(2000));
        }

        [Test]
        [ExpectedException(typeof(BuilderException))]
        public void WillNotLetYouDoThingsThatDoNotMakeSense()
        {
            Builder<Product>
                .CreateListOfSize(10)
                .WhereTheFirst(5)
                    .Have(x => x.Title = "titleone")
                .AndTheNext(10)
                    .Have(x => x.Title = "titletwo")
                .Build();
        }

        [Test]
        public void SupportsStructsButDoesNotSupportAutomaticallyNamingTheProperties()
        {
            var locations = Builder<WarehouseLocation>
                .CreateListOfSize(10)
                .Build();

            Assert.That(locations.Count, Is.EqualTo(10));
            Assert.That(locations[0].Location, Is.EqualTo(0));
            Assert.That(locations[1].Location, Is.EqualTo(0));
        }

        [Test]
        public void StructsCanHavePropertyAssignments()
        {
            // ctor: WarehouseLocation(char aisle, int shelf, int location)

            var locations = Builder<WarehouseLocation>
                .CreateListOfSize(10)
                .WhereSection(5,6)
                .AreConstructedWith('A', 1, 2)
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
            BuilderSetup.AutoNameProperties = false;

            var products = Builder<Product>.CreateListOfSize(10).Build();

            Assert.That(products[0].Title, Is.Null);
            Assert.That(products[9].Title, Is.Null);

            Assert.That(products[0].Id, Is.EqualTo(0));
            Assert.That(products[9].Id, Is.EqualTo(0));
        }

        [Test]
        public void ItIsPossibleToSwitchOffAutomaticPropertyNamingForASinglePropertyOfASpecificType()
        {
            BuilderSetup.DisablePropertyNamingFor<Product, int>(x => x.Id);

            var products = Builder<Product>.CreateListOfSize(10).Build();

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
            BuilderSetup.SetDefaultPropertyNamer(new RandomValuePropertyNamer(new RandomGenerator(), new ReflectionUtil(), true, DateTime.Now, DateTime.Now.AddDays(10), true));

            var products = Builder<Product>.CreateListOfSize(10).Build();

            Assert.That(products[0].Title, Is.Not.EqualTo("StringOne1"));
            Assert.That(products[9].Title, Is.Not.EqualTo("StringOne10"));
        }
    }
}