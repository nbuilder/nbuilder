using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FizzWare.NBuilder.FunctionalTests.Model;

public class IntegrationTestConfiguration : DbConfiguration
{
    public IntegrationTestConfiguration()
    {
        base.SetDatabaseInitializer(new DropCreateDatabaseAlways<ProductsDbContext>());
        base.SetDefaultConnectionFactory(new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0"));
    }
}