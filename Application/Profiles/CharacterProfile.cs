using AutoMapper;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
namespace Application.Profiles {
    [MapperProfile]
    
    public class CharacterProfile : Profile {
        public CharacterProfile() {
            //              From                 To
            CreateMap<CharacterRegisterDtoC2S, CharacterDbEntry>();
        }
    }
}
