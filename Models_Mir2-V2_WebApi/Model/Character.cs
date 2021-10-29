using System.ComponentModel.DataAnnotations;
using Models_Mir2_V2_WebApi.Enums;

namespace Models_Mir2_V2_WebApi.Model {

    public class Character {
        [Key]
        public string Id { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public CharacterClass CharacterClass { get; set; }
    }
}
