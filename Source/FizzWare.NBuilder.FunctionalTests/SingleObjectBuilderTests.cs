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
        private interface IMyInterface { }
        private abstract class MyAbstractClass { }

        [SetUp]
        public void SetUp()
        {
            // Need to call this explicitly here to overcome a bug in resharper's test runner
            new RepositoryBuilderSetup().SetUp();
        }

        [Test]
        public void CreatingAnObject()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>().Build();

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
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                .CreateNew< Product>()
                    .With(x => x.Description = "A custom description here")
                .Build();

            Assert.That(product.Title, Is.EqualTo("Title1"));
            Assert.That(product.Description, Is.EqualTo("A custom description here"));
            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void SettingMultipleProperties()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                .CreateNew< Product>()
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
            var builderSetup = new BuilderSettings();
            var invoice = new Builder(builderSetup)
                .CreateNew< Invoice>()
                .With(x => x.Amount, 100)
                .Build();

            Assert.That(invoice.Amount, Is.EqualTo(100));
        }

        [Test]
        public void ItsPossibleToAssignValuesToReadonlyProperties()
        {
            var builderSetup = new BuilderSettings();
            var invoice = new Builder(builderSetup)
                .CreateNew< Invoice>()
                .With(x => x.Id, 100)
                .Build();

            Assert.That(invoice.Id, Is.EqualTo(100));
        }

        [Test]
        public void ItsPosibleToUseAndInAdditionToWithInOrderToImproveReadability()
        {
            var builderSetup = new BuilderSettings();
            var invoice = new Builder(builderSetup)
                .CreateNew< Invoice>()
                .With(x => x.Amount, 100)
                .And(x => x.Id, 200)
                .Build();

            Assert.That(invoice.Amount, Is.EqualTo(100));
            Assert.That(invoice.Id, Is.EqualTo(200));
        }

        [Test]
        public void ItsPossibleToUseThePrivateSetWithToSetNormalProperties()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                .CreateNew< Product>()
                .With(x => x.Title, "special title")
                .Build();

            Assert.That(product.Title, Is.EqualTo("special title"));
        }

        [Test]
        public void CreatingAClassThatHasAConstructorUsingLegacySyntax()
        {
            var builder = new Builder();
            var basket = builder.CreateNew< ShoppingBasket>().Build();
            var product = builder.CreateNew< Product>().Build();
            const int quantity = 5;

            // BasketItem's ctor: BasketItem(ShoppingBasket basket, Product product, int quantity)
            var basketItem = new Builder()
                .CreateNew< BasketItem>()
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
        public void CreatingAClassThatHasAConstructorUsingExpressionSyntax()
        {
            var builder = new Builder();
            var basket = builder.CreateNew< ShoppingBasket>().Build();
            var product = builder.CreateNew< Product>().Build();
            const int quantity = 5;

            // BasketItem's ctor: BasketItem(ShoppingBasket basket, Product product, int quantity)
            var basketItem = builder
                .CreateNew< BasketItem>()
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
            var builder = new Builder();
            var child = builder.CreateNew< Category>().Build();

            var category = builder
                .CreateNew< Category>()
                    .Do(x => x.AddChild(child))
                .Build();

            Assert.That(category.Children[0], Is.EqualTo(child));
        }

        [Test]
        public void CallingMultipleMethods()
        {
            var builderSetup = new BuilderSettings();
            var builder = new Builder(builderSetup);
            var child = builder.CreateNew< Category>().Build();
            var anotherChild = builder.CreateNew< Category>().Build();

            var category = builder
                .CreateNew< Category>()
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
            var builderSetup = new BuilderSettings();
            var builder = new Builder(builderSetup);
            var categories = builder.CreateListOfSize< Category>(5).Build();

            var product = builder
                    .CreateNew< Product>()
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
        public void NBuilderIsNotAMockingFramework() // (!)
        {
            var builderSetup = new BuilderSettings();
            Assert.Throws<TypeCreationException>(() =>
            {
                new Builder(builderSetup).CreateNew< IProduct>().Build();
            });
        }

        [Test]
        public void NBuilderCannotBeUsedToBuildInterfaces()
        {
            var builderSetup = new BuilderSettings();
            var ex = Assert.Throws<TypeCreationException>(() => new Builder(builderSetup).CreateNew< IMyInterface>().Build());
            Assert.That(ex.Message, Is.EqualTo("Cannot build an interface"));
        }

        [Test]
        public void NBuilderCannotBeUsedToBuildAbstractClasses()
        {
            var builderSetup = new BuilderSettings();
            var ex = Assert.Throws<TypeCreationException>(() => new Builder(builderSetup).CreateNew< MyAbstractClass>().Build(), "Cannot build an abstract class");
            Assert.That(ex.Message, Is.EqualTo("Cannot build an abstract class"));
        }

        [Test]
        public void WillComplainIfYouTryToBuildAClassThatCannotBeInstantiatedDirectly()
        {
            var builderSetup = new BuilderSettings();
            Assert.Throws<TypeCreationException>(() =>
            {
                new Builder(builderSetup).CreateNew< ChuckNorris>().Build();
            });
        }

    }
}