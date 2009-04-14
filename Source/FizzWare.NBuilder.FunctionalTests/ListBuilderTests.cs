using FizzWare.NBuilder.FunctionalTests.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.FunctionalTests
{
    /// <remarks>
    /// To run these tests, create a local database named 'NBuilderTests'
    /// </remarks>
    [TestFixture]
    public class ListBuilderTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Need to call this explicitly here to overcome a bug in resharper's test runner
            new SetupFixture().TestFixtureSetUp();
        }

        [SetUp]
        public void SetUp()
        {
            Database.Clear();
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
        public void UsingAGenerator()
        {
            var generator = new RandomGenerator<decimal>();

            var products = Builder<Product>
                .CreateListOfSize(10)
                .WhereAll()
                    .Have(x => x.PriceBeforeTax = generator.Generate(50, 1000))
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
        public void PersistingATaxTypeAnd100Products()
        {
            var taxType = Builder<TaxType>.CreateNew().Persist();

            Builder<Product>.CreateListOfSize(100)
                .WhereAll()
                    .Have(x => x.TaxType = taxType)
                .Persist(); // NB: Persistence is setup in the SetupFixture class

            var dbProducts = Database.GetContentsOf(Database.Tables.Product);

            Assert.That(dbProducts.Rows.Count, Is.EqualTo(100));
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
        public void PersistingAListOfProductsAndCategories()
        {
            var categories = Builder<Category>.CreateListOfSize(50).Persist();

            Builder<Product>
                .CreateListOfSize(500)
                .WhereAll()
                    .Have(x => x.Categories = Pick<Category>.UniqueRandomList(With.Between(5, 10).Elements).From(categories))
                .Persist(); // NB: Persistence is setup in the SetupFixture class
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
        [Description("You can use HaveDoneToThemForAll to do something to all the items in the declaration")]
        public void UsingHaveDoneToThem()
        {
            var children = Builder<Category>.CreateListOfSize(2).Build();

            var categories = Builder<Category>
                .CreateListOfSize(10)
                .WhereTheFirst(2)
                    .HaveDoneToThem(x => x.AddChild(children[0]))
                .AndTheNext(2)
                    .HaveDoneToThem(x => x.AddChild(children[1]))
                .Build();
                
            Assert.That(categories[0].Children[0], Is.EqualTo(children[0]));
            Assert.That(categories[1].Children[0], Is.EqualTo(children[0]));
            Assert.That(categories[2].Children[0], Is.EqualTo(children[1]));
            Assert.That(categories[3].Children[0], Is.EqualTo(children[1]));
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
                Assert.That(product.Categories, Has.Count(5));
                Assert.That(product.Categories[0], Is.EqualTo(categories[0]));
                Assert.That(product.Categories[1], Is.EqualTo(categories[1]));
                Assert.That(product.Categories[2], Is.EqualTo(categories[2]));
                Assert.That(product.Categories[3], Is.EqualTo(categories[3]));
                Assert.That(product.Categories[4], Is.EqualTo(categories[4]));
            }
        }
    }
}