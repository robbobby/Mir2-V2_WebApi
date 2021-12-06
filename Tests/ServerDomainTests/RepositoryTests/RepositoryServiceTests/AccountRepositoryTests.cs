using System.Linq;
using Application.Profiles;
using Application.Repository.RepositoryService;
using AutoMapper;
using Bogus;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.Enums;
using Xunit;
using Xunit.Abstractions;
namespace ServerDomainTests.RepositoryTests.RepositoryServiceTests {
    public class AccountRepositoryTests {
        private readonly AccountRepository _sut;
        private static readonly AccountLoginDtoC2S _validLogin = new();
        private AccountLoginDtoC2S _invalidLoginPassword = new();
        private readonly AccountLoginDtoC2S _invalidAccount = new();
        private Faker _faker = new Faker();
        private readonly AccountRegisterDtoC2S _accountRegisterDtoC2S;
        private readonly ITestOutputHelper _writer;
        private readonly TestDatabase _dbContext = new TestDatabase();


        public AccountRepositoryTests(ITestOutputHelper writer) {
            _writer = writer;
            MapperConfiguration config = new MapperConfiguration(config => {
                config.AddProfile<AccountProfile>();
                config.AddProfile<CharacterProfile>(); 
            });
            IMapper mapper = new Mapper(config);
            _sut = new AccountRepository(_dbContext.DbContext, mapper);
            _accountRegisterDtoC2S = SetUpNewRegisterDto();
            _validLogin.UserName = _accountRegisterDtoC2S.UserName;
            _validLogin.Password = _accountRegisterDtoC2S.Password;
        }

        private static void SetFakeAccountDbEntryDetails(AccountDbEntry account) {
            account.UserName = _validLogin.UserName;
            account.Password = _validLogin.Password;
        }

        [Fact]
        public void CanStartTestsWithoutError() {
            Assert.True(true);
        }

        [Fact]
        public async void PostAccount_Returns_Success_SuccessfullyAddedToDatabase() {
            AccountRegisterResult result = await _sut.PostAccount(_accountRegisterDtoC2S);
            Assert.True(result == AccountRegisterResult.Success);
        }
        
        [Fact]
        public async void PostAccount_Returns_EmailNotValid_WhenEmailIsNotValid() {
            _accountRegisterDtoC2S.Email = "NotAValidEmail";
            AccountRegisterResult x = await _sut.PostAccount(_accountRegisterDtoC2S);
            Assert.True(x == AccountRegisterResult.EmailNotValid);
        }
        
        [Fact]
        public async void PostAccount_Returns_UserNameAlreadyExists_WhenUsernameAlreadyExists() {
            await _dbContext.DbContext.Database.EnsureDeletedAsync();
            AccountRegisterResult postAccountResult = await _sut.PostAccount(_accountRegisterDtoC2S);
            _accountRegisterDtoC2S.Email = "ChangeEmail_ToTest_Cannot_ReRegisterSameUserName@a";
            AccountRegisterResult postAccount = await _sut.PostAccount(_accountRegisterDtoC2S);
            Assert.True(postAccountResult == AccountRegisterResult.Success);
            Assert.True(postAccount == AccountRegisterResult.UserNameAlreadyExists);
            Assert.True(_dbContext.DbContext.Accounts.ToList().Count == 1);
        }
        
        [Fact]
        public async void PostAccount_Returns_EmailAlreadyRegistered_WhenEmailIsAlreadyRegistered() {
            await _dbContext.DbContext.Database.EnsureDeletedAsync();
            AccountRegisterResult postedAccount = await _sut.PostAccount(_accountRegisterDtoC2S);
            AccountRegisterResult postedAccount2 = await _sut.PostAccount(_accountRegisterDtoC2S);
            _writer.WriteLine(_dbContext.DbContext.Accounts.ToList().Count.ToString());
            Assert.True(postedAccount == AccountRegisterResult.Success);
            Assert.True(postedAccount2 == AccountRegisterResult.EmailAlreadyExists);
            Assert.True(_dbContext.DbContext.Accounts.ToList().Count == 1);
        }

        [Fact]
        public async void GetAccount_Returns_UserNameDoesNotExist_IfUsernameIsNotRegistered() {
            await _dbContext.DbContext.Database.EnsureDeletedAsync();
            AccountRegisterResult x = await _sut.PostAccount(_accountRegisterDtoC2S);
            AccountLoginResult result = await _sut.GetAccount(_invalidAccount);
            Assert.Equal(LoginResult.UserNameDoesNotExist, result.LoginResult);
        }

        [Fact]
        public async void GetAccount_Returns_WrongPassword_WhenPasswordIsWrong() {
            await _dbContext.DbContext.Database.EnsureDeletedAsync();
            AccountRegisterResult x = await _sut.PostAccount(_accountRegisterDtoC2S);
            _validLogin.Password = "SomeWrongPassword";
            AccountLoginResult result = await _sut.GetAccount(_validLogin);
            Assert.Equal(LoginResult.WrongPassword, result.LoginResult);
        }
        
        [Fact(Skip = "Need to implement 'LoggedIn' functionality")]
        public async void GetAccount_Returns_AlreadyLoggedIn_WhenAccountIsAlreadyLoggedIn() {
            await _dbContext.DbContext.Database.EnsureDeletedAsync();
            AccountRegisterResult x = await _sut.PostAccount(_accountRegisterDtoC2S);
            AccountLoginResult result = await _sut.GetAccount(_validLogin);
            Assert.Equal(LoginResult.AlreadyLoggedIn, result.LoginResult);
        }

        [Fact]
        public void IsEmailOrUsernameAlreadyRegistered_returns_OK_When_NeitherRegistered() {
            var result = _sut.IsEmailOrUserNameAlreadyRegistered("someRandomEmail@ae", "SomeRandomUserName");
            Assert.Equal(AccountRegisterResult.Ok,result);
        }
        
        [Fact]
        public void IsEmailOrUsernameAlreadyRegistered_Returns_EmailAlreadyRegistered_When_EmailIsAlreadyRegistered() {
            _sut.PostAccount(_accountRegisterDtoC2S);
            var result = _sut.IsEmailOrUserNameAlreadyRegistered(_accountRegisterDtoC2S.Email, "SomeUnique_Username");
            Assert.Equal(AccountRegisterResult.EmailAlreadyExists,result);
        }
        
        [Fact]
        public void IsEmailOrUsernameAlreadyRegistered_Returns_UserNameAlreadyRegistered_When_UserNameIsAlreadyRegistered() {
            _sut.PostAccount(_accountRegisterDtoC2S);
            var result = _sut.IsEmailOrUserNameAlreadyRegistered("SomeUnique_Email@a", _accountRegisterDtoC2S.UserName);
            Assert.Equal(AccountRegisterResult.UserNameAlreadyExists,result);
        }
        
        private AccountRegisterDtoC2S SetUpNewRegisterDto() {
            return new Faker<AccountRegisterDtoC2S>()
                .RuleFor(account => account.FirstName, faker => faker.Name.FirstName())
                .RuleFor(account => account.LastName, faker => faker.Name.LastName())
                .RuleFor(account => account.Email, faker => faker.Person.Email)
                .RuleFor(account => account.Password, faker => faker.Random.Hash())
                .RuleFor(account => account.UserName, faker => faker.Person.UserName);
        }
    }
}
