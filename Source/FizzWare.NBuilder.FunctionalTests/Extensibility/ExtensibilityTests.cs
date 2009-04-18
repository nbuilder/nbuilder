using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.FunctionalTests.Extensibility
{
    [TestFixture]
    public class ExtensibilityTests
    {
        [SetUp]
        public void SetUp()
        {
            new SetupFixture().SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            BuilderSetup.ResetToDefaults();            
        }

        [Test]
        public void AddingACustom_Have_ExtensionForProducts()
        {
            var products = Builder<Product>
                .CreateListOfSize(10)
                .WhereAll()
                .HaveWarehouseLocations() // This will only appear when using Builder<Product>
                .Build();
            
            Assert.That(products[0].Location.Aisle, Is.EqualTo('A'));
            Assert.That(products[0].Location.Shelf, Is.EqualTo(1));
            Assert.That(products[0].Location.Location, Is.EqualTo(7500));

            Assert.That(products[9].Location.Aisle, Is.EqualTo('J'));
            Assert.That(products[9].Location.Shelf, Is.EqualTo(10));
            Assert.That(products[9].Location.Location, Is.EqualTo(16500));
        }

        [Test]
        public void SpecifyingACustomPropertyNamerForASpecificType()
        {
            BuilderSetup.SetPropertyNamerFor<Product>(new CustomProductPropertyNamer(new ReflectionUtil()));

            var products = Builder<Product>.CreateListOfSize(10).Build();

            Assert.That(products[0].Location.Aisle, Is.EqualTo('A'));
            Assert.That(products[0].Location.Shelf, Is.EqualTo(2));
            Assert.That(products[0].Location.Location, Is.EqualTo(1000));
            
            Assert.That(products[9].Location.Aisle, Is.EqualTo('J'));
            Assert.That(products[9].Location.Shelf, Is.EqualTo(20));
            Assert.That(products[9].Location.Location, Is.EqualTo(10000));
            
            // Reset it afterwards so the other tests work as expected
            BuilderSetup.ResetToDefaults();
        }
    }    
}