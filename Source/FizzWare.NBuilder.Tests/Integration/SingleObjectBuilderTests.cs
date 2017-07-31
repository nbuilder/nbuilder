using FizzWare.NBuilder.Tests.Integration.Models;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Integration
{
    // Note: The assertions are intentionally verbose to show how NBuilder works
    
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

        [Fact]
        public void CreatingAnObject()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>().Build();

            product.Id.ShouldBe(1);
            product.Title.ShouldBe("Title1");
            product.Description.ShouldBe("Description1");
            product.PriceBeforeTax.ShouldBe(1m);
            product.QuantityInStock.ShouldBe(1);
            product.Weight.ShouldBe(1.0);
        }

        [Fact]
        public void SettingTheValueOfAProperty()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                .CreateNew< Product>()
                    .With(x => x.Description = "A custom description here")
                .Build();

            product.Title.ShouldBe("Title1");
            product.Description.ShouldBe("A custom description here");
            product.Id.ShouldBe(1);
        }

        [Fact]
        public void SettingMultipleProperties()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                .CreateNew< Product>()
                .With(x => x.Title = "Special title")
                .And(x => x.Description = "Special description") 
                .And(x => x.Id = 2)
                .Build();

            product.Title.ShouldBe("Special title");
            product.Description.ShouldBe("Special description");
            product.Id.ShouldBe(2);

            // NB: You can call With() multiple times too, but And() is provided
            //     to improve readability
        }


        [Fact]
        public void ItsPossibleToAssignValuesToPrivateSetProperties()
        {
            var builderSetup = new BuilderSettings();
            var invoice = new Builder(builderSetup)
                .CreateNew< Invoice>()
                .With(x => x.Amount, 100)
                .Build();

            invoice.Amount.ShouldBe(100);
        }

        [Fact]
        public void ItsPossibleToAssignValuesToReadonlyProperties()
        {
            var builderSetup = new BuilderSettings();
            var invoice = new Builder(builderSetup)
                .CreateNew< Invoice>()
                .With(x => x.Id, 100)
                .Build();

            invoice.Id.ShouldBe(100);
        }

        [Fact]
        public void ItsPosibleToUseAndInAdditionToWithInOrderToImproveReadability()
        {
            var builderSetup = new BuilderSettings();
            var invoice = new Builder(builderSetup)
                .CreateNew< Invoice>()
                .With(x => x.Amount, 100)
                .And(x => x.Id, 200)
                .Build();

            invoice.Amount.ShouldBe(100);
            invoice.Id.ShouldBe(200);
        }

        [Fact]
        public void ItsPossibleToUseThePrivateSetWithToSetNormalProperties()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                .CreateNew< Product>()
                .With(x => x.Title, "special title")
                .Build();

            product.Title.ShouldBe("special title");
        }

        [Fact]
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
            basketItem.DiscountCode.ShouldBe("DiscountCode1");

            basketItem.Basket.ShouldBe(basket);
            basketItem.Product.ShouldBe(product);
            basketItem.Quantity.ShouldBe(quantity);
        }

        [Fact]
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
            basketItem.DiscountCode.ShouldBe("DiscountCode1");

            basketItem.Basket.ShouldBe(basket);
            basketItem.Product.ShouldBe(product);
            basketItem.Quantity.ShouldBe(quantity);
        }

        [Fact]
        public void UsingDo()
        {
            var builder = new Builder();
            var child = builder.CreateNew< Category>().Build();

            var category = builder
                .CreateNew< Category>()
                    .Do(x => x.AddChild(child))
                .Build();

            category.Children[0].ShouldBe(child);
        }

        [Fact]
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

            category.Children[0].ShouldBe(child);
            category.Children[1].ShouldBe(anotherChild);
        }

        [Fact]
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
            product.Categories.Count.ShouldBe(5);
            product.Categories[0].ShouldBe(categories[0]);
            product.Categories[1].ShouldBe(categories[1]);
            product.Categories[2].ShouldBe(categories[2]);
            product.Categories[3].ShouldBe(categories[3]);
            product.Categories[4].ShouldBe(categories[4]);
        }

        [Fact]
        public void NBuilderIsNotAMockingFramework() // (!)
        {
            var builderSetup = new BuilderSettings();
            Should.Throw<TypeCreationException>(() =>
            {
                new Builder(builderSetup).CreateNew< IProduct>().Build();
            });
        }

        [Fact]
        public void NBuilderCannotBeUsedToBuildInterfaces()
        {
            var builderSetup = new BuilderSettings();
            var ex = Should.Throw<TypeCreationException>(() => new Builder(builderSetup).CreateNew< IMyInterface>().Build());
            ex.Message.ShouldBe("Cannot build an interface");
        }

        [Fact]
        public void NBuilderCannotBeUsedToBuildAbstractClasses()
        {
            var builderSetup = new BuilderSettings();
            var ex = Should.Throw<TypeCreationException>(() => new Builder(builderSetup).CreateNew< MyAbstractClass>().Build(), "Cannot build an abstract class");
            ex.Message.ShouldBe("Cannot build an abstract class");
        }

        [Fact]
        public void WillComplainIfYouTryToBuildAClassThatCannotBeInstantiatedDirectly()
        {
            var builderSetup = new BuilderSettings();
            Should.Throw<TypeCreationException>(() =>
            {
                new Builder(builderSetup).CreateNew< ChuckNorris>().Build();
            });
        }

    }
}