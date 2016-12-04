using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests.Extensibility
{
    [TestFixture]
    public class ExtensibilityTests
    {


      

        [Test]
        public void AddingACustom_With_ExtensionForProducts()
        {
            BuilderSettings builderSettings = new RepositoryBuilderSetup().DoSetup();
            var products = new Builder(builderSettings)
                .CreateListOfSize<Product>(10)
                .All()
                .WithWarehouseLocations() // This will only appear when using Builder<Product>
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
            BuilderSettings builderSettings = new RepositoryBuilderSetup().DoSetup();
            builderSettings.SetPropertyNamerFor<Product>(new CustomProductPropertyNamer(new ReflectionUtil(),builderSettings));

            var products = new Builder(builderSettings).CreateListOfSize<Product>(10).Build();

            Assert.That(products[0].Location.Aisle, Is.EqualTo('A'));
            Assert.That(products[0].Location.Shelf, Is.EqualTo(2));
            Assert.That(products[0].Location.Location, Is.EqualTo(1000));
            
            Assert.That(products[9].Location.Aisle, Is.EqualTo('J'));
            Assert.That(products[9].Location.Shelf, Is.EqualTo(20));
            Assert.That(products[9].Location.Location, Is.EqualTo(10000));

            // Reset it afterwards so the other tests work as expected
            builderSettings.ResetToDefaults();
        }
    }    
}