using Models_Mir2_V2_WebApi.Enums;
namespace SharedModels_Mir2_V2.Model {

    public class Character {
        public string Id { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public CharacterClass CharacterClass { get; set; }
    }
}
