using System.Linq;
using FizzWare.NBuilder.Tests.Integration.Models;
using FizzWare.NBuilder.Tests.Integration.Models.Repositories;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class RepositoryPersistenceTests
    {
        public RepositoryPersistenceTests()
        {
            new ProductRepository().DeleteAll();
            new CategoryRepository().DeleteAll();
        }

        [Fact]
        public void PersistingASingleObject()
        {
            var builderSetup =new RepositoryBuilderSetup().SetUp();
            new Builder(builderSetup).CreateNew< Product>().Persist();

            // Go directly to the database to do some asserts
            var dataTable = new ProductRepository().GetAll();

            dataTable.Count.ShouldBe(1);

            dataTable[0].Title.ShouldBe("Title1");
            dataTable[0].Description.ShouldBe("Description1");
            dataTable[0].PriceBeforeTax.ShouldBe(1m);
            dataTable[0].QuantityInStock.ShouldBe(1);
        }

        [Fact]
        public void PersistingASingleTaxTypeAndAListOf100Products()
        {
            var builderSetup = new RepositoryBuilderSetup().SetUp();
            var taxType = new Builder(builderSetup).CreateNew< TaxType>().Persist();

            new Builder(builderSetup).CreateListOfSize< Product>(100)
                .All()
                    .With(x => x.TaxType = taxType)
                .Persist(); // NB: Persistence is setup in the RepositoryBuilderSetup class

            var dbProducts = new ProductRepository().GetAll();

            dbProducts.Count.ShouldBe(100);
        }


        [Fact]
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

            productsTable.Count.ShouldBe(numProducts, "products");
            categoriesTable.Count.ShouldBe(numCategories, "categories");
        }

        // TODO: Add CreatingAHierarchyOfCategoriesAndAddingProducts
    }
}