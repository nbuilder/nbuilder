using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    [TestFixture]
    public class PersistenceTests
    {
        [SetUp]
        public void SetUp()
        {
            // Need to call this explicitly here to overcome a bug in resharper's test runner
            new SetupFixture().SetUp();

            Database.Clear();
        }

        [Test]
        public void PersistingASingleObject()
        {
            Builder<Product>.CreateNew().Persist();

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
            var taxType = Builder<TaxType>.CreateNew().Persist();

            Builder<Product>.CreateListOfSize(100)
                .WhereAll()
                    .Have(x => x.TaxType = taxType)
                .Persist(); // NB: Persistence is setup in the SetupFixture class

            var dbProducts = Database.GetContentsOf(Database.Tables.Product);

            Assert.That(dbProducts.Rows.Count, Is.EqualTo(100));
        }

        [Test]
        [Description("Instead of assigning directly to a categor")]
        public void PersistingUsingPick_UpTo_AndHaveDoneToThemForAll()
        {
            var categories = Builder<Category>.CreateListOfSize(10).Persist();

            Builder<Product>
                .CreateListOfSize(50)
                .WhereAll()
                .HaveDoneToThemForAll((x, y) => x.AddToCategory(y), Pick<Category>.UniqueRandomList(With.UpTo(4).Elements).From(categories))
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

            var categories = Builder<Category>.CreateListOfSize(numCategories).Persist();

            Builder<Product>
                .CreateListOfSize(numProducts)
                .WhereAll()
                    .Have(x => x.Categories = Pick<Category>.UniqueRandomList(With.Exactly(numCategoriesForEachProduct).Elements).From(categories))
                .Persist(); // NB: Persistence is setup in the SetupFixture class

            DataTable productsTable = Database.GetContentsOf(Database.Tables.Product);
            DataTable categoriesTable = Database.GetContentsOf(Database.Tables.Category);
            DataTable productCategoriesTable = Database.GetContentsOf(Database.Tables.ProductCategory);

            Assert.That(productsTable.Rows.Count, Is.EqualTo(numProducts));
            Assert.That(categoriesTable.Rows.Count, Is.EqualTo(numCategories));
            Assert.That(productCategoriesTable.Rows.Count, Is.EqualTo(numCategoriesForEachProduct * numProducts));
        }

        // TODO: Add CreatingAHierarchyOfCategoriesAndAddingProducts
    }
}