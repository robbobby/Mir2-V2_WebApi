using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository.Broker;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi.Enums;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using SharedModels_Mir2_V2.BaseModels;
using SharedModels_Mir2_V2.Enums;
namespace ServerDomainTests.RepositoryTests {
    public class TestDatabase : IDisposable {

        public DbContextBroker DbContext { get; }
        public TestDatabase() {
            var dbContextOptions = new DbContextOptionsBuilder<DbContextBroker>().UseInMemoryDatabase("Mir-V2_Database").Options;
            DbContext = new DbContextBroker(dbContextOptions, true);
        }

        public async Task<List<AccountDbEntry>> AddFakeAccountsToDatabase(int numberOfAccounts = 1) {
            List<AccountDbEntry> accounts = new(); 
            for (var i = 0; i < numberOfAccounts; i++) {
                var account = FakerAccount.GetNewFakeAccountDbEntry();
                accounts.Add(account);
            }
            await AddDatabaseEntries<AccountDbEntry>(accounts);
            return accounts;
        }

        private async Task AddDatabaseEntries<T>(List<T> entities) where T : class {
            foreach (T entity in entities) {
                await DbContext.AddAsync(entity);
                // DbContext.Entry<T>(entity).State = EntityState.Detached;
            }
            // await DbContext.AddRangeAsync(entities);
            await DbContext.SaveChangesAsync();
        }
        
        public void Dispose() {
            DbContext.Database.EnsureDeletedAsync();
            DbContext?.Dispose();
        }
        public async Task<List<CharacterDbEntry>> AddFakeCharactersToDatabase(int numberOfCharacters) {
            var fakeAccountList = await AddFakeAccountsToDatabase(1);
            AccountDbEntry account = fakeAccountList[0];
            List<CharacterDbEntry> characters = new();
            for (var i = 0; i < numberOfCharacters; i++) {
                var character = FakerCharacter.GetNewFakeCharacterDbEntry();
                characters.Add(character);
            }
            await AddDatabaseEntries(characters);
            return characters;
        }

        public async Task<List<ItemDbEntry>> AddFakeItemsToDatabase(int numberOfItems) {
            List<ItemDbEntry> items = new();
            for(var i = 0; i < numberOfItems; i++) {
                var item = GetNewFakeItemDbEntry();
                items.Add(item);
            }
            await AddDatabaseEntries(items);

            return items;
        }
        private static Faker<ItemDbEntry> GetNewFakeItemDbEntry() {

            return new Faker<ItemDbEntry>()
                .RuleFor(item => item.AccountDbEntry, FakerAccount.GetNewFakeAccountDbEntry())
                .RuleFor(item => item.ItemTypeId, faker => faker.Random.Short(0, 32767));
        }
    }
}
