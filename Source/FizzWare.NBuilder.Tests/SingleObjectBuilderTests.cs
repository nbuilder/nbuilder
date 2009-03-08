using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestModel;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests
{
    [TestFixture]
    public class SingleObjectBuilderTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnObject()
        {
            var taxType = Builder<TaxType>.CreateNew()
                                          .With(x => x.Name = "VAT")
                                          .And(x => x.Percentage = 15m).Build();

            Assert.That(taxType.Name, Is.EqualTo("VAT"));
            Assert.That(taxType.Percentage, Is.EqualTo(15m));
        }

        [Test]
        public void ShouldBeAbleToUseDo_And()
        {
            var product = Builder<Product>.CreateNew().Build();
            var product2 = Builder<Product>.CreateNew().Build();

            var shoppingBasket = Builder<ShoppingBasket>.CreateNew().Do(x => x.Add(product, 2)).And(x => x.Add(product2, 5)).Build();

            Assert.That(shoppingBasket.Items.Count, Is.EqualTo(2));
            Assert.That(shoppingBasket.Items.First(x => x.Product == product) != null);
            Assert.That(shoppingBasket.Items.First(x => x.Product == product2) != null);
        }
    }
}