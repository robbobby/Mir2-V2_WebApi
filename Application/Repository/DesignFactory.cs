using System.IO;
using Application.Repository.Broker;
using Application.Repository.PostgresLocalDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Application.Repository {
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContextBroker> {
        public DbContextBroker CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("DatabaseConfig.json")
                .Build();

            DbContextOptionsBuilder<DbContextBroker> dbContextBuilder = new DbContextOptionsBuilder<DbContextBroker>();

            PostgresConfig databaseDetails = new PostgresConfig();
            configuration.GetSection("PostgresDev").Bind(databaseDetails);

            dbContextBuilder.UseNpgsql(databaseDetails.GetConnectionString());

            return new DbContextBroker(dbContextBuilder.Options);
        }
    }
}