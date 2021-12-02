using System.ComponentModel.DataAnnotations;
using Models_Mir2_V2_WebApi.Model;
namespace Models_Mir2_V2_WebApi.Models {
    public class ItemDbEntry {
        
        [Key]
        public int Id { get; set; }
        public short ItemTypeId { get; set; }
        public AccountDbEntry AccountDbEntry { get; set; }
        
    }
}
