using AutoMapper;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
namespace Mir2_V2_WebApi.Profiles {
    public class AccountProfile : Profile {
        public AccountProfile() {
            CreateMap<AccountRegisterDtoC2S, AccountDbEntry>();
            CreateMap<AccountDbEntry, AccountLoginDtoS2C>();
        }
    }
}
