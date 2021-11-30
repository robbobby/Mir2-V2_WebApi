using Application.Profiles;
using AutoMapper;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2.AccountDto;
using Xunit;
namespace ServerDomainTests.ProfilesTests {
    public class AccountProfileTests {
        private IMapper _sut;
        private MapperConfiguration config;
        public AccountProfileTests() {
            config = new MapperConfiguration(config => config.AddProfile<AccountProfile>());
            _sut = new Mapper(config);
        }

        [Fact] public void CanMap_AccountRegisterDtoC2S_To_AccountDbEntry() {
            AccountDbEntry resultOfMap = _sut.Map<AccountDbEntry>(new AccountRegisterDtoC2S());
            Assert.Equal(typeof(AccountDbEntry), resultOfMap.GetType());
        }
        
        [Fact] public void CanMap_AccountDbEntry_To_AccountLoginDto() {
            AccountLoginDto resultOfMap = _sut.Map<AccountLoginDto>(new AccountDbEntry());
            Assert.Equal(typeof(AccountLoginDto), resultOfMap.GetType());
        }
    }
}
