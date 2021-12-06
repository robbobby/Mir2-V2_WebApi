using Application.Profiles;
using Application.Repository.RepositoryService;
using AutoMapper;
using Bogus;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using Xunit.Abstractions;
namespace ServerDomainTests.RepositoryTests.RepositoryServiceTests {
    public class CharacterRepositoryTests {
        private readonly CharacterRepository _sut;
        private readonly CharacterRegisterDtoC2S _validCharacterRegister = new CharacterRegisterDtoC2S();
        private readonly CharacterRegisterDtoC2S _invalidCharacterRegister = new CharacterRegisterDtoC2S();
        private ITestOutputHelper _writer;
        private Faker _faker = new Faker();
        private readonly TestDatabase _dbContext = new TestDatabase();
        
        public CharacterRepositoryTests(ITestOutputHelper writer) {
            _writer = writer;
            MapperConfiguration config = new MapperConfiguration(config => {
                config.AddProfile<AccountProfile>();
                config.AddProfile<CharacterProfile>(); 
            });
            IMapper mapper = new Mapper(config);
            _sut = new CharacterRepository(_dbContext.DbContext, mapper);
            
        }
    }
    
}
