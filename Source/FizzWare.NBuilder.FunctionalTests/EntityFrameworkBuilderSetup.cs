using System.Collections.Generic;
using System.Data.Entity;
using FizzWare.NBuilder;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

public class EntityFrameworkBuilderSetup
{
    private bool _setup;

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
            using (var db = new ProductsDbContext())
            {
                db.Set<T>().Attach(item);
                db.Set<T>().Add(item);
                db.SaveChanges();
            }
        });

        builderSettings.SetCreatePersistenceMethod<IList<T>>((items) =>
        {
            using (var db = new ProductsDbContext())
            {
                foreach (var item in items)
                {
                    db.Set<T>().Attach(item);
                    db.Set<T>().Add(item);
                }
                db.SaveChanges();
            }
        });

        builderSettings.SetUpdatePersistenceMethod<T>(item =>
        {
            using (var db = new ProductsDbContext())
            {
                db.Set<T>().Attach(item);
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        });

        builderSettings.SetUpdatePersistenceMethod<IList<T>>((items) =>
        {
            using (var db = new ProductsDbContext())
            {
                foreach (var item in items)
                {
                    db.Set<T>().Attach(item);
                    db.Entry(item).State = EntityState.Modified;
                    db.Set<T>().Add(item);
                }
                db.SaveChanges();
            }
        });
    }
}