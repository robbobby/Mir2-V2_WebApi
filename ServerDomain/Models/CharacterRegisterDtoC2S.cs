using Models_Mir2_V2_WebApi.Enums;
using SharedModels_Mir2_V2.Enums;
namespace Models_Mir2_V2_WebApi.Model {
    public class CharacterRegisterDtoC2S {
        public string Name { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public CharacterGender Gender { get; set; }
    }
}
