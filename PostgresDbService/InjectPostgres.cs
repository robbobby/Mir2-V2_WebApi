using Database_Mir2_V2_WebApi.PostgresLocalDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mir2_V2_WebApi.Broker;
using Mir2_V2_WebApi.Controllers;
using Serilog;

namespace Database_Mir2_V2_WebApi {
    public class InjectPostgres {
        
        public void InjectAccountDb(IConfiguration _configuration, IServiceCollection _services) {
            PostgresConfig databaseDetails = new PostgresConfig();
            _configuration.GetSection("database:PostgresDev").Bind(databaseDetails);
            
            Log.Debug(databaseDetails.GetConnectionString());
            
            _services.AddDbContext<DbContextBroker>(_context => _context.UseNpgsql(databaseDetails.GetConnectionString()));
            
            _services.AddTransient<IDataAccess, AccountRepository>();
        }
    }
}