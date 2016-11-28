using System.Collections.Generic;
using System.Data.Entity;
using FizzWare.NBuilder;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

public class RepositoryBuilderSetup
{
    private bool setup;
    private static bool arInitialized;
    
    [SetUp]
    public BuilderSettings SetUp()
    {
        return DoSetup();
    }

    public BuilderSettings DoSetup()
    {
        DbConfiguration.SetConfiguration(new IntegrationTestConfiguration());
        using (var db = new ProductsDbContext())
        {
            db.Database.Delete();
        }

        BuilderSettings builderSettings = new BuilderSettings();

        if (setup)
            return builderSettings;

        


        setup = true;

        var productRepository = Dependency.Resolve<IProductRepository>();
        var taxTypeRepository = Dependency.Resolve<ITaxTypeRepository>();
        var categoryRepository = Dependency.Resolve<ICategoryRepository>();

        builderSettings.SetCreatePersistenceMethod<Product>(productRepository.Create);
        builderSettings.SetCreatePersistenceMethod<IList<Product>>(productRepository.CreateAll);

        builderSettings.SetCreatePersistenceMethod<TaxType>(taxTypeRepository.Create);
        builderSettings.SetCreatePersistenceMethod<IList<TaxType>>(taxTypeRepository.CreateAll);

        builderSettings.SetCreatePersistenceMethod<Category>(categoryRepository.Create);
        builderSettings.SetCreatePersistenceMethod<IList<Category>>(categoryRepository.CreateAll);

        builderSettings.SetUpdatePersistenceMethod<Category>(categoryRepository.Save);
        builderSettings.SetUpdatePersistenceMethod<IList<Category>>(categoryRepository.SaveAll);
        return builderSettings;
    }
}