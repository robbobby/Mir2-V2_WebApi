using Application.Repository.Broker;
using Application.Repository.PostgresLocalDev;
using Application.Repository.RepositoryService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using Serilog;
namespace Application.Repository {
    public class InjectPostgres {
        
        public void InjectAccountDb(IConfiguration configuration, IServiceCollection services) {
            PostgresConfig databaseDetails = new PostgresConfig();
            configuration.GetSection("database:PostgresDev").Bind(databaseDetails);
            
            services.AddDbContext<DbContextBroker>(_context => _context.UseNpgsql(databaseDetails.GetConnectionString()));
            
            services.AddTransient<IAccountAccessService<AccountDbEntry>, AccountRepository>();
            services.AddTransient<ICharacterAccessService<CharacterDbEntry>, CharacterRepository>();
        }
    }
}
