using AutoMapper;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
namespace Application.Profiles {
    [MapperProfile]
    public class AccountProfile : Profile {
        public AccountProfile() {
                        // From                 To
            CreateMap<AccountRegisterDtoC2S, AccountDbEntry>();
            // CreateMap<AccountDbEntry, AccountRegisterDtoC2S>();
            CreateMap<AccountDbEntry, AccountLoginDto>();
        }
    }
}
