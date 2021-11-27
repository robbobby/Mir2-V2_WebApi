using System.ComponentModel.DataAnnotations;
using Models_Mir2_V2_WebApi.Enums;
using SharedModels_Mir2_V2.BaseModels;

namespace Models_Mir2_V2_WebApi.Model {

    public class CharacterDbEntry {
        [Key]
        public string Id { get; set; }
        public AccountDbEntry AccountDbEntry { get; set; }
        public string Name { get; set; }
        public CharacterClass CharacterClass { get; set; }
    }
}
