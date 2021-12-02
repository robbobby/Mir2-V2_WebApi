using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
namespace Application.Repository.Broker {
    public sealed class DbContextBroker : DbContext {
        public DbContextBroker(DbContextOptions<DbContextBroker> options, bool isTestEnvironment = false) : base(options) {
            if(!isTestEnvironment)
                Database.Migrate();
        }

        public DbSet<AccountDbEntry> Accounts { get; set; }
        public DbSet<CharacterDbEntry> Characters { get; set; }
        public DbSet<ItemDbEntry> Items { get; set; }
    }
}
