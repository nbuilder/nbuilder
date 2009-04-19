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

[SetUpFixture]
public class SetupFixture
{
    private bool setup;
    private static bool arInitialized;

    [SetUp]
    public void SetUp()
    {
        EnsureActiveRecordInitialized();

        if (setup)
            return;

        setup = true;

        var productRepository = Dependency.Resolve<IProductRepository>();
        var taxTypeRepository = Dependency.Resolve<ITaxTypeRepository>();
        var categoryRepository = Dependency.Resolve<ICategoryRepository>();

        BuilderSetup.SetCreatePersistenceMethod<Product> ( productRepository.Create );
        BuilderSetup.SetCreatePersistenceMethod<IList<Product>> ( productRepository.CreateAll);
        BuilderSetup.SetCreatePersistenceMethod<TaxType>( taxTypeRepository.Create );
        BuilderSetup.SetCreatePersistenceMethod<IList<TaxType>> ( taxTypeRepository.CreateAll );
        BuilderSetup.SetCreatePersistenceMethod<Category> ( categoryRepository.Create );
        BuilderSetup.SetCreatePersistenceMethod<IList<Category>> ( categoryRepository.CreateAll );
        BuilderSetup.SetUpdatePersistenceMethod<Category> ( categoryRepository.Save );
        BuilderSetup.SetUpdatePersistenceMethod<IList<Category>> ( categoryRepository.SaveAll );
    }

    [TestFixtureSetUp]
    public void EnsureActiveRecordInitialized()
    {
        if (arInitialized)
            return;

        arInitialized = true;

        ActiveRecordStarter.Initialize(typeof(Product).Assembly, ActiveRecordSectionHandler.Instance);

        ActiveRecordStarter.CreateSchema();
    }
}
