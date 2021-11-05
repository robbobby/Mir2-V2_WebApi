using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi.Model;
namespace Database_Mir2_V2_WebApi.Broker {
    public sealed class DbContextBroker : DbContext {
        public DbContextBroker(DbContextOptions<DbContextBroker> _options) : base(_options) {
            this.Database.Migrate();
        }

        public DbSet<AccountDbEntry> Accounts { get; set; }
        public DbSet<CharacterDbEntry> Characters { get; set; }
        public DbSet<ItemDbEntry> Items { get; set; }
    }
}