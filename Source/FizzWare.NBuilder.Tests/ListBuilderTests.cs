using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Generators;
using FizzWare.NBuilder.Tests.TestModel;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests
{
    [TestFixture]
    public class ListBuilderTests
    {
        [Test]
        public void ShouldBeAbleToCreateAList()
        {
            var productList =
                Builder<Product>.CreateListOfSize(50).WhereTheFirst(25).Have(x => x.Title = "TitleOne").List;

            for (int i = 0; i < 25; i++)
            {
                Assert.That(productList[i].Title == "TitleOne");
            }
        }

        [Test]
        public void ShouldBeAbleToUseWhereTheLast()
        {
            var productList =
                Builder<Product>.CreateListOfSize(100).WhereTheLast(50).Have(x => x.Title = "TitleOne").List;

            for (int i = 0; i < 50; i++)
                Assert.That(productList[i].Title, Is.Not.EqualTo("TitleOne"));

            for (int i = 50; i < 100; i++)
                Assert.That(productList[i].Title, Is.EqualTo("TitleOne"));
        }

        [Test]
        public void ShouldBeAbleToUseAndTheNext()
        {
            var productList =
                Builder<Product>.CreateListOfSize(50).WhereTheFirst(25).Have(x => x.Title = "TitleOne").AndTheNext(25).Have
                    (x => x.Title = "TitleTwo").List;

            for (int i = 0; i < 25; i++)
            {
                Assert.That(productList[i].Title, Is.EqualTo("TitleOne"));
            }

            for (int i = 25; i < 50; i++)
            {
                Assert.That(productList[i].Title, Is.EqualTo("TitleTwo"));
            }
        }

        [Test]
        public void ShouldBeAbleToUseAnd()
        {
            var productList =
                Builder<Product>.CreateListOfSize(50).WhereTheFirst(25).Have(x => x.Title = "TitleOne").And(x => x.Description = "DescriptionOne").List;

            for (int i = 0; i < 25; i++)
            {
                Assert.That(productList[i].Title, Is.EqualTo("TitleOne"));
                Assert.That(productList[i].Description, Is.EqualTo("DescriptionOne"));
            }

            for (int i = 25; i < 50; i++)
            {
                Assert.That(productList[i].Title, Is.Not.EqualTo("TitleOne"));
                Assert.That(productList[i].Description, Is.Not.EqualTo("DescriptionOne"));
            }
        }

        [Test]
        public void ShouldBeAbleToUseBasedOn()
        {
            var product = new Product();
            product.Title = "TitleOne";

            var productList = Builder<Product>.CreateListOfSize(10).BasedOn(product).List;

            Assert.That(productList, Has.All.Property("Title").EqualTo("TitleOne"));
        }

        [Test]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            IList<Category> categoryList = Builder<Category>.CreateListOfSize(20).List;

            var productList = Builder<Product>.CreateListOfSize(10000)
                .WhereAll()
                .Have(
                x =>
                x.Categories = Pick.UniqueRandomListOf<Category>(With.Between(1).And(5).Elements).From(categoryList))
                .And(x => x.Title = "TheTitle").List;

            foreach (var product in productList)
            {
                Assert.That(product.Categories.Count(), Is.AtLeast(1));
                Assert.That(product.Categories.Count(), Is.AtMost(10));
                Assert.That(product.Title, Is.EqualTo("TheTitle"));
            }
        }

        [Test]
        public void BetweenPickerConstraintCountsShouldBeDifferent()
        {
            IList<Category> categoryList = Builder<Category>.CreateListOfSize(20).List;

            var productList = Builder<Product>.CreateListOfSize(1000)
                .WhereAll()
                .Have(x => x.Categories = Pick.UniqueRandomListOf<Category>(With.Between(50).And(100).Elements).From(categoryList))
                .And(x => x.Title = "TheTitle").List;

            bool different = false;
            // Check the counts are different
            for (int i = 0; i < productList.Count; i++)
            {
                if (i == 0)
                    continue;

                if (productList[i].Categories.Count != productList[i - 1].Categories.Count)
                    different = true;
            }

            Assert.That(different, Is.True, "All the counts were the same");
        }

        [Test]
        public void ShouldBeAbleToUseExactlyPickerConstraint()
        {
            IList<Category> categoryList = Builder<Category>.CreateListOfSize(20).List;

            var productList = Builder<Product>.CreateListOfSize(20)
                .WhereAll()
                .Have(x => x.Categories = Pick.UniqueRandomListOf<Category>(With.Exactly(6).Elements).From(categoryList))
                .And(x => x.Title = "TheTitle").List;

            foreach (var product in productList)
            {
                Assert.That(product.Categories.Count(), Is.EqualTo(6));
                Assert.That(product.Title, Is.EqualTo("TheTitle"));
            }
        }

        [Test]
        public void ShouldBeAbleToUseUpToPickerConstraint()
        {
            IList<Category> categoryList = Builder<Category>.CreateListOfSize(20).List;

            var productList = Builder<Product>.CreateListOfSize(20)
                .WhereAll()
                .Have(x => x.Categories = Pick.UniqueRandomListOf<Category>(With.UpTo(5).Elements).From(categoryList))
                .List;

            foreach (var product in productList)
            {
                Assert.That(product.Categories.Count(), Is.AtMost(5));
            }
        }

        [Test]
        public void ShouldBeAbleToUseAtLeastPickerConstraint()
        {
            IList<Category> categoryList = Builder<Category>.CreateListOfSize(20).List;

            var productList = Builder<Product>.CreateListOfSize(20)
                .WhereAll()
                .Have(x => x.Categories = Pick.UniqueRandomListOf<Category>(With.AtLeast(5).Elements).From(categoryList))
                .List;

            foreach (var product in productList)
            {
                Assert.That(product.Categories.Count(), Is.AtLeast(5));
            }
        }

        [Test]
        public void RealWorldExample()
        {
            TaxType vat = new TaxType();

            var intGenerator = new RandomGenerator<int>(0, 2000);
            var decimalGenerator = new RandomGenerator<decimal>(5, 500);

            var products = Builder<Product>.CreateListOfSize(100)
                .WhereAll()
                .Have(x => x.TaxType = vat)
                  .And(x => x.QuantityInStock = intGenerator.Generate())
                  .And(x => x.PriceBeforeTax = decimalGenerator.Generate())
                .WhereTheFirst(10)
                  .Have(x => x.Title = "The first ten")
                    .And(x => x.QuantityInStock = 5)
                .WhereRandom(90)
                  .Have(x => x.Description = "DESCRIPTION")
                .List;

            for (int i = 0; i <  10; i++)
                Assert.That(products[i].QuantityInStock, Is.EqualTo(5));

            // The last 90 should all have DESCRIPTION as their description
            for (int i = 10; i < products.Count; i++)
                Assert.That(products[i].Description, Is.EqualTo("DESCRIPTION"));

            // The first 10 should have the assigned description
            for (int i = 0; i < 10; i++)
                Assert.That(products[i].Description, Is.EqualTo("Description" + (i + 1)));
        }

        [Test]
        public void ShouldBeAbleToUseSingleAndListBuilderTogether()
        {
            var decimalGenerator = new RandomGenerator<decimal>(1, 999);

            var taxType = Builder<TaxType>.CreateNew()
                                          .With(x => x.Name = "VAT")
                                          .And(x => x.Percentage = 15m).Value;

            var categories = Builder<Category>.CreateListOfSize(10).List;

            var products = Builder<Product>.CreateListOfSize(50)
                .WhereAll()
                .Have(x => x.TaxType = taxType)
                .And(x => x.PriceBeforeTax = decimalGenerator.Generate())
                .And(x => x.Categories = Pick.UniqueRandomListOf<Category>(With.Between(1).And(2).Elements).From(categories))
                .List;

            Assert.That(products, Has.All.Property("TaxType", taxType));
        }

        [Test]
        public void ShouldBeAbleToPickUniqueObjects()
        {
            // eg 50 Products - 50 different tax types

            var taxTypes = Builder<TaxType>.CreateListOfSize(50).List;

            var products = Builder<Product>.CreateListOfSize(50)
                .WhereAll()
                .Have(x => x.TaxType = Pick.Unique<TaxType>().From(taxTypes))
                .List;

            List<TaxType> assignedTaxTypes = new List<TaxType>();

            foreach (var product in products)
                assignedTaxTypes.Add(product.TaxType);
        }

        [Test]
        public void ByDefaultShouldIncrementNumberFieldsStartingWith1()
        {
            var products = Builder<Product>.CreateListOfSize(10).List;

            for (int i = 0; i < products.Count; i++)
            {
                Assert.That(products[i].Id, Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ByDefaultShouldStringFieldsShouldBeTheirNameThenTheirOneBasedIndex()
        {
            var products = Builder<Product>.CreateListOfSize(10).List;

            for (int i = 0; i < products.Count; i++)
            {
                Assert.That(products[i].Title, Is.EqualTo("Title" + (i + 1)));
            }
        }

        [Test]
        public void ShouldBeAbleToUseHaveDoneToThem()
        {
            // Problem - you have a class a value object for instance, which doesn't have a public
            //           no parameter constructor.
            //           You want to be able to add this object to every element of your built list
            //           using the fluent interface.

            var product = Builder<Product>.CreateNew().Value;
            var product2 = Builder<Product>.CreateNew().Value;

            var baskets = Builder<ShoppingBasket>.CreateListOfSize(5).WhereAll().HaveDoneToThem(x => x.Add(product, 1)).And(x => x.Add(product2, 2)).List;

            for (int i = 0; i < baskets.Count; i++)
            {
                Assert.That(baskets[i].Items.Count, Is.EqualTo(2));
            }
        }
    }
}