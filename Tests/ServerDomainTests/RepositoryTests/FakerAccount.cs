using System;
using Bogus;
using Models_Mir2_V2_WebApi.Enums;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using SharedModels_Mir2_V2.Enums;

namespace ServerDomainTests.RepositoryTests {
    public static class FakerAccount {
        private static Faker _faker;
        
        public static Faker<AccountDbEntry> GetNewFakeAccountDbEntry() {
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
    }
}
