using System.Collections.Generic;
using FizzWare.NBuilder;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

public class PersistenceTestsBuilderSetup
{
    private bool _setup;

    public BuilderSettings SetUp()
    {
        return DoSetup();
    }

    public BuilderSettings DoSetup()
    {
        new ProductRepository().DeleteAll();
        new CategoryRepository().DeleteAll();
        new TaxTypeRepository().DeleteAll();

        BuilderSettings builderSettings = new BuilderSettings();

        if (_setup)
            return builderSettings;

        _setup = true;

        SetPersistenceMethod<Product>(builderSettings);
        SetPersistenceMethod<TaxType>(builderSettings);
        SetPersistenceMethod<Category>(builderSettings);
        return builderSettings;
    }

    private static void SetPersistenceMethod<T>(BuilderSettings builderSettings) where T: class
    {
        builderSettings.SetCreatePersistenceMethod<T>((item) =>
        {
            new BaseRepository<T>().Create(item);
        });

        builderSettings.SetCreatePersistenceMethod<IList<T>>((items) =>
        {
            new BaseRepository<T>().CreateAll(items);
        });

        builderSettings.SetUpdatePersistenceMethod<T>(item =>
        {
            new BaseRepository<T>().Save(item);
        });

        builderSettings.SetUpdatePersistenceMethod<IList<T>>((items) =>
        {
            new BaseRepository<T>().SaveAll(items);
        });
    }
}