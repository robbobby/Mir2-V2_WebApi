using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repository.Broker;
using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using Serilog.Core;
using Xunit;
using Xunit.Abstractions;
namespace ServerDomainTests.RepositoryTests.BrokerTests {
    public class DbContextBrokerTests : IClassFixture<TestDatabase> {
        private ITestOutputHelper _outputHelper;

        private TestDatabase _testDatabase;

        public DbContextBrokerTests(TestDatabase testDatabase, ITestOutputHelper outputHelper) {
            _outputHelper = outputHelper;
            _testDatabase = testDatabase;
            using TestDatabase database = new TestDatabase();
        }
        
        [Fact]
        public async void Has_AccountDbSet() {
            _testDatabase = new TestDatabase();
            const int numberOfAccounts = 3;
            var account = await _testDatabase.AddFakeAccountsToDatabase(numberOfAccounts);
            var result = _testDatabase.DbContext.Accounts.ToList();
            for (int i = 0; i < numberOfAccounts; i++) {
                Assert.True(result.Count == numberOfAccounts);
                Assert.Equal(result[i].Email, account[i].Email);
            }
        }

        [Fact]
        public async void Has_CharacterDbSet() {
            _testDatabase = new TestDatabase();
            const int numberOfCharacters = 3;
            var character = await _testDatabase.AddFakeCharactersToDatabase(numberOfCharacters);
            var result = _testDatabase.DbContext.Characters.ToList();

            for (var i = 0; i < numberOfCharacters; i++) {
                Assert.True(result.Count == numberOfCharacters);
                Assert.Equal(result[i].Name, character[i].Name);
            }
        }

        [Fact]
        public async void Has_ItemDbSet() {
            _testDatabase = new TestDatabase();
            const int numberOfItems = 3;
            List<ItemDbEntry> items = await _testDatabase.AddFakeItemsToDatabase(numberOfItems);
            var result = _testDatabase.DbContext.Items.ToList();
        }
    }
}
