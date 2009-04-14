using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using FizzWare.NBuilder;
using FizzWare.NBuilder.FunctionalTests;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using NUnit.Framework;

[SetUpFixture]
public class SetupFixture
{
    private static bool setup;

    [TestFixtureSetUp]
    public void TestFixtureSetUp()
    {
        if (setup)
            return;

        setup = true;

        ActiveRecordStarter.Initialize(typeof(Product).Assembly, ActiveRecordSectionHandler.Instance);  

        ActiveRecordStarter.CreateSchema();

        BuilderSetup.SetPersistenceMethod<Product>
            (
                x => 
                {
                    var productRepository = Dependency.Resolve<IProductRepository>();
                    productRepository.Create(x);
                }
            );

        BuilderSetup.SetPersistenceMethod<IList<Product>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<IProductRepository>();
                    productRepository.CreateAll(x);
                }
            );

        BuilderSetup.SetPersistenceMethod<TaxType>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ITaxTypeRepository>();
                    productRepository.Create(x);
                }
            );

        BuilderSetup.SetPersistenceMethod<IList<TaxType>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ITaxTypeRepository>();
                    productRepository.CreateAll(x);
                }
            );

        BuilderSetup.SetPersistenceMethod<Category>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ICategoryRepository>();
                    productRepository.Create(x);
                }
            );

        BuilderSetup.SetPersistenceMethod<IList<Category>>
            (
                x =>
                {
                    var productRepository = Dependency.Resolve<ICategoryRepository>();
                    productRepository.CreateAll(x);
                }
            );
    }
}
