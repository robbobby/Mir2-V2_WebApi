using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
namespace Application.Repository.Broker {
    public class DbContextBroker : DbContext {
        public DbContextBroker(DbContextOptions<DbContextBroker> options, bool isTestEnvironment = false) : base(options) {
            if(!isTestEnvironment)
                Database.Migrate();
            else {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }

        public void SetDbSetsToMock(DbSet<AccountDbEntry> mockAccounts, DbSet<CharacterDbEntry> mockCharacters, DbSet<ItemDbEntry> mockItems) {
            Accounts = mockAccounts;
            Characters = mockCharacters;
            Items = mockItems;
        }

        public DbSet<AccountDbEntry> Accounts { get; set; }
        public DbSet<CharacterDbEntry> Characters { get; set; }
        public DbSet<ItemDbEntry> Items { get; set; }
    }
}
