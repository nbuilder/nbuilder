using System.Collections.Generic;
using FizzWare.NBuilder.Tests.Integration.Models;
using FizzWare.NBuilder.Tests.Integration.Models.Repositories;
using FizzWare.NBuilder.Tests.Integration.Support;


namespace FizzWare.NBuilder.Tests.Integration
{
    public class RepositoryBuilderSetup
    {
        private bool _setup;

        public RepositoryBuilderSetup()
        {
            this.Products = Dependency.Resolve<IProductRepository>();
            this.TaxTypes = Dependency.Resolve<ITaxTypeRepository>();
            this.Categories = Dependency.Resolve<ICategoryRepository>();
        }

        public BuilderSettings DoSetup()
        {
            BuilderSettings builderSettings = new BuilderSettings();

            if (_setup)
                return builderSettings;

        


            _setup = true;

            builderSettings.SetCreatePersistenceMethod<Product>(Products.Create);
            builderSettings.SetCreatePersistenceMethod<IList<Product>>(Products.CreateAll);

            builderSettings.SetCreatePersistenceMethod<TaxType>(TaxTypes.Create);
            builderSettings.SetCreatePersistenceMethod<IList<TaxType>>(TaxTypes.CreateAll);

            builderSettings.SetCreatePersistenceMethod<Category>(Categories.Create);
            builderSettings.SetCreatePersistenceMethod<IList<Category>>(Categories.CreateAll);

            builderSettings.SetUpdatePersistenceMethod<Category>(Categories.Save);
            builderSettings.SetUpdatePersistenceMethod<IList<Category>>(Categories.SaveAll);
            return builderSettings;
        }

        public ICategoryRepository Categories { get; set; }

        public ITaxTypeRepository TaxTypes { get; set; }

        public IProductRepository Products { get; set; }
    }
}