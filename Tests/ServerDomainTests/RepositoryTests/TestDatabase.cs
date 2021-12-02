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
                Faker<AccountDbEntry> account = GetNewFakeAccount();
                accounts.Add(account);
            }
            await AddDatabaseEntries<AccountDbEntry>(accounts);
            return accounts;
        }
        private static Faker<AccountDbEntry> GetNewFakeAccount() {
            return new Faker<AccountDbEntry>()
                .RuleFor(account => account.FirstName, faker => faker.Name.FirstName())
                .RuleFor(account => account.LastName, faker => faker.Name.LastName())
                .RuleFor(account => account.Email, faker => faker.Person.Email)
                .RuleFor(account => account.Password, faker => faker.Random.Hash())
                .RuleFor(account => account.UserName, faker => faker.Person.UserName)
                .RuleFor(account => account.SessionToken, Guid.NewGuid)
                .RuleFor(account => account.IsLoggedIn, false)
                .RuleFor(account => account.CreatedOn, DateTime.Now)
                .RuleFor(account => account.Salt, faker => faker.Random.Hash());
        }

        private async Task AddDatabaseEntries<T>(List<T> entities) {
            foreach (T entity in entities) {
                await DbContext.AddAsync(entity);
            }
            // await DbContext.AddRangeAsync(entities);
            await DbContext.SaveChangesAsync();
        }
        
        public void Dispose() {
            DbContext?.Dispose();
        }
        public async Task<List<CharacterDbEntry>> AddFakeCharactersToDatabase(int numberOfCharacters) {
            var fakeAccountList = await AddFakeAccountsToDatabase(1);
            AccountDbEntry account = fakeAccountList[0];
            List<CharacterDbEntry> characters = new();
            for (var i = 0; i < numberOfCharacters; i++) {
                var character = GetNewFakeCharacter();
                characters.Add(character);
            }
            await AddDatabaseEntries(characters);
            return characters;
        }
        private static Faker<CharacterDbEntry> GetNewFakeCharacter() {

            return new Faker<CharacterDbEntry>()
                .RuleFor(character => character.Account, GetNewFakeAccount())
                .RuleFor(character => character.Experience, faker => faker.Random.Int(0, 5000000))
                .RuleFor(character => character.Level, faker => faker.Random.Int(0, 60))
                .RuleFor(character => character.Gender, faker => faker.PickRandom<CharacterGender>())
                .RuleFor(character => character.Name, faker => faker.Person.UserName)
                .RuleFor(character => character.CharacterClass, faker => faker.PickRandom<CharacterClass>())
                .RuleFor(character => character.IsDeleted, false);
        }
        public async Task<List<ItemDbEntry>> AddFakeItemsToDatabase(int numberOfItems) {
            List<ItemDbEntry> items = new();
            for(var i = 0; i < numberOfItems; i++) {
                var item = GetNewFakeItem();
                items.Add(item);
            }
            await AddDatabaseEntries(items);

            return items;
        }
        private static Faker<ItemDbEntry> GetNewFakeItem() {

            return new Faker<ItemDbEntry>()
                .RuleFor(item => item.AccountDbEntry, GetNewFakeAccount())
                .RuleFor(item => item.ItemTypeId, faker => faker.Random.Short(0, 32767));
        }
    }
}
