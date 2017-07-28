using System.Linq;
using FizzWare.NBuilder.Tests.Integration.Models;
using FizzWare.NBuilder.Tests.Integration.Models.Repositories;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class RepositoryPersistenceTests
    {
        [OneTimeSetUp]
        public void BeforeEach()
        {
            new ProductRepository().DeleteAll();
            new CategoryRepository().DeleteAll();
        }

        [Test]
        public void PersistingASingleObject()
        {
            var builderSetup =new RepositoryBuilderSetup().SetUp();
            new Builder(builderSetup).CreateNew< Product>().Persist();

            // Go directly to the database to do some asserts
            var dataTable = new ProductRepository().GetAll();

            Assert.That(dataTable.Count, Is.EqualTo(1));

            Assert.That(dataTable[0].Title, Is.EqualTo("Title1"));
            Assert.That(dataTable[0].Description, Is.EqualTo("Description1"));
            Assert.That(dataTable[0].PriceBeforeTax, Is.EqualTo(1m));
            Assert.That(dataTable[0].QuantityInStock, Is.EqualTo(1));
        }

        [Test]
        public void PersistingASingleTaxTypeAndAListOf100Products()
        {
            var builderSetup = new RepositoryBuilderSetup().SetUp();
            var taxType = new Builder(builderSetup).CreateNew< TaxType>().Persist();

            new Builder(builderSetup).CreateListOfSize< Product>(100)
                .All()
                    .With(x => x.TaxType = taxType)
                .Persist(); // NB: Persistence is setup in the RepositoryBuilderSetup class

            var dbProducts = new ProductRepository().GetAll();

            Assert.That(dbProducts.Count, Is.EqualTo(100));
        }


        [Test]
        public void PersistingAListOfProductsAndCategories()
        {
            var builderSetup = new RepositoryBuilderSetup().SetUp();
            const int numProducts = 500;
            const int numCategories = 50;
            const int numCategoriesForEachProduct = 5;

            var categories = new Builder(builderSetup)
                .CreateListOfSize< Category>(numCategories)
                .Persist();

            new Builder(builderSetup)
                 .CreateListOfSize< Product>(numProducts)
                .All()
                    .With(x => x.Categories = Pick<Category>.
                        UniqueRandomList(With.Exactly(numCategoriesForEachProduct).Elements)
                        .From(categories)
                        .ToList())
                .Persist(); // NB: Persistence is setup in the RepositoryBuilderSetup class

            var productsTable = new ProductRepository().GetAll();
            var categoriesTable = new CategoryRepository().GetAll();

            Assert.That(productsTable.Count, Is.EqualTo(numProducts), "products");
            Assert.That(categoriesTable.Count, Is.EqualTo(numCategories), "categories");
        }

        // TODO: Add CreatingAHierarchyOfCategoriesAndAddingProducts
    }
}