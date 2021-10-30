using System.IO;
using Database_Mir2_V2_WebApi.PostgresLocalDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Mir2_V2_WebApi.Broker;
public class CampContextFactory : IDesignTimeDbContextFactory<DbContextBroker> {
    public DbContextBroker CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("DatabaseConfig.json")
            .Build();

        var dbContextBuilder = new DbContextOptionsBuilder();

        PostgresConfig databaseDetails = new PostgresConfig();
        configuration.GetSection("PostgresDev").Bind(databaseDetails);

        dbContextBuilder.UseNpgsql(databaseDetails.GetConnectionString());

        return new DbContextBroker(dbContextBuilder.Options);
    }
}