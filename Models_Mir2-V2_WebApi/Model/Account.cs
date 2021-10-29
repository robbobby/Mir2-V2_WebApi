using System.ComponentModel.DataAnnotations;
namespace Models_Mir2_V2_WebApi.Model {
    public class Account {

        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
