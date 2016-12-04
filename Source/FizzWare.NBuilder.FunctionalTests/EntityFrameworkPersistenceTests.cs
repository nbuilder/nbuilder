using System.Data;
using System.Linq;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    [TestFixture]
    public class EntityFrameworkPersistenceTests
    {
        private BuilderSettings builderSettings;

        [SetUp]
        public void BeforeEach()
        {
            this.builderSettings = new EntityFrameworkBuilderSetup().SetUp();
        }

        [Test]
        public void PersistingASingleObject()
        {
            new Builder(builderSettings).CreateNew<Product>().Persist();

            // Go directly to the database to do some asserts
            var dataTable = Database.GetContentsOf(Database.Tables.Product);

            Assert.That(dataTable.Rows.Count, Is.EqualTo(1));

            Assert.That(dataTable.Rows[0]["Title"], Is.EqualTo("Title1"));
            Assert.That(dataTable.Rows[0]["Description"], Is.EqualTo("Description1"));
            Assert.That(dataTable.Rows[0]["PriceBeforeTax"], Is.EqualTo(1m));
            Assert.That(dataTable.Rows[0]["QuantityInStock"], Is.EqualTo(1));
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

            var dbProducts = Database.GetContentsOf(Database.Tables.Product);

            Assert.That(dbProducts.Rows.Count, Is.EqualTo(100));
        }

        [Test]
        [Description("Instead of assigning directly to a category")]
        public void PersistingUsingPick_UpTo_AndDoForEach()
        {
            var builder = new Builder(builderSettings);
            var categories = builder.CreateListOfSize<Category>(10).Persist();

            builder
                .CreateListOfSize<Product>(50)
                .All()
                .DoForEach((x, y) => x.AddToCategory(y), Pick<Category>.UniqueRandomList(With.UpTo(4).Elements).From(categories))
                .Persist();

            DataTable productCategoriesTable = Database.GetContentsOf(Database.Tables.ProductCategory);

            Assert.That(productCategoriesTable.Rows.Count, Is.LessThanOrEqualTo(50 * 4));
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

            DataTable productsTable = Database.GetContentsOf(Database.Tables.Product);
            DataTable categoriesTable = Database.GetContentsOf(Database.Tables.Category);
            DataTable productCategoriesTable = Database.GetContentsOf(Database.Tables.ProductCategory);

            Assert.That(productsTable.Rows.Count, Is.EqualTo(numProducts), "products");
            Assert.That(categoriesTable.Rows.Count, Is.EqualTo(numCategories), "categories");
            Assert.That(productCategoriesTable.Rows.Count, Is.EqualTo(numCategoriesForEachProduct * numProducts));
        }

        // TODO: Add CreatingAHierarchyOfCategoriesAndAddingProducts
    }
}