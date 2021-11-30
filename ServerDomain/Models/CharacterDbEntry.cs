using System;
using System.ComponentModel.DataAnnotations;
using Models_Mir2_V2_WebApi.Enums;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2.Enums;
namespace Models_Mir2_V2_WebApi.Models {

    public class CharacterDbEntry {
        [Key]
        public int Id { get; set; }
        [Required]
        public AccountDbEntry Account { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public CharacterClass CharacterClass { get; set; }
        [Required]
        public CharacterGender Gender { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int Experience { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }

}
