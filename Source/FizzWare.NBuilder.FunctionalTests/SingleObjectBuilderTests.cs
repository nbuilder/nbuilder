using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    // Note: The assertions are intentionally verbose to show how NBuilder works
    [TestFixture]
    public class SingleObjectBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            // Need to call this explicitly here to overcome a bug in resharper's test runner
            new SetupFixture().SetUp();

            Database.Clear();
        }

        [Test]
        public void CreatingAnObject()
        {
            var product = Builder<Product>.CreateNew().Build();

            Assert.That(product.Id, Is.EqualTo(1));
            Assert.That(product.Title, Is.EqualTo("Title1"));
            Assert.That(product.Description, Is.EqualTo("Description1"));
            Assert.That(product.PriceBeforeTax, Is.EqualTo(1m));
            Assert.That(product.QuantityInStock, Is.EqualTo(1));
            Assert.That(product.Weight, Is.EqualTo(1.0));
        }

        [Test]
        public void SettingTheValueOfAProperty()
        {
            var product = Builder<Product>
                .CreateNew()
                    .With(x => x.Description = "A custom description here")
                .Build();

            Assert.That(product.Title, Is.EqualTo("Title1"));
            Assert.That(product.Description, Is.EqualTo("A custom description here"));
            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void SettingMultipleProperties()
        {
            var product = Builder<Product>
                .CreateNew()
                .With(x => x.Title = "Special title")
                .And(x => x.Description = "Special description") 
                .And(x => x.Id = 2)
                .Build();

            Assert.That(product.Title, Is.EqualTo("Special title"));
            Assert.That(product.Description, Is.EqualTo("Special description"));
            Assert.That(product.Id, Is.EqualTo(2));

            // NB: You can call With() multiple times too, but And() is provided
            //     to improve readability
        }


        [Test]
        public void ItsPossibleToAssignValuesToPrivateSetProperties()
        {
            var invoice = Builder<Invoice>
                .CreateNew()
                .With(x => x.Amount, 100)
                .Build();

            Assert.That(invoice.Amount, Is.EqualTo(100));
        }

        [Test]
        public void ItsPossibleToAssignValuesToReadonlyProperties()
        {
            var invoice = Builder<Invoice>
                .CreateNew()
                .With(x => x.Id, 100)
                .Build();

            Assert.That(invoice.Id, Is.EqualTo(100));
        }

        [Test]
        public void ItsPossibleToUseThePrivateSetWithToSetNormalProperties()
        {
            var product = Builder<Product>
                .CreateNew()
                .With(x => x.Title, "special title")
                .Build();

            Assert.That(product.Title, Is.EqualTo("special title"));
        }

        [Test]
        public void CreatingAClassThatHasAConstructorUsingLegacySyntax()
        {
            var basket = Builder<ShoppingBasket>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            const int quantity = 5;

            // BasketItem's ctor: BasketItem(ShoppingBasket basket, Product product, int quantity)
            var basketItem = Builder<BasketItem>
                .CreateNew()
                    .WithConstructorArgs(basket, product, quantity)
                .Build();

            // The property namer will still apply sequential names to the properties
            // however it won't overwrite any properties that have been set through the constructor
            Assert.That(basketItem.DiscountCode, Is.EqualTo("DiscountCode1"));

            Assert.That(basketItem.Basket, Is.EqualTo(basket));
            Assert.That(basketItem.Product, Is.EqualTo(product));
            Assert.That(basketItem.Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void CreatingAClassThatHasAConstructorUsingExpressionSyntax()
        {
            var basket = Builder<ShoppingBasket>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            const int quantity = 5;

            // BasketItem's ctor: BasketItem(ShoppingBasket basket, Product product, int quantity)
            var basketItem = Builder<BasketItem>
                .CreateNew()
                    .WithConstructor(() => new BasketItem(basket, product, quantity))
                .Build();

            // The property namer will still apply sequential names to the properties
            // however it won't overwrite any properties that have been set through the constructor
            Assert.That(basketItem.DiscountCode, Is.EqualTo("DiscountCode1"));

            Assert.That(basketItem.Basket, Is.EqualTo(basket));
            Assert.That(basketItem.Product, Is.EqualTo(product));
            Assert.That(basketItem.Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void UsingDo()
        {
            var child = Builder<Category>.CreateNew().Build();

            var category = Builder<Category>
                .CreateNew()
                    .Do(x => x.AddChild(child))
                .Build();

            Assert.That(category.Children[0], Is.EqualTo(child));
        }

        [Test]
        public void CallingMultipleMethods()
        {
            var child = Builder<Category>.CreateNew().Build();
            var anotherChild = Builder<Category>.CreateNew().Build();

            var category = Builder<Category>
                .CreateNew()
                    .Do(x => x.AddChild(child))
                    .And(x => x.AddChild(anotherChild))
                .Build();

            Assert.That(category.Children[0], Is.EqualTo(child));
            Assert.That(category.Children[1], Is.EqualTo(anotherChild));
        }

        [Test]
        [Description("Multi functions allow you to call a method on an object on each item in a list")]
        public void UsingMultiFunctions()
        {
            var categories = Builder<Category>.CreateListOfSize(5).Build();

            var product = Builder<Product>
                    .CreateNew()
                        .DoForAll( (prod, cat) => prod.AddToCategory(cat), categories)
                    .Build();

            // Assertions are intentionally verbose for clarity
            Assert.That(product.Categories.Count, Is.EqualTo(5));
            Assert.That(product.Categories[0], Is.EqualTo(categories[0]));
            Assert.That(product.Categories[1], Is.EqualTo(categories[1]));
            Assert.That(product.Categories[2], Is.EqualTo(categories[2]));
            Assert.That(product.Categories[3], Is.EqualTo(categories[3]));
            Assert.That(product.Categories[4], Is.EqualTo(categories[4]));
        }

        [Test]
        [ExpectedException(typeof(TypeCreationException))]
        public void NBuilderIsNotAMockingFramework() // (!)
        {
            Builder<IProduct>.CreateNew().Build();
            //      ^
        }
    }
}