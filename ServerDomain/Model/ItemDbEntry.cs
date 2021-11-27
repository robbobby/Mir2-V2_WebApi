using System.ComponentModel.DataAnnotations;
using SharedModels_Mir2_V2.BaseModels;
namespace Models_Mir2_V2_WebApi.Model {
    public class ItemDbEntry {
        
        [Key]
        public string Id { get; set; }
    }
}
