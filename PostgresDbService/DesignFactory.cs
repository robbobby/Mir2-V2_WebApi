using System.IO;
using Database_Mir2_V2_WebApi.Broker;
using Database_Mir2_V2_WebApi.PostgresLocalDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Database_Mir2_V2_WebApi {
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