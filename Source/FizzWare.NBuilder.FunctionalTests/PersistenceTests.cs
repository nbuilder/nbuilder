using System.Data;
using System.Linq;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    [TestFixture]
    public class PersistenceTests
    {
        private BuilderSettings builderSettings;

        [SetUp]
        public void BeforeEach()
        {
            this.builderSettings = new PersistenceTestsBuilderSetup().SetUp();
        }

        [Test]
        public void PersistingASingleObject()
        {
            new Builder(builderSettings).CreateNew<Product>().Persist();

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
            var builder = new Builder(builderSettings);
            var taxType = builder.CreateNew<TaxType>().Persist();

            builder.CreateListOfSize<Product>(100)
                .All()
                .With(x => x.TaxType = taxType)
                .Persist(); // NB: Persistence is setup in the RepositoryBuilderSetup class

            var dbProducts = new ProductRepository().GetAll();

            Assert.That(dbProducts.Count, Is.EqualTo(100));
        }


        [Test]
        public void PersistingAListOfProductsAndCategories()
        {
            const int numProducts = 500;
            const int numCategories = 50;
            const int numCategoriesForEachProduct = 5;

            var builder = new Builder(builderSettings);
            var categories = builder
                .CreateListOfSize<Category>(numCategories)
                .Persist();

            builder
                .CreateListOfSize<Product>(numProducts)
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