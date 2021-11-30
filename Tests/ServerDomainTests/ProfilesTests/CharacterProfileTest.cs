using Application.Profiles;
using AutoMapper;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using Xunit;
namespace ServerDomainTests.ProfilesTests {
    public class CharacterProfileTest {
        private readonly IMapper _sut;
        private readonly MapperConfiguration _config;
        public CharacterProfileTest() {
            _config = new MapperConfiguration(config => config.AddProfile<CharacterProfile>());
            _sut = new Mapper(_config);
        }

        [Fact] public void CanMap_CharacterRegisterDtoC2S_To_CharacterDbEntry() {
            CharacterDbEntry resultOfMap = _sut.Map<CharacterDbEntry>(new CharacterRegisterDtoC2S());
            Assert.Equal(typeof(CharacterDbEntry), resultOfMap.GetType());
        }
    }
}
