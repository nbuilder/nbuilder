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

        BuilderSetup.SetCreatePersistenceMethod<Product>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<IProductRepository>();
                    productRepository.Create(x);
                }
            );

        BuilderSetup.SetCreatePersistenceMethod<IList<Product>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<IProductRepository>();
                    productRepository.CreateAll(x);
                }
            );

        BuilderSetup.SetCreatePersistenceMethod<TaxType>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ITaxTypeRepository>();
                    productRepository.Create(x);
                }
            );

        BuilderSetup.SetCreatePersistenceMethod<IList<TaxType>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ITaxTypeRepository>();
                    productRepository.CreateAll(x);
                }
            );

        BuilderSetup.SetCreatePersistenceMethod<Category>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ICategoryRepository>();
                    productRepository.Create(x);
                }
            );

        BuilderSetup.SetCreatePersistenceMethod<IList<Category>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ICategoryRepository>();
                    productRepository.CreateAll(x);
                }
            );

        BuilderSetup.SetUpdatePersistenceMethod<Category>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ICategoryRepository>();
                    productRepository.Save(x);
                }
            );

        BuilderSetup.SetUpdatePersistenceMethod<IList<Category>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ICategoryRepository>();
                    productRepository.SaveAll(x);
                }
            );
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
