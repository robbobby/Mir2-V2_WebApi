using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi.Model;
namespace Database_Mir2_V2_WebApi.Broker {
    public sealed class DbContextBroker : DbContext {
        public DbContextBroker(DbContextOptions _options) : base(_options) {
            this.Database.Migrate();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}