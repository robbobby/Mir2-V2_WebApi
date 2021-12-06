using Bogus;
using Models_Mir2_V2_WebApi.Enums;
using Models_Mir2_V2_WebApi.Models;
using SharedModels_Mir2_V2.Enums;
namespace ServerDomainTests.RepositoryTests {
    public static class FakerCharacter {
        private static Faker _faker = new Faker();
        
        public static Faker<CharacterDbEntry> GetNewFakeCharacterDbEntry() {
            return new Faker<CharacterDbEntry>()
                .RuleFor(character => character.Account, FakerAccount.GetNewFakeAccountDbEntry())
                .RuleFor(character => character.Experience, faker => faker.Random.Int(0, 5000000))
                .RuleFor(character => character.Level, faker => faker.Random.Int(0, 60))
                .RuleFor(character => character.Gender, faker => faker.PickRandom<CharacterGender>())
                .RuleFor(character => character.Name, faker => faker.Person.UserName)
                .RuleFor(character => character.CharacterClass, faker => faker.PickRandom<CharacterClass>())
                .RuleFor(character => character.IsDeleted, false);
        }
    }
}
