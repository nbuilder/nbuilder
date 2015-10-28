using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using FizzWare.NBuilder;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

public class SetupFixture
{
    private bool setup;
    private static bool arInitialized;
    
    [SetUp]
    public BuilderSetup SetUp()
    {
        return DoSetup();
    }

    public BuilderSetup DoSetup()
    {
        BuilderSetup builderSetup = new BuilderSetup();
        EnsureActiveRecordInitialized();

        if (setup)
            return builderSetup;

        setup = true;

        var productRepository = Dependency.Resolve<IProductRepository>();
        var taxTypeRepository = Dependency.Resolve<ITaxTypeRepository>();
        var categoryRepository = Dependency.Resolve<ICategoryRepository>();

        builderSetup.SetCreatePersistenceMethod<Product>(productRepository.Create);
        builderSetup.SetCreatePersistenceMethod<IList<Product>>(productRepository.CreateAll);
        builderSetup.SetCreatePersistenceMethod<TaxType>(taxTypeRepository.Create);
        builderSetup.SetCreatePersistenceMethod<IList<TaxType>>(taxTypeRepository.CreateAll);
        builderSetup.SetCreatePersistenceMethod<Category>(categoryRepository.Create);
        builderSetup.SetCreatePersistenceMethod<IList<Category>>(categoryRepository.CreateAll);
        builderSetup.SetUpdatePersistenceMethod<Category>(categoryRepository.Save);
        builderSetup.SetUpdatePersistenceMethod<IList<Category>>(categoryRepository.SaveAll);
        return builderSetup;
    }

    public void EnsureActiveRecordInitialized()
    {
        if (arInitialized)
            return;

        arInitialized = true;

        ActiveRecordStarter.Initialize(typeof(Product).Assembly, ActiveRecordSectionHandler.Instance);

        ActiveRecordStarter.CreateSchema();
    }
}
