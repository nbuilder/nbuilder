using System.Collections.Generic;
using System.Data.Entity;
using FizzWare.NBuilder;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.FunctionalTests.Model.Repositories;
using FizzWare.NBuilder.FunctionalTests.Support;
using NUnit.Framework;

public class EntityFrameworkBuilderSetup
{
    private bool setup;
    private static bool arInitialized;
    
    public BuilderSetup SetUp()
    {
        return DoSetup();
    }

    public BuilderSetup DoSetup()
    {
        DbConfiguration.SetConfiguration(new IntegrationTestConfiguration());
        using (var db = new ProductsDbContext())
        {
            db.Database.Delete();
        }

        BuilderSetup builderSetup = new BuilderSetup();

        if (setup)
            return builderSetup;

        setup = true;

        SetPersistenceMethod<Product>(builderSetup);
        SetPersistenceMethod<TaxType>(builderSetup);
        SetPersistenceMethod<Category>(builderSetup);
        return builderSetup;
    }

    private static void SetPersistenceMethod<T>(BuilderSetup builderSetup) where T: class
    {
        builderSetup.SetCreatePersistenceMethod<T>((item) =>
        {
            using (var db = new ProductsDbContext())
            {
                db.Set<T>().Attach(item);
                db.Set<T>().Add(item);
                db.SaveChanges();
            }
        });

        builderSetup.SetCreatePersistenceMethod<IList<T>>((items) =>
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

        builderSetup.SetUpdatePersistenceMethod<T>(item =>
        {
            using (var db = new ProductsDbContext())
            {
                db.Set<T>().Attach(item);
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        });

        builderSetup.SetUpdatePersistenceMethod<IList<T>>((items) =>
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