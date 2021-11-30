using System;
using System.ComponentModel.DataAnnotations;
using Models_Mir2_V2_WebApi.Attributes;
using SharedModels_Mir2_V2.BaseModels;
namespace Models_Mir2_V2_WebApi.Model {
    public class AccountDbEntry {

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required][UniqueEmail]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public Guid SessionToken { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
