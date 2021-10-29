using Database_Mir2_V2_WebApi.Broker;
using Database_Mir2_V2_WebApi.PostgresLocalDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database_Mir2_V2_WebApi {
    public static class InjectPostgres {
        public static void InjectAccountDb() {
            PostgresConfig databaseDetails = new PostgresConfig();
        }
        public static void InjectAccountDb(IConfiguration _configuration, IServiceCollection _services) {
            PostgresConfig databaseDetails = new PostgresConfig();
            _configuration.GetSection("database:PostgresDev").Bind(databaseDetails);

            _services.AddDbContext<AccountBroker>(_context => _context.UseNpgsql(databaseDetails.GetConnectionString()));
        }
    }
}
