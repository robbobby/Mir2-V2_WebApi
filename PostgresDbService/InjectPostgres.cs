using Database_Mir2_V2_WebApi.Broker;
using Database_Mir2_V2_WebApi.PostgresLocalDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;

namespace Database_Mir2_V2_WebApi {
    public class InjectPostgres {
        
        public void InjectAccountDb(IConfiguration _configuration, IServiceCollection _services) {
            PostgresConfig databaseDetails = new PostgresConfig();
            _configuration.GetSection("database:PostgresDev").Bind(databaseDetails);
            
            Log.Debug(databaseDetails.GetConnectionString());
            
            _services.AddDbContext<DbContextBroker>(_context => _context.UseNpgsql(databaseDetails.GetConnectionString()));
            
            _services.AddTransient<IDataAccess<Account>, AccountRepository>();
        }
    }
}
