using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.Integration.Models;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration.Extensibility
{
    
    public class ExtensibilityTests
    {

        [Fact]
        public void AddingACustom_With_ExtensionForProducts()
        {
            BuilderSettings builderSettings = new RepositoryBuilderSetup().DoSetup();
            var products = new Builder(builderSettings)
                .CreateListOfSize<Product>(10)
                .All()
                .WithWarehouseLocations() // This will only appear when using Builder<Product>
                .Build();
            
            products[0].Location.Aisle.ShouldBe('A');
            products[0].Location.Shelf.ShouldBe(1);
            products[0].Location.Location.ShouldBe(7500);

            products[9].Location.Aisle.ShouldBe('J');
            products[9].Location.Shelf.ShouldBe(10);
            products[9].Location.Location.ShouldBe(16500);
        }

        [Fact]
        public void SpecifyingACustomPropertyNamerForASpecificType()
        {
            BuilderSettings builderSettings = new RepositoryBuilderSetup().DoSetup();
            builderSettings.SetPropertyNamerFor<Product>(new CustomProductPropertyNamer(new ReflectionUtil(),builderSettings));

            var products = new Builder(builderSettings).CreateListOfSize<Product>(10).Build();

            products[0].Location.Aisle.ShouldBe('A');
            products[0].Location.Shelf.ShouldBe(2);
            products[0].Location.Location.ShouldBe(1000);
            
            products[9].Location.Aisle.ShouldBe('J');
            products[9].Location.Shelf.ShouldBe(20);
            products[9].Location.Location.ShouldBe(10000);

            // Reset it afterwards so the other tests work as expected
            builderSettings.ResetToDefaults();
        }
    }    
}